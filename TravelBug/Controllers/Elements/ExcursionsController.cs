using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace TravelBug.Controllers.Elements
{
    public class ExcursionsController : BaseController
    {

        public ActionResult GetAllExcursions()
        {
            ViewBag.Excursions = manager.GetExcursion().ToList();
            return View();
        }


        /// <summary>
        /// Получаем экскурсию по айди
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult ExcursionView(int? id)
        {
            if (id == null)
            {
                return View(manager.GetExcursionByID(id ?? 0));
            }
            else
            {
                return View();
            }
        }

        public ActionResult Delete(int excursionID)
        {
            manager.DeleteExcursionByID(excursionID);

            return RedirectToAction("GetAllExcursions");
        }
    }
}