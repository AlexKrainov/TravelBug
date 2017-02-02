﻿using System;
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

        internal IQueryable<Excursion> GetExcursionByID()
        {
            //ToDo: and Delete != false
            return db.Excursion.Where(x => !string.IsNullOrEmpty(x.Title));
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
            // excursion.Time = _excursion.Time != null ? _excursion.Time.TrimEnd(' ') : null;
            excursion.TimeID = _excursion.TimeID;
            if (excursion.Id == 0) db.Excursion.Add(excursion);
            db.SaveChanges();


            #region Update language
            if (!string.IsNullOrEmpty(Name_Language))
            {
                string[] languages = Name_Language.Split(' ');
                this.DeleteLanguageByExcursionID(excursion.Id);

                for (int i = 0; i < languages.Count(); i++)
                {
                    this.CreateLanguage(languages.ElementAt(i), excursion.Id);
                }
            }
            #endregion

            #region Update cost
            if (!string.IsNullOrEmpty(Money))
            {
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
            }
            #endregion

            db.SaveChanges();
            return excursion;
        }

        internal bool DeleteExcursionByID(int id)
        {
            bool value = true;
            try
            {
                Excursion excur = db.Excursion.FirstOrDefault(x => x.Id == id);
                if (excur != null)
                {
                    value = value && this.DeleteCostByExcursionID(id);
                    value = value && this.DeleteLanguageByExcursionID(id);
                    value = value && this.DeletePhotoByExcursionID(id);

                    if (value)
                    {
                        db.Excursion.Remove(excur);
                        db.SaveChanges();
                    }
                    else
                        return false;
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