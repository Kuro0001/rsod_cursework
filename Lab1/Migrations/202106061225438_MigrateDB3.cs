namespace Lab1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TourOperators", "Login", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.TourOperators", "Password", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.Clients", "Login", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.Clients", "Password", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.Employees", "Login", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.Employees", "Password", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Categories", "Name", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Categories", "AddedValue", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Categories", "Discount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Tours", "Name", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Tours", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Hotels", "Name", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Hotels", "Address", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Hotels", "Photo", c => c.Binary());
            AlterColumn("dbo.Directions", "Name", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Kinds", "Name", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.TourOperators", "Name", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.TourOperators", "Email", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Vouchers", "Name", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Vouchers", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Clients", "Name", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Clients", "Surname", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Clients", "Patronymic", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Clients", "Email", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Employees", "Name", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Employees", "Surname", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Employees", "Patronymic", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Employees", "Email", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Employees", "Email", c => c.String(nullable: false, maxLength: 45));
            AlterColumn("dbo.Employees", "Patronymic", c => c.String(nullable: false, maxLength: 45));
            AlterColumn("dbo.Employees", "Surname", c => c.String(nullable: false, maxLength: 45));
            AlterColumn("dbo.Employees", "Name", c => c.String(nullable: false, maxLength: 45));
            AlterColumn("dbo.Clients", "Email", c => c.String(nullable: false, maxLength: 45));
            AlterColumn("dbo.Clients", "Patronymic", c => c.String(maxLength: 45));
            AlterColumn("dbo.Clients", "Surname", c => c.String(nullable: false, maxLength: 45));
            AlterColumn("dbo.Clients", "Name", c => c.String(nullable: false, maxLength: 45));
            AlterColumn("dbo.Vouchers", "Price", c => c.Double(nullable: false));
            AlterColumn("dbo.Vouchers", "Name", c => c.String(nullable: false, maxLength: 45));
            AlterColumn("dbo.TourOperators", "Email", c => c.String(nullable: false, maxLength: 45));
            AlterColumn("dbo.TourOperators", "Name", c => c.String(nullable: false, maxLength: 45));
            AlterColumn("dbo.Kinds", "Name", c => c.String(nullable: false, maxLength: 45));
            AlterColumn("dbo.Directions", "Name", c => c.String(nullable: false, maxLength: 45));
            AlterColumn("dbo.Hotels", "Photo", c => c.Binary(nullable: false));
            AlterColumn("dbo.Hotels", "Address", c => c.String(nullable: false, maxLength: 45));
            AlterColumn("dbo.Hotels", "Name", c => c.String(nullable: false, maxLength: 45));
            AlterColumn("dbo.Tours", "Price", c => c.Double(nullable: false));
            AlterColumn("dbo.Tours", "Name", c => c.String(nullable: false, maxLength: 45));
            AlterColumn("dbo.Categories", "Discount", c => c.Double(nullable: false));
            AlterColumn("dbo.Categories", "AddedValue", c => c.Double(nullable: false));
            AlterColumn("dbo.Categories", "Name", c => c.String(nullable: false, maxLength: 45));
            DropColumn("dbo.Employees", "Password");
            DropColumn("dbo.Employees", "Login");
            DropColumn("dbo.Clients", "Password");
            DropColumn("dbo.Clients", "Login");
            DropColumn("dbo.TourOperators", "Password");
            DropColumn("dbo.TourOperators", "Login");
        }
    }
}
