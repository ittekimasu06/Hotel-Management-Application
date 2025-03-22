namespace QuanLyKhachSan.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SixthCreate : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Bills", "ID", "dbo.Users");
            DropIndex("dbo.Bills", new[] { "ID" });
            DropColumn("dbo.Bills", "ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Bills", "ID", c => c.Int(nullable: false));
            CreateIndex("dbo.Bills", "ID");
            AddForeignKey("dbo.Bills", "ID", "dbo.Users", "ID", cascadeDelete: true);
        }
    }
}
