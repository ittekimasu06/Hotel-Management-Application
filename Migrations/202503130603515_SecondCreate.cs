namespace QuanLyKhachSan.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SecondCreate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Bills", "phone", c => c.String(nullable: false, maxLength: 12));
            AddColumn("dbo.Bills", "startDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Bills", "endDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Bills", "numberAdult", c => c.Int(nullable: false));
            AddColumn("dbo.Bills", "numberChildren", c => c.Int(nullable: false));
            AddColumn("dbo.Bills", "numberRoom", c => c.Int(nullable: false));
            AddColumn("dbo.Bills", "total", c => c.Single(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Bills", "total");
            DropColumn("dbo.Bills", "numberRoom");
            DropColumn("dbo.Bills", "numberChildren");
            DropColumn("dbo.Bills", "numberAdult");
            DropColumn("dbo.Bills", "endDate");
            DropColumn("dbo.Bills", "startDate");
            DropColumn("dbo.Bills", "phone");
        }
    }
}
