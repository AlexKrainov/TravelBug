using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
        public ActionResult CreateLanguage(Language language)
        {
            manager.CreateLanguage(language);
            return RedirectToAction("CreatePhoto", "Step3", new { excursionID = language.ExcursionID });
        }

    }
}