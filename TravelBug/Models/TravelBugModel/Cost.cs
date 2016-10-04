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

        [Required]
        [StringLength(50)]
        public string Money { get; set; }

        public virtual Excursion Excursion { get; set; }
    }
}
