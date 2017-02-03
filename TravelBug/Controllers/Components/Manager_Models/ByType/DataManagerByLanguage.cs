using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TravelBug.Models.TravelBugModel;

namespace TravelBug.Models.Manager
{
    public partial class DataManager
    {
        public IQueryable<Language> GetLanguage()
        {
            return db.Language;
        }

        public Language GetLanguageByID(int id)
        {
            return db.Language.FirstOrDefault(x => x.ID == id);
        }

        public IQueryable<Language> GetLanguageByExcursionID(int excursionID)
        {
            return db.Language.Where(x => x.ExcursionID == excursionID);
        }

        internal int CreateLanguage(Language language)
        {
            db.Language.Add(language);
            db.SaveChanges();

            return language.ID;
        }

        internal int CreateLanguage(string Name_language, int excursionID)
        {
            if (string.IsNullOrEmpty(Name_language))
                return -1;
            else
            {
                Language newLanguage = new Language();
                newLanguage.Name_Language = Name_language.Trim();
                newLanguage.ExcursionID = excursionID;

                return CreateLanguage(newLanguage);
            }

        }
        internal void CreateArrayLanguage(string Name_Language, int excursionID)
        {
            if (!string.IsNullOrEmpty(Name_Language))
            {
                string[] languages = Name_Language.Split(' ');
                this.DeleteLanguageByExcursionID(excursionID);

                for (int i = 0; i < languages.Count(); i++)
                {
                    this.CreateLanguage(languages.ElementAt(i), excursionID);
                }
            }
        }

        internal bool DeleteLanguageByExcursionID(int excursionID)
        {
            var lang = db.Language.Where(x => x.ExcursionID == excursionID);

            if (lang != null && lang.Count() != 0)
            {
                try
                {
                    List<Language> languages = lang.ToList();

                    for (int i = 0; i < languages.Count(); i++)
                    {
                        if (this.DeleteLanguageByID(languages.ElementAt(i).ID))
                            continue;
                        else
                            return false;
                    }
                }
                catch (Exception ex)
                {
                    return false;
                    //throw new Exception(ex.Message);
                }
            }
            return true;
        }

        internal bool DeleteLanguageByID(int id)
        {
            try
            {
                Language old = db.Language.FirstOrDefault(x => x.ID == id);
                db.Language.Remove(old);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return true;
        }
    }
}