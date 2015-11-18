namespace DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IsFreeEntrance : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Events", "isFreeEntrance", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Events", "isFreeEntrance");
        }
    }
}
