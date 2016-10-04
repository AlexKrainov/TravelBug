using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
        public ActionResult Index(Excursion excursion, string Name_Language, string Money)
        {
            manager.UpdateExcursion(excursion, Name_Language, Money);
            

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult DeleteImages(int excursionID)
        {
            ViewBag.LanguageCollecion = manager.GetLanguage().ToList();
            var currentExcursion = manager.GetExcursionByID(excursionID);

            return View(currentExcursion);
        }
    }
}