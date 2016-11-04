using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TravelBug.Controllers.Pages
{
    public class HomeController : BaseController
    {
        public ActionResult MainView()
        {
            ViewBag.Excursions = manager.GetExcursion().ToList();

            return View();
        }
    }
}