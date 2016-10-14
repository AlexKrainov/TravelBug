using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TravelBug.Controllers.Elements
{
    public class SliderController : Controller
    {

        public ActionResult View()
        {
            return PartialView("View");
        }
    }
}