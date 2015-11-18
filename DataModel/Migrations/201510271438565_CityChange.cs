namespace DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CityChange : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.EventModelEventCities", "EventModel_Id", "dbo.Events");
            DropForeignKey("dbo.EventModelEventCities", "EventCity_Id", "dbo.Cities");
            DropForeignKey("dbo.EventPersonCategoryEventModels", "EventPersonCategory_Id", "dbo.PersonCategories");
            DropForeignKey("dbo.EventPersonCategoryEventModels", "EventModel_Id", "dbo.Events");
            DropIndex("dbo.EventModelEventCities", new[] { "EventModel_Id" });
            DropIndex("dbo.EventModelEventCities", new[] { "EventCity_Id" });
            DropIndex("dbo.EventPersonCategoryEventModels", new[] { "EventPersonCategory_Id" });
            DropIndex("dbo.EventPersonCategoryEventModels", new[] { "EventModel_Id" });
            AddColumn("dbo.Events", "City_Id", c => c.Int(nullable: false));
            AddColumn("dbo.Events", "PersonsCategory_Id", c => c.Int());
            CreateIndex("dbo.Events", "City_Id");
            CreateIndex("dbo.Events", "PersonsCategory_Id");
            AddForeignKey("dbo.Events", "City_Id", "dbo.Cities", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Events", "PersonsCategory_Id", "dbo.PersonCategories", "Id");
            DropTable("dbo.EventModelEventCities");
            DropTable("dbo.EventPersonCategoryEventModels");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.EventPersonCategoryEventModels",
                c => new
                    {
                        EventPersonCategory_Id = c.Int(nullable: false),
                        EventModel_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.EventPersonCategory_Id, t.EventModel_Id });
            
            CreateTable(
                "dbo.EventModelEventCities",
                c => new
                    {
                        EventModel_Id = c.Int(nullable: false),
                        EventCity_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.EventModel_Id, t.EventCity_Id });
            
            DropForeignKey("dbo.Events", "PersonsCategory_Id", "dbo.PersonCategories");
            DropForeignKey("dbo.Events", "City_Id", "dbo.Cities");
            DropIndex("dbo.Events", new[] { "PersonsCategory_Id" });
            DropIndex("dbo.Events", new[] { "City_Id" });
            DropColumn("dbo.Events", "PersonsCategory_Id");
            DropColumn("dbo.Events", "City_Id");
            CreateIndex("dbo.EventPersonCategoryEventModels", "EventModel_Id");
            CreateIndex("dbo.EventPersonCategoryEventModels", "EventPersonCategory_Id");
            CreateIndex("dbo.EventModelEventCities", "EventCity_Id");
            CreateIndex("dbo.EventModelEventCities", "EventModel_Id");
            AddForeignKey("dbo.EventPersonCategoryEventModels", "EventModel_Id", "dbo.Events", "Id", cascadeDelete: true);
            AddForeignKey("dbo.EventPersonCategoryEventModels", "EventPersonCategory_Id", "dbo.PersonCategories", "Id", cascadeDelete: true);
            AddForeignKey("dbo.EventModelEventCities", "EventCity_Id", "dbo.Cities", "Id", cascadeDelete: true);
            AddForeignKey("dbo.EventModelEventCities", "EventModel_Id", "dbo.Events", "Id", cascadeDelete: true);
        }
    }
}
