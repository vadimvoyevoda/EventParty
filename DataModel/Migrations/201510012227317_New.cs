namespace DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class New : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        WriteTime = c.DateTime(nullable: false),
                        Text = c.String(nullable: false, maxLength: 256),
                        Customer_Id = c.Int(nullable: false),
                        Event_Id = c.Int(nullable: false),
                        Type_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.Customer_Id, cascadeDelete: true)
                .ForeignKey("dbo.Events", t => t.Event_Id, cascadeDelete: true)
                .ForeignKey("dbo.LikeCommentTypes", t => t.Type_Id, cascadeDelete: true)
                .Index(t => t.Customer_Id)
                .Index(t => t.Event_Id)
                .Index(t => t.Type_Id);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Login = c.String(nullable: false, maxLength: 50),
                        Password = c.String(nullable: false),
                        Salt = c.String(nullable: false, maxLength: 16),
                        Name = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        Mobile = c.String(nullable: false, maxLength: 13),
                        Email = c.String(nullable: false),
                        Birthday = c.DateTime(nullable: false),
                        Country = c.String(),
                        Address = c.String(),
                        IsBan = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        ShowContacts = c.Boolean(nullable: false),
                        Photo = c.String(),
                        EventModel_Id = c.Int(),
                        EventModel_Id1 = c.Int(),
                        EventModel_Id2 = c.Int(),
                        EventModel_Id3 = c.Int(),
                        Permission_Id = c.Int(nullable: false),
                        Rating_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Events", t => t.EventModel_Id)
                .ForeignKey("dbo.Events", t => t.EventModel_Id1)
                .ForeignKey("dbo.Events", t => t.EventModel_Id2)
                .ForeignKey("dbo.Events", t => t.EventModel_Id3)
                .ForeignKey("dbo.Permissions", t => t.Permission_Id, cascadeDelete: true)
                .ForeignKey("dbo.Ratings", t => t.Rating_Id, cascadeDelete: true)
                .Index(t => t.EventModel_Id)
                .Index(t => t.EventModel_Id1)
                .Index(t => t.EventModel_Id2)
                .Index(t => t.EventModel_Id3)
                .Index(t => t.Permission_Id)
                .Index(t => t.Rating_Id);
            
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 100),
                        StartTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(nullable: false),
                        Region = c.String(nullable: false, maxLength: 80),
                        City = c.String(nullable: false, maxLength: 80),
                        Place = c.String(nullable: false, maxLength: 256),
                        Description = c.String(nullable: false),
                        ShortDescription = c.String(nullable: false, maxLength: 100),
                        IsCharitable = c.Boolean(nullable: false),
                        IsPublished = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        Sponsors = c.String(),
                        MainPhoto = c.String(),
                        Cost = c.Double(nullable: false),
                        Type_Id = c.Int(nullable: false),
                        EventCustomer_Id = c.Int(),
                        EventCustomer_Id1 = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Types", t => t.Type_Id, cascadeDelete: true)
                .ForeignKey("dbo.Customers", t => t.EventCustomer_Id)
                .ForeignKey("dbo.Customers", t => t.EventCustomer_Id1)
                .Index(t => t.Type_Id)
                .Index(t => t.EventCustomer_Id)
                .Index(t => t.EventCustomer_Id1);
            
            CreateTable(
                "dbo.Likes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Customer_Id = c.Int(nullable: false),
                        Event_Id = c.Int(nullable: false),
                        Type_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.Customer_Id, cascadeDelete: true)
                .ForeignKey("dbo.Events", t => t.Event_Id, cascadeDelete: true)
                .ForeignKey("dbo.LikeCommentTypes", t => t.Type_Id, cascadeDelete: true)
                .Index(t => t.Customer_Id)
                .Index(t => t.Event_Id)
                .Index(t => t.Type_Id);
            
            CreateTable(
                "dbo.LikeCommentTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PersonCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Category = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Photos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Content = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Types",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Permissions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Ratings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EventPersonCategoryEventModels",
                c => new
                    {
                        EventPersonCategory_Id = c.Int(nullable: false),
                        EventModel_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.EventPersonCategory_Id, t.EventModel_Id })
                .ForeignKey("dbo.PersonCategories", t => t.EventPersonCategory_Id, cascadeDelete: true)
                .ForeignKey("dbo.Events", t => t.EventModel_Id, cascadeDelete: true)
                .Index(t => t.EventPersonCategory_Id)
                .Index(t => t.EventModel_Id);
            
            CreateTable(
                "dbo.EventPhotoEventModels",
                c => new
                    {
                        EventPhoto_Id = c.Int(nullable: false),
                        EventModel_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.EventPhoto_Id, t.EventModel_Id })
                .ForeignKey("dbo.Photos", t => t.EventPhoto_Id, cascadeDelete: true)
                .ForeignKey("dbo.Events", t => t.EventModel_Id, cascadeDelete: true)
                .Index(t => t.EventPhoto_Id)
                .Index(t => t.EventModel_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "Type_Id", "dbo.LikeCommentTypes");
            DropForeignKey("dbo.Comments", "Event_Id", "dbo.Events");
            DropForeignKey("dbo.Comments", "Customer_Id", "dbo.Customers");
            DropForeignKey("dbo.Customers", "Rating_Id", "dbo.Ratings");
            DropForeignKey("dbo.Customers", "Permission_Id", "dbo.Permissions");
            DropForeignKey("dbo.Events", "EventCustomer_Id1", "dbo.Customers");
            DropForeignKey("dbo.Events", "EventCustomer_Id", "dbo.Customers");
            DropForeignKey("dbo.Events", "Type_Id", "dbo.Types");
            DropForeignKey("dbo.EventPhotoEventModels", "EventModel_Id", "dbo.Events");
            DropForeignKey("dbo.EventPhotoEventModels", "EventPhoto_Id", "dbo.Photos");
            DropForeignKey("dbo.EventPersonCategoryEventModels", "EventModel_Id", "dbo.Events");
            DropForeignKey("dbo.EventPersonCategoryEventModels", "EventPersonCategory_Id", "dbo.PersonCategories");
            DropForeignKey("dbo.Customers", "EventModel_Id3", "dbo.Events");
            DropForeignKey("dbo.Customers", "EventModel_Id2", "dbo.Events");
            DropForeignKey("dbo.Customers", "EventModel_Id1", "dbo.Events");
            DropForeignKey("dbo.Customers", "EventModel_Id", "dbo.Events");
            DropForeignKey("dbo.Likes", "Type_Id", "dbo.LikeCommentTypes");
            DropForeignKey("dbo.Likes", "Event_Id", "dbo.Events");
            DropForeignKey("dbo.Likes", "Customer_Id", "dbo.Customers");
            DropIndex("dbo.EventPhotoEventModels", new[] { "EventModel_Id" });
            DropIndex("dbo.EventPhotoEventModels", new[] { "EventPhoto_Id" });
            DropIndex("dbo.EventPersonCategoryEventModels", new[] { "EventModel_Id" });
            DropIndex("dbo.EventPersonCategoryEventModels", new[] { "EventPersonCategory_Id" });
            DropIndex("dbo.Likes", new[] { "Type_Id" });
            DropIndex("dbo.Likes", new[] { "Event_Id" });
            DropIndex("dbo.Likes", new[] { "Customer_Id" });
            DropIndex("dbo.Events", new[] { "EventCustomer_Id1" });
            DropIndex("dbo.Events", new[] { "EventCustomer_Id" });
            DropIndex("dbo.Events", new[] { "Type_Id" });
            DropIndex("dbo.Customers", new[] { "Rating_Id" });
            DropIndex("dbo.Customers", new[] { "Permission_Id" });
            DropIndex("dbo.Customers", new[] { "EventModel_Id3" });
            DropIndex("dbo.Customers", new[] { "EventModel_Id2" });
            DropIndex("dbo.Customers", new[] { "EventModel_Id1" });
            DropIndex("dbo.Customers", new[] { "EventModel_Id" });
            DropIndex("dbo.Comments", new[] { "Type_Id" });
            DropIndex("dbo.Comments", new[] { "Event_Id" });
            DropIndex("dbo.Comments", new[] { "Customer_Id" });
            DropTable("dbo.EventPhotoEventModels");
            DropTable("dbo.EventPersonCategoryEventModels");
            DropTable("dbo.Ratings");
            DropTable("dbo.Permissions");
            DropTable("dbo.Types");
            DropTable("dbo.Photos");
            DropTable("dbo.PersonCategories");
            DropTable("dbo.LikeCommentTypes");
            DropTable("dbo.Likes");
            DropTable("dbo.Events");
            DropTable("dbo.Customers");
            DropTable("dbo.Comments");
        }
    }
}
