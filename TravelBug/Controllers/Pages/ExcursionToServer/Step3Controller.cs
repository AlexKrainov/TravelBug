using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TravelBug.Models.TravelBugModel;

namespace TravelBug.Controllers.Pages.ExcursionToServer
{
    public class Step3Controller : BaseController
    {
        [HttpGet]
        public ActionResult CreatePhoto(int excursionID)
        {
            ViewBag.ExcursionID = excursionID;
            return View();
        }


        [HttpPost]
        public ActionResult CreatePhoto(int ExcursionID, HttpPostedFileBase[] Pictures)
        {
            if (Pictures.Count() > 0 && Pictures[0] != null)
            {
                GetPictureForTour(ExcursionID, Pictures);
            }
            ViewBag.ExcursionID = ExcursionID;
            return View();
        }

        private void GetPictureForTour(int ExcursionID, HttpPostedFileBase[] Pictures)
        {   
            if (Pictures.Count() > 0 && Pictures[0] != null)
            {
                for (int i = 0; i < Pictures.Count(); i++)
                {

                    Photo photo = new Photo();
                    photo.FileName = Pictures[i].FileName;
                    photo.ContentType = Pictures[i].ContentType;
                    photo.ExcursionID = ExcursionID;

                    using (var reader = new BinaryReader(Pictures[i].InputStream))
                    {
                        //picture.Picture = reader.ReadBytes(Pictures[i].ContentLength);
                        photo.Image = CompressionImage.GetCroppedImage(reader.ReadBytes(Pictures[i].ContentLength), CompressionImage.GetFormatImage(Pictures[i].ContentType));
                    }

                    manager.CreatePhoto(photo);
                }
            }
        }
    }
}