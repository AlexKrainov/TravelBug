using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TravelBug.Controllers.Components;
using TravelBug.Models.TravelBugModel;

namespace TravelBug.Controllers.Pages.ExcursionToServer
{
    public class Step3Controller : BaseController
    {
        [HttpGet]
        public ActionResult CreatePhoto(int excursionID)
        {
            ViewBag.ExcursionID = excursionID;
            return View();
        }

        [HttpPost]
        public ActionResult CreatePhoto(int ExcursionID, HttpPostedFileBase[] Pictures)
        {
            if (Pictures != null && Pictures.Count() > 0 && Pictures[0] != null)
            {
                PhotoBase photo = new PhotoBase();
                photo.AddPhotosByExecursionID(ExcursionID, Pictures);
            }
            ViewBag.ExcursionID = ExcursionID;
            return View();
        }

     
    }
}