using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using TravelBug.Models.TravelBugModel;

namespace TravelBug.Controllers.Pages
{
    public class HomeController : BaseController
    {
        public ActionResult Home()
        {
            ViewBag.Excursions = manager.GetExcursion().ToList();

            return View();
        }


        #region Tour collection

        

        [HttpGet]
        public JsonResult GetTours()
        {
            //var data = manager.GetExcursionByID();

            //var result = (from m in data
            //              where m.Id != 0
            //              select new
            //              {
            //                  id = m.Id,
            //                  Title = m.Title,

            //                  Time = m.TimeID
            //              });

            //return Json(result.ToArray(), JsonRequestBehavior.AllowGet);
            var excursions = manager.GetExcursions();

            // ??
            //if (excursion.Cost != null && excursion.Cost.Count() == 1)
            //{
            //    excursion.Cost.ElementAt(0).Excursion = null;
            //}

            List<Excursion> ex_s = new List<Excursion>();

            foreach (Excursion excursion in excursions)
            {
                Excursion exc = new Excursion
                {
                    Id = excursion.Id,
                    Title = excursion.Title,
                    Description = excursion.Description,
                    TimeID = excursion.TimeID,
                    //      Language = excursion.Language.Where(x => x.ID != 0)
                    //.Select(x =>
                    //new Language
                    //{
                    //    ID = x.ID,
                    //    Name_Language = x.Name_Language,
                    //    ExcursionID = x.ExcursionID
                    //}).ToArray(),
                    Photo = excursion.Photo.Where(x => x.Delete != true && x.ToMine).Select(x =>
                   new Photo
                   {
                       ID = x.ID,
                       ExcursionID = x.ExcursionID,
                       FileName = x.FileName,
                       ContentType = x.ContentType,
                       ToMine = x.ToMine,
                       Image = x.Image,
                       Delete = x.Delete
                   }).ToArray(),
                    Cost = excursion.Cost
                };

                ex_s.Add(exc);
            }


            //Что бы не было цикличной сериализации
            JsonSerializerSettings settings =
                new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };

            var tmp = JsonConvert.SerializeObject(ex_s, settings);
            return Json(tmp, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetPhotoByTourID(int id)
        {
            var data = manager.GetMainPhotoByExcursionID(id);

            //Что бы не было цикличной сериализации
            JsonSerializerSettings settings =
                new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };


            var tmp = JsonConvert.SerializeObject(data, settings);

            return Json(tmp, JsonRequestBehavior.AllowGet);
        }

        #endregion

    }
}