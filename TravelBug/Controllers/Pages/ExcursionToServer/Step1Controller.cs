using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TravelBug.Models.TravelBugModel;

namespace TravelBug.Controllers.Pages.ExcursionToServer
{
    public class Step1Controller : BaseController
    {
        [HttpGet]
        public ActionResult CreateNew()
        {
            ViewBag.TimeCollection = manager.GetTime().ToList();
            return View();
        }

        [HttpPost]
        public ActionResult CreateNew(Excursion excursion)
        {
            int newExcursionID = manager.CreateExcursion(excursion);

            return RedirectToAction("CreateLanguage", "Step2", new { excursionID = newExcursionID });
        }
    }
}