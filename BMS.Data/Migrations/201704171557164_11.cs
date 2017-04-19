namespace BMS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _11 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BarberShopInfo",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.LoginHistory",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        LoginTime = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Role",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(unicode: false),
                        CreateOn = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Share",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Content = c.String(unicode: false),
                        UserId = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false, precision: 0),
                        IsDeleted = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserRole",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.User", "PhotoUrl", c => c.String(unicode: false));
            AddColumn("dbo.User", "PresonalInfo", c => c.String(unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.User", "PresonalInfo");
            DropColumn("dbo.User", "PhotoUrl");
            DropTable("dbo.UserRole");
            DropTable("dbo.Share");
            DropTable("dbo.Role");
            DropTable("dbo.LoginHistory");
            DropTable("dbo.BarberShopInfo");
        }
    }
}
