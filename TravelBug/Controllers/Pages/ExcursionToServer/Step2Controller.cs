using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using TravelBug.Models.TravelBugModel;

namespace TravelBug.Controllers.Pages.ExcursionToServer
{
    public class Step2Controller : BaseController
    {
        [HttpGet]
        public ActionResult CreateLanguage(int excursionID)
        {
            //ToDo: make //.Select(x => x.Title ).Distinct();
            ViewBag.LanguageCollecion = manager.GetLanguage().ToList();
            ViewBag.ExcursionID = excursionID;


            return View();
        }

        [HttpPost]
        public ActionResult CreateLanguage(int ExcursionID, string Name_Language, string Money)
        {
            #region Update language
            if (!string.IsNullOrEmpty(Name_Language))
            {
                string[] languages = Name_Language.Split(' ');

                for (int i = 0; i < languages.Count(); i++)
                {
                    manager.CreateLanguage(languages.ElementAt(i), ExcursionID);
                }
            }
            #endregion

            manager.CreateCost(new Cost { ExcursionID = ExcursionID, Money = Money });

            return RedirectToAction("CreatePhoto", "Step3", new { excursionID = ExcursionID });
        }

    }
}