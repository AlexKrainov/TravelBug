using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TravelBug.Controllers.Pages
{
    public class AdminController : BaseController
    {
        // GET: Admin
        public ActionResult Index()
        {

            return View();
        }


        [HttpGet]
        public JsonResult GetDataForTable()
        {

            var data = manager.GetExcursionByID();

            var result = (from m in data
                          where m.Id != 0
                          select new
                          {
                              id = m.Id,
                              Title = m.Title,
                              Description = m.Description,
                              Time = m.Time
                          });



            return Json(result.ToArray(), JsonRequestBehavior.AllowGet);
        }
    }
}