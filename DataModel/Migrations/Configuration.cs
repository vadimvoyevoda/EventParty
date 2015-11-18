namespace DataModel.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DataModel.EventContext>
    {
        public Configuration()
        {
        }

        protected override void Seed(DataModel.EventContext context)
        {
            using (var ctx = new EventContext())
            {
                ctx.Permissions.Add(new EventPermission { Type = "User"});
                ctx.Ratings.Add(new EventRating { Name = "Low" });

                ctx.SaveChanges();
            }
        }
    }
}
