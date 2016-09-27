using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TravelBug.Models.TravelBugModel;

namespace TravelBug.Models.Manager
{
    public partial class DataManager
    {
        internal Excursion GetExcursionByID(int id)
        {
            return db.Excursion.FirstOrDefault(x => x.Id == id);
        }

        internal int CreateExcursion(Excursion excursion)
        {
            db.Excursion.Add(excursion);
            db.SaveChanges();

            return excursion.Id;
        }

        internal int CreateLanguage(Language language)
        {
            db.Language.Add(language);
            db.SaveChanges();

            return language.ID;
        }

        internal int CreatePhoto(Photo photo)
        {
            db.Photo.Add(photo);
            db.SaveChanges();

            return photo.ID;
        }
    }
}