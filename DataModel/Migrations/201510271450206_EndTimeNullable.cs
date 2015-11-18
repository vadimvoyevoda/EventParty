namespace DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EndTimeNullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Events", "EndTime", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Events", "EndTime", c => c.DateTime(nullable: false));
        }
    }
}
