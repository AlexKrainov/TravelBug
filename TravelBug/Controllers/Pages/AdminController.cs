using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TravelBug.Controllers.Pages
{
    using Components;
    using Excursion = Models.TravelBugModel.Excursion;
    using Language = Models.TravelBugModel.Language;
    using Photo = Models.TravelBugModel.Photo;

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
        public JsonResult OnUpdate(Excursion newExcursion, string Name_Language)
        {
            var result = manager.CreateOrUpdateExcursion(newExcursion, Name_Language, null);

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UploadFiles(int id)
        {
            // Checking no of files injected in Request object  
            if (Request.Files.Count > 0)
            {
                try
                {
                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        //string path = AppDomain.CurrentDomain.BaseDirectory + "Uploads/";  
                        //string filename = Path.GetFileName(Request.Files[i].FileName);  

                        HttpPostedFileBase file = files[i];
                        string fname;

                        // Checking for Internet Explorer  
                        if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                        {
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
                            fname = testfiles[testfiles.Length - 1];
                        }
                        else
                        {
                            fname = file.FileName;
                        }

                        Photo photo = new Photo();
                        photo.FileName = fname;
                        photo.ContentType = file.ContentType;
                        photo.ExcursionID = id;

                        using (var reader = new System.IO.BinaryReader(file.InputStream))
                        {
                            //picture.Picture = reader.ReadBytes(file.ContentLength);
                            photo.Image = ExcursionToServer.CompressionImage.GetCroppedImage(reader.ReadBytes(file.ContentLength),
                                ExcursionToServer.CompressionImage.GetFormatImage(file.ContentType));
                        }
                    }
                    return Json(true);
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("No files selected.");
            }
        }

        [HttpGet]
        public JsonResult getFiles(int id)
        {
            var files = manager.GetPhotoByExcursionID(id).ToArray();

            List<Photo> photos = new List<Photo>();
            for (int i = 0; i < files.Length; i++)
            {
                Photo photo = new Photo
                {
                    ContentType = files[i].ContentType,
                    ID = files[i].ID,
                    Image = files[i].Image,
                    FileName = files[i].FileName,
                    Delete = files[i].Delete
                };
                photos.Add(photo);
            }

            //Что бы не было цикличной сериализации
            JsonSerializerSettings settings =
                new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };

            var tmp = JsonConvert.SerializeObject(photos, settings);

            return Json(tmp, JsonRequestBehavior.AllowGet);
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