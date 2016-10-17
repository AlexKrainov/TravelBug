using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TravelBug.Controllers.Elements
{
    public class SliderController : BaseController
    {

        public ActionResult SliderPartial(int excursionID)
        {
            ViewData.Model = manager.GetExcursionByID(excursionID).Photo;
            return PartialView("SliderPartial");
        }
    }
}