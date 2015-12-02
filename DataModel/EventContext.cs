using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class EventContext : DbContext
    {
        public EventContext()
            : base("defaultConn4")
        {
            Database.SetInitializer<EventContext>(null);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EventComment>()
                .HasRequired(c => c.Event)
                .WithMany()
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EventLike>()
                .HasRequired(c => c.Event)
                .WithMany()
                .WillCascadeOnDelete(false);
        }

        public DbSet<EventModel> Events { get; set; }
        public DbSet<EventType> EventTypes { get; set; }
        public DbSet<EventPersonCategory> PersonCategories { get; set; }
        public DbSet<EventCustomer> Customers { get; set; }
        public DbSet<EventRating> Ratings { get; set; }
        public DbSet<EventPermission> Permissions { get; set; }
        public DbSet<EventLike> Likes { get; set; }
        public DbSet<EventComment> Comments { get; set; }
        public DbSet<LikeCommentType> LikeCommentTypes { get; set; }
        public DbSet<EventPhoto> Photos { get; set; }
        public DbSet<EventCity> Cities { get; set; }
        public DbSet<EventRegion> Regions { get; set; }
        public DbSet<EventCountry> Countries { get; set; }
    }
}
