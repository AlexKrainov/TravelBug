using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TravelBug.Models.Manager;

namespace TravelBug.Controllers
{
    public class BaseController : Controller
    {
        protected DataManager manager;
        public BaseController()
        {
            manager = new DataManager();
        }
    }
}