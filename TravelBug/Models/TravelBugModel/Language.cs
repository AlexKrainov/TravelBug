namespace TravelBug.Models.TravelBugModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Language")]
    public partial class Language
    {
        public int ID { get; set; }

        public int? ExcursionID { get; set; }

        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        public virtual Excursion Excursion { get; set; }
    }
}
