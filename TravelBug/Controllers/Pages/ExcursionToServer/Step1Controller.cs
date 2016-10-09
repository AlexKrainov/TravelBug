using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TravelBug.Controllers.Components;
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
        public ActionResult CreateNew(Excursion excursion, string Name_Language, string Money,
            HttpPostedFileBase[] Pictures)
        {
            var newExcursion = manager.CreateOrUpdateExcursion(excursion, Name_Language, Money);

            if (Pictures != null && Pictures.Count() > 0 && Pictures[0] != null)
            {
                PhotoBase photo = new PhotoBase();
                photo.AddPhotosByExecursionID(newExcursion.Id, Pictures);
            }

            return RedirectToAction("GetAllExcursions", "Excursion");
        }
    }
}