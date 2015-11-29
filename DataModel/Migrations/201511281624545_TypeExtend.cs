namespace DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TypeExtend : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Types", "Color", c => c.String());
            AddColumn("dbo.Types", "Image", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Types", "Image");
            DropColumn("dbo.Types", "Color");
        }
    }
}
