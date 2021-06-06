namespace Lab1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tours", "VouchersCount", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tours", "VouchersCount");
        }
    }
}
