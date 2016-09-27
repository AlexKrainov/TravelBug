using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TravelBug.Models.TravelBugModel;

namespace TravelBug.Controllers.Pages.ExcursionToServer
{
    public class ShowImageController : BaseController
    {
        public ActionResult GetUrlFromPictureOfExcursion(int excursionID)
        {
            Photo picture = manager.GetMainPhotoByExcursionID(excursionID);


            if (picture != null)
                return File(picture.Image, picture.ContentType);

            return File("pic01.jpg", "image/jpeg");
        }
    }
}