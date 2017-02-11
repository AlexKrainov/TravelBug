using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TravelBug.Controllers;
using TravelBug.Models.Manager;
using TravelBug.Models.TravelBugModel;

namespace TravelBug.Controllers
{
    public class BaseController : Controller
    {
        protected DataManager manager;
        public BaseController()
        {
            manager = new DataManager();
        }

        #region Crate photo
     
        #endregion
    }
}