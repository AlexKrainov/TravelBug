using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TravelBug.Models.TravelBugModel;

namespace TravelBug.Controllers.Pages.ExcursionToServer
{
    public class DropDownListEditController : BaseController
    {
        public ActionResult Item(string type, int id)
        {
            switch (type)
            {
                case "Language":
                    var language = manager.GetLanguageByExcursionID(id);
                    if (language != null)
                    {
                        List<Language> languages = language.ToList();
                        string hiddenLanguage = "";

                        for (int i = 0; i < languages.Count; i++)
                        {
                            hiddenLanguage += languages[i].Name_Language;
                            hiddenLanguage += " ";
                        }
                        ViewBag.Languages = languages;
                        ViewBag.HiddenLanguage = hiddenLanguage;
                    }

                    ViewBag.Type = "Language";
                    break;
                case "Cost":
                    List<Cost> colCost = manager.GetCost().ToList();
                    Cost cost = manager.GetCostByExcursionID(id);

                    ViewBag.Collection = colCost;

                    ViewBag.Type = "Cost";
                    ViewBag.Value = cost != null ? cost.Money : null;
                    break;
                case "Time":
                    List<Time> colTime = manager.GetTime().ToList();
                    Excursion ex = manager.GetExcursionByID(id);

                    if (ex != null && !string.IsNullOrEmpty(ex.Time))
                    {
                        ViewBag.Value = ex.Time;
                    }
                    else
                    {
                        ViewBag.Value = "";
                    }

                    ViewBag.Collection = colTime;


                    ViewBag.Type = "Time";

                    break;
                default:
                    break;
            }

            return View();
        }
    }
}