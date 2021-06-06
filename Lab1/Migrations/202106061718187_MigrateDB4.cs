namespace Lab1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB4 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Hotels", "Photo");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Hotels", "Photo", c => c.Binary());
        }
    }
}
