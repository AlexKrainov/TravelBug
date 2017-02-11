using System;
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

        internal Photo GetPhotoByID(int id)
        {
            return db.Photo.FirstOrDefault(x => x.ID == id);

        }

        internal bool UpdatePhotoByID(int id, bool value)
        {
            try
            {
                Photo oldPhoto = db.Photo.FirstOrDefault(x => x.ID == id);
                oldPhoto.Delete = value;

                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
                return false; // ?
            }

            return true;
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

        internal bool DeletePhotoByExcursionID(int excursionID)
        {
            try
            {
                IQueryable<Photo> costs = db.Photo.Where(x => x.ExcursionID == excursionID);
                if (costs != null)
                {
                    db.Photo.RemoveRange(costs);
                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}