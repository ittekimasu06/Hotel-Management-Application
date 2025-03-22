namespace QuanLyKhachSan.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeventhCreate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Bills", "total", c => c.Single(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Bills", "total");
        }
    }
}
