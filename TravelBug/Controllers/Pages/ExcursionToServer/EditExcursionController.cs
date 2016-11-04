using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TravelBug.Controllers.Components;
using TravelBug.Models.TravelBugModel;

namespace TravelBug.Controllers.Pages.ExcursionToServer
{
    public class EditExcursionController : BaseController
    {
        [HttpGet]
        public ActionResult Index(int excursionID)
        {
            ViewBag.LanguageCollecion = manager.GetLanguage().ToList();
            var currentExcursion = manager.GetExcursionByID(excursionID);

            return View(currentExcursion);
        }

        [HttpPost]
        public ActionResult Index(Excursion excursion, string Name_Language, string Money,
            HttpPostedFileBase[] Pictures)
        {
            manager.CreateOrUpdateExcursion(excursion, Name_Language, Money);

            if (Pictures != null && Pictures.Count() > 0 && Pictures[0] != null)
            {
                new PhotoBase().AddPhotosByExecursionID(excursion.Id, Pictures);
            }

            return RedirectToAction("AdminPage", "Excursions");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ImagesID"> "img2 img3 "</param>
        /// <param name="ExcursionID">"1012"</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult JsonDeleteImages(string ImagesID, string ExcursionID)
        {
            bool result = true; string exMessage = "";
            string[] IDs = ImagesID.Trim().Replace("img", "").Split(' ');

            for (int i = 0; i < IDs.Length; i++)
            {
                try
                {
                    result = manager.RemovePhotoByID(Convert.ToInt32(IDs[i]));
                    if (!result) break;
                }
                catch (Exception ex)
                {
                    exMessage = ex.Message;
                    result = false;
                    break;
                }
            }

            if (result)
            {
                return Json(new { OnSuccess = "true" });
            }
            else
            {
                return Json(new { OnSuccess = "false", exMessage = exMessage });
            }

        }
    }
}