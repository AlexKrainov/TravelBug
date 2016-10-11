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

        internal Excursion CreateOrUpdateExcursion(Excursion _excursion, string Name_Language, string Money)
        {
            Excursion excursion = db.Excursion.FirstOrDefault(x => x.Id == _excursion.Id);

            if (excursion == null) excursion = new Excursion();

            excursion.Title = _excursion.Title;
            excursion.Description = _excursion.Description;
            excursion.Time = _excursion.Time != null ? _excursion.Time.TrimEnd(' ') : null;
            if (excursion.Id == 0) db.Excursion.Add(excursion);
            db.SaveChanges();


            #region Update language
            string[] languages = Name_Language.Split(' ');
            this.DeleteLanguageByExcursionID(excursion.Id);

            for (int i = 0; i < languages.Count(); i++)
            {
                this.CreateLanguage(languages.ElementAt(i), excursion.Id);
            }
            #endregion

            #region Update cost
            Cost cost = db.Cost.FirstOrDefault(x => x.ExcursionID == excursion.Id);
            if (cost != null)
            {
                cost.Money = Money;
            }
            else
            {
                cost = new Cost();
                cost.ExcursionID = excursion.Id;
                cost.Money = Money;
                db.Cost.Add(cost);
            }
            #endregion

            db.SaveChanges();
            return excursion;
        }

    }
}