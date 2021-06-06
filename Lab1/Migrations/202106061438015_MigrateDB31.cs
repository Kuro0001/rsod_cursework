namespace Lab1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB31 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Clients", "Sex", c => c.String(nullable: false, maxLength: 1));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Clients", "Sex");
        }
    }
}
