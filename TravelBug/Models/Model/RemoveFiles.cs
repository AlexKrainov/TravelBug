using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TravelBug.Models.Model
{
    public class RemoveFiles
    {
        public int ExcursionID { get; set; }
        public int ID { get; set; }
        public bool Hide { get; set; }
        public bool Delete { get; set; }

    }
}