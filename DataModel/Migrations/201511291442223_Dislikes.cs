namespace DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Dislikes : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Likes", "EventModel_Id", "dbo.Events");
            AddColumn("dbo.Likes", "EventModel_Id1", c => c.Int());
            CreateIndex("dbo.Likes", "EventModel_Id1");
            AddForeignKey("dbo.Likes", "EventModel_Id", "dbo.Events", "Id");
            AddForeignKey("dbo.Likes", "EventModel_Id1", "dbo.Events", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Likes", "EventModel_Id1", "dbo.Events");
            DropForeignKey("dbo.Likes", "EventModel_Id", "dbo.Events");
            DropIndex("dbo.Likes", new[] { "EventModel_Id1" });
            DropColumn("dbo.Likes", "EventModel_Id1");
            AddForeignKey("dbo.Likes", "EventModel_Id", "dbo.Events", "Id");
        }
    }
}
