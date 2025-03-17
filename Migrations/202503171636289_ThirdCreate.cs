namespace QuanLyKhachSan.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ThirdCreate : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Bills", "numberRoom");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Bills", "numberRoom", c => c.Int(nullable: false));
        }
    }
}
