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


        protected Excursion GetExcursionWithoutEndlessCycle(Excursion excursion)
        {
            return new Excursion
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
                   Delete = x.Delete
               }).ToArray(),
                Cost = excursion.Cost
            };
        }


    }
}