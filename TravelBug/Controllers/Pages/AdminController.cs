using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TravelBug.Controllers.Pages
{
    using Excursion = Models.TravelBugModel.Excursion;
    using Language = Models.TravelBugModel.Language;

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

        [HttpGet]
        public JsonResult OnCreate()
        {
            var excursion = manager.GetExcursionByID(
                manager.CreateExcursion(new Models.TravelBugModel.Excursion())
                );

            var result = new
            {
                id = excursion.Id,
                Title = excursion.Title,
                Description = excursion.Description,
                TimeID = excursion.TimeID
            };

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newExcursion"></param>
        /// <param name="Name_Language"> в формате: 'Russian France '</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult OnUpdate(Excursion newExcursion, string Name_Language, HttpPostedFileBase[] Pictures)
        {
            // var newExcursion = JsonConvert.DeserializeObject<Models.TravelBugModel.Excursion>(data);
            var result = manager.CreateOrUpdateExcursion(newExcursion, Name_Language, null);

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult OnEdit(int id)
        {
            var excursion = manager.GetExcursionByID(id);
            Excursion exc = new Excursion
            {
                Id = excursion.Id,
                Title = excursion.Title,
                Description = excursion.Description,
                TimeID = excursion.TimeID,
                Language = excursion.Language.Where(x => x.ID != 0)
                .Select(x =>
                new Language
                {
                    ID = x.ID,
                    Name_Language = x.Name_Language,
                    ExcursionID = x.ExcursionID
                }).ToArray()
            };

            //Что бы не было цикличной сериализации
            JsonSerializerSettings settings =
                new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };

            var tmp = JsonConvert.SerializeObject(exc, settings);
            return Json(tmp, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult OnDelete(int id)
        {

            return Json(null, JsonRequestBehavior.AllowGet);
        }


        #region Get collection
        public JsonResult GetTimes()
        {
            var times = manager.GetTime().ToArray();

            var jsonTimes = JsonConvert.SerializeObject(times);

            return Json(jsonTimes, JsonRequestBehavior.AllowGet);
        }


        #endregion
    }
}