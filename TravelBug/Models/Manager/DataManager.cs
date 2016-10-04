using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TravelBug.Models.TravelBugModel;

namespace TravelBug.Models.Manager
{
    public partial class DataManager
    {
        protected TravelBugModel.TravelBug db;
        public DataManager()
        {
            db = new TravelBugModel.TravelBug();
        }

        /// <summary>
        /// Получаем все Экскурсии
        /// </summary>
        /// <returns></returns>
        public IQueryable<Excursion> GetExcursion()
        {
            return db.Excursion;
        }




        #region Get images
        public IQueryable<Photo> GetPhoto()
        {
            return db.Photo;
        }

        public Photo GetMainPhotoByExcursionID(int excursionID)
        {
            return db.Photo.FirstOrDefault(x => x.ExcursionID == excursionID && x.ToMine);
        } 
        #endregion
    }
}