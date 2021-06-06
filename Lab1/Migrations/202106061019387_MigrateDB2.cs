namespace Lab1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Tours", "VouchersCount");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tours", "VouchersCount", c => c.Int(nullable: false));
        }
    }
}
