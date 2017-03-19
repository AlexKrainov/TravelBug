using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TravelBug.Models.TravelBugModel;

namespace TravelBug.Controllers.Pages
{
    public class ExcursionController : BaseController
    {
        [HttpGet]
        public ActionResult View(int id)
        {
            ViewBag.ExcursionID = id;
            return View();
        }

        [HttpGet]
        public JsonResult GetTourByID(int id)
        {
            var excursion = manager.GetExcursionByID(id);

            Time time = excursion.TimeID != null ? manager.GetTimeByID(excursion.TimeID ?? 0) : null;

            var exc = new 
            {
                Id = excursion.Id,
                Title = excursion.Title,
                Description = excursion.Description,
                Time = time,
                Language = excursion.Language.Where(x => x.ID != 0)
                .Select(x =>
                new Language
                {
                    ID = x.ID,
                    Name_Language = x.Name_Language,
                    ExcursionID = x.ExcursionID
                }).ToArray(),
                Photo = excursion.Photo.Where(x => x.Delete != true).Select(x =>
               new Photo
               {
                   ID = x.ID,
                   ExcursionID = x.ExcursionID,
                   FileName = x.FileName,
                   ContentType = x.ContentType,
                   ToMine = x.ToMine,
                   Image = x.Image,
                   Delete = false // Тут delete играет роль "отображение на стороне клиента эту картинку или нет "
               }).ToArray(),
                Cost = new Cost { Money = excursion.Cost.FirstOrDefault().Money }
            };

            if (exc != null && exc.Photo != null) // && exc.Photo.Count != 0)
            {
                var tmpPhoto = exc.Photo.ElementAt(0);
                tmpPhoto.Delete = true;
            }


            JsonSerializerSettings settings =
              new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };

            var tmp = JsonConvert.SerializeObject(exc, settings);

            return Json(tmp, JsonRequestBehavior.AllowGet);
        }
    }
}