namespace TravelBug.Models.TravelBugModel
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class TravelBug : DbContext
    {
        public TravelBug()
            : base("name=TravelBug1")
        {
        }

        public virtual DbSet<Cost> Cost { get; set; }
        public virtual DbSet<Excursion> Excursion { get; set; }
        public virtual DbSet<Language> Language { get; set; }
        public virtual DbSet<Photo> Photo { get; set; }
        public virtual DbSet<Time> Time { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Excursion>()
                .Property(e => e.Time)
                .IsFixedLength();
        }
    }
}
