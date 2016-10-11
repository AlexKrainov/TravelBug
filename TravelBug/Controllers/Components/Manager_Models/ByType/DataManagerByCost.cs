using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TravelBug.Models.TravelBugModel;

namespace TravelBug.Models.Manager
{
    public partial class DataManager
    {
        public IQueryable<Cost> GetCost()
        {
                return db.Cost;
        }

        public Cost GetCostByID(int id)
        {
            return db.Cost.FirstOrDefault(x => x.ID == id);
        }

        public Cost GetCostByExcursionID(int excursionID)
        {
            return db.Cost.FirstOrDefault(x => x.ExcursionID == excursionID);
        }

        internal int CreateCost(Cost cost)
        {
            if(db.Cost.Any(x => x.ExcursionID == cost.ExcursionID))
            {
                Cost oldCost = db.Cost.First(x => x.ExcursionID == cost.ExcursionID);
                db.Cost.Remove(oldCost);
            }

            db.Cost.Add(cost);
            db.SaveChanges();

            return cost.ID;
        }
    }
}