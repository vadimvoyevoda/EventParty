namespace DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Enter : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Events", "Enter", c => c.String());
            DropColumn("dbo.Events", "Cost");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Events", "Cost", c => c.Double(nullable: false));
            DropColumn("dbo.Events", "Enter");
        }
    }
}
