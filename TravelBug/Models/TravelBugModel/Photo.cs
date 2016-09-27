namespace TravelBug.Models.TravelBugModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Photo")]
    public partial class Photo
    {
        public int ID { get; set; }

        public int? ExcursionID { get; set; }

        public byte[] Image { get; set; }

        public bool ToMine { get; set; }

        public bool Delete { get; set; }

        public string FileName { get; set; }

        [StringLength(50)]
        public string ContentType { get; set; }

        public virtual Excursion Excursion { get; set; }
    }
}
