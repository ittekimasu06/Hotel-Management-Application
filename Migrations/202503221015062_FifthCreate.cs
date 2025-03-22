namespace QuanLyKhachSan.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FifthCreate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Bills", "ID", c => c.Int(nullable: false));
            AddColumn("dbo.Bills", "status", c => c.Int(nullable: false));
            CreateIndex("dbo.Bills", "roomId");
            CreateIndex("dbo.Bills", "ID");
            AddForeignKey("dbo.Bills", "roomId", "dbo.Rooms", "roomId", cascadeDelete: true);
            AddForeignKey("dbo.Bills", "ID", "dbo.Users", "ID", cascadeDelete: true);
            DropColumn("dbo.Bills", "userId");
            DropColumn("dbo.Bills", "total");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Bills", "total", c => c.Single(nullable: false));
            AddColumn("dbo.Bills", "userId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Bills", "ID", "dbo.Users");
            DropForeignKey("dbo.Bills", "roomId", "dbo.Rooms");
            DropIndex("dbo.Bills", new[] { "ID" });
            DropIndex("dbo.Bills", new[] { "roomId" });
            DropColumn("dbo.Bills", "status");
            DropColumn("dbo.Bills", "ID");
        }
    }
}
