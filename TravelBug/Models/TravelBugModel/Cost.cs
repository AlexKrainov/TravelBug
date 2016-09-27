namespace TravelBug.Models.TravelBugModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Cost")]
    public partial class Cost
    {
        public int ID { get; set; }

        public int? ExcursionID { get; set; }

        [Column("Cost")]
        public int Cost1 { get; set; }

        public virtual Excursion Excursion { get; set; }
    }
}
