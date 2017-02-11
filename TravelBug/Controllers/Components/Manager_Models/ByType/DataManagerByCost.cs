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
            if (db.Cost.Any(x => x.ExcursionID == cost.ExcursionID))
            {
                Cost oldCost = db.Cost.First(x => x.ExcursionID == cost.ExcursionID);
                db.Cost.Remove(oldCost);
            }

            db.Cost.Add(cost);
            db.SaveChanges();

            return cost.ID;
        }


        internal void CreateOrUpdateCosts(ICollection<Cost> cost, int excursionID)
        {
            this.DeleteCostByExcursionID(excursionID);
            if (cost != null && cost.Count == 1 && cost.ElementAt(0).Money != null)
            {
                var c = cost.ElementAt(0);
                c.ExcursionID = excursionID;
                db.Cost.Add(c);
                db.SaveChanges();
            }
        }

        internal bool DeleteCostByExcursionID(int excursionID)
        {
            try
            {
                IQueryable<Cost> costs = db.Cost.Where(x => x.ExcursionID == excursionID);
                if (costs != null)
                {
                    db.Cost.RemoveRange(costs);
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