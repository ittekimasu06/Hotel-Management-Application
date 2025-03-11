namespace QuanLyKhachSan.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bills",
                c => new
                    {
                        billId = c.Int(nullable: false, identity: true),
                        roomId = c.Int(nullable: false),
                        userId = c.Int(nullable: false),
                        createdAt = c.DateTime(nullable: false),
                        fullName = c.String(nullable: false, maxLength: 50),
                        email = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.billId);
            
            CreateTable(
                "dbo.Rooms",
                c => new
                    {
                        roomId = c.Int(nullable: false, identity: true),
                        numberPeople = c.Int(nullable: false),
                        numberBed = c.Int(nullable: false),
                        quality = c.Int(nullable: false),
                        bedType = c.String(nullable: false),
                        price = c.Single(nullable: false),
                        roomTypeId = c.Int(nullable: false),
                        roomArea = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.roomId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false, maxLength: 50),
                        PasswordHash = c.String(nullable: false),
                        FullName = c.String(nullable: false, maxLength: 50),
                        Email = c.String(nullable: false, maxLength: 100),
                        Phone = c.String(maxLength: 12),
                        Role = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Users");
            DropTable("dbo.Rooms");
            DropTable("dbo.Bills");
        }
    }
}
