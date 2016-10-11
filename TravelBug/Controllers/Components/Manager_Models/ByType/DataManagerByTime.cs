using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TravelBug.Models.TravelBugModel;

namespace TravelBug.Models.Manager
{
    public partial class DataManager
    {
        public IQueryable<Time> GetTime()
        {
                return db.Time;
        }

        public Time GetTimeByID(int id)
        {
            return db.Time.FirstOrDefault(x => x.ID == id);
        }


    }
}