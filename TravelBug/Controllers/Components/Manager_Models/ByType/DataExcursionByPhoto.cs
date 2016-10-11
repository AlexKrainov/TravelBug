﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TravelBug.Models.TravelBugModel;

namespace TravelBug.Models.Manager
{
    public partial class DataManager
    {
        internal int CreatePhoto(Photo photo)
        {
            db.Photo.Add(photo);
            db.SaveChanges();

            return photo.ID;
        }

        internal bool RemovePhotoByID(int id)
        {
            try
            {
                Photo oldPhoto = db.Photo.FirstOrDefault(x => x.ID == id);

                db.Photo.Remove(oldPhoto);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
                return false; // ?
            }

            return true;
        }
    }
}