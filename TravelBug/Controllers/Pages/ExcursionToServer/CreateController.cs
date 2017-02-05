using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TravelBug.Controllers.Components;
using TravelBug.Models.TravelBugModel;

namespace TravelBug.Controllers.Pages.ExcursionToServer
{
    public class CreateController : BaseController
    {
        [HttpGet]
        public ActionResult Excursion()
        {
            ViewBag.TimeCollection = manager.GetTime().ToList();
            return View();
        }

        [HttpPost]
        public ActionResult Excursion(Excursion excursion, string Name_Language, string Money,
            HttpPostedFileBase[] Pictures)
        {
            var newExcursion = manager.CreateOrUpdateExcursion(excursion, Name_Language, Money);

            if (Pictures != null && Pictures.Count() > 0 && Pictures[0] != null)
            {
                PhotoBase photo = new PhotoBase();
                photo.AddPhotosByExecursionID(newExcursion.Id, Pictures);
            }

            return RedirectToAction("AdminPage", "Excursion");
        }

    }
}