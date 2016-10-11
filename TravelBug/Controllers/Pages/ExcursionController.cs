using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TravelBug.Controllers.Pages
{
    public class ExcursionController : BaseController
    {
        [HttpGet]
        public ActionResult View(int excursionID)
        {


            return View(manager.GetExcursionByID(excursionID));
        }
    }
}