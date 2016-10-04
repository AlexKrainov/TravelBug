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

        internal void UpdateExcursion(Excursion excursion, string Name_Language, string Money)
        {
            Excursion oldExcursion = db.Excursion.FirstOrDefault(x => x.Id == excursion.Id);

            if (oldExcursion != null)
            {
                oldExcursion.Title = excursion.Title;
                oldExcursion.Description = excursion.Description;
                oldExcursion.Time = excursion.Time.TrimEnd(' ');

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
                #endregion
                db.SaveChanges();
            }
        }

    }
}