namespace TravelBug.Models.TravelBugModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Time")]
    public partial class Time
    {
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Option { get; set; }
    }
}
