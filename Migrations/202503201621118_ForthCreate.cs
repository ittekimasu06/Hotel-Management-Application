namespace QuanLyKhachSan.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ForthCreate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Rooms", "roomType", c => c.String(nullable: false));
            DropColumn("dbo.Rooms", "roomTypeId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Rooms", "roomTypeId", c => c.Int(nullable: false));
            DropColumn("dbo.Rooms", "roomType");
        }
    }
}
