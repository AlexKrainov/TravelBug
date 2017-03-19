using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TravelBug.Controllers.Partials
{
    public class ShowController : BaseController
    {
        [HttpGet]
        public ActionResult MainImage(int id)
        {

            var data = manager.GetMainPhotoByExcursionID(id);

          //  ViewBag.Tour = data;

            return View(data);
        }
    }
}