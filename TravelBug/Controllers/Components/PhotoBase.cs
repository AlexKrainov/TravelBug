using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using TravelBug.Controllers.Pages.ExcursionToServer;
using TravelBug.Models.Manager;
using TravelBug.Models.TravelBugModel;

namespace TravelBug.Controllers.Components
{
    public class PhotoBase
    {
        private DataManager manager;
        public PhotoBase()
        {
            manager = new DataManager();
        }
        public void AddPhotosByExecursionID(int ExcursionID, HttpPostedFileBase[] Pictures)
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