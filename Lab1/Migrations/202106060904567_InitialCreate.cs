namespace Lab1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 45),
                        AddedValue = c.Double(nullable: false),
                        Discount = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            
            
            CreateTable(
                "dbo.Hotels",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 45),
                        Address = c.String(nullable: false, maxLength: 45),
                        Photo = c.Binary(nullable: false),
                        DirectionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Directions", t => t.DirectionId, cascadeDelete: true)
                .Index(t => t.DirectionId);
            
            CreateTable(
                "dbo.Directions",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 45),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Kinds",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 45),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.TourOperators",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 45),
                        Phone = c.String(nullable: false, maxLength: 11),
                        UniqCompanyNumber = c.String(nullable: false, maxLength: 13),
                        Email = c.String(nullable: false, maxLength: 45),
                    })
                .PrimaryKey(t => t.ID);
            
            
            
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Pasport = c.String(nullable: false, maxLength: 10),
                        Name = c.String(nullable: false, maxLength: 45),
                        Surname = c.String(nullable: false, maxLength: 45),
                        Patronymic = c.String(maxLength: 45),
                        BirthDate = c.DateTime(nullable: false),
                        Phone = c.String(nullable: false, maxLength: 11),
                        Email = c.String(nullable: false, maxLength: 45),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 45),
                        Surname = c.String(nullable: false, maxLength: 45),
                        Patronymic = c.String(nullable: false, maxLength: 45),
                        Email = c.String(nullable: false, maxLength: 45),
                    })
                .PrimaryKey(t => t.ID);


            CreateTable(
                "dbo.Tours",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 45),
                    OffersAll = c.Int(nullable: false),
                    Price = c.Double(nullable: false),
                    StartDate = c.DateTime(nullable: false),
                    DayCount = c.Int(nullable: false),
                    TourOperatorId = c.Int(nullable: false),
                    KindId = c.Int(nullable: false),
                    CategoryId = c.Int(nullable: false),
                    HotelId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.Hotels", t => t.HotelId, cascadeDelete: true)
                .ForeignKey("dbo.Kinds", t => t.KindId, cascadeDelete: true)
                .ForeignKey("dbo.TourOperators", t => t.TourOperatorId, cascadeDelete: true)
                .Index(t => t.TourOperatorId)
                .Index(t => t.KindId)
                .Index(t => t.CategoryId)
                .Index(t => t.HotelId);


            CreateTable(
                "dbo.Vouchers",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 45),
                    Date = c.DateTime(nullable: false),
                    Price = c.Double(nullable: false),
                    EmployeeId = c.Int(nullable: false),
                    TourId = c.Int(nullable: false),
                    ClientId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Clients", t => t.ClientId, cascadeDelete: true)
                .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: true)
                .ForeignKey("dbo.Tours", t => t.TourId, cascadeDelete: true)
                .Index(t => t.EmployeeId)
                .Index(t => t.TourId)
                .Index(t => t.ClientId);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Vouchers", "TourId", "dbo.Tours");
            DropForeignKey("dbo.Vouchers", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.Vouchers", "ClientId", "dbo.Clients");
            DropForeignKey("dbo.Tours", "TourOperatorId", "dbo.TourOperators");
            DropForeignKey("dbo.Tours", "KindId", "dbo.Kinds");
            DropForeignKey("dbo.Tours", "HotelId", "dbo.Hotels");
            DropForeignKey("dbo.Hotels", "DirectionId", "dbo.Directions");
            DropForeignKey("dbo.Tours", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Vouchers", new[] { "ClientId" });
            DropIndex("dbo.Vouchers", new[] { "TourId" });
            DropIndex("dbo.Vouchers", new[] { "EmployeeId" });
            DropIndex("dbo.Hotels", new[] { "DirectionId" });
            DropIndex("dbo.Tours", new[] { "HotelId" });
            DropIndex("dbo.Tours", new[] { "CategoryId" });
            DropIndex("dbo.Tours", new[] { "KindId" });
            DropIndex("dbo.Tours", new[] { "TourOperatorId" });
            DropTable("dbo.Employees");
            DropTable("dbo.Clients");
            DropTable("dbo.Vouchers");
            DropTable("dbo.TourOperators");
            DropTable("dbo.Kinds");
            DropTable("dbo.Directions");
            DropTable("dbo.Hotels");
            DropTable("dbo.Tours");
            DropTable("dbo.Categories");
        }
    }
}
