using System.Data.Entity.Migrations;

namespace DatavendingSite.Migrations
{
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LogEntry",
                c => new
                    {
                        LogEntryId = c.Long(nullable: false, identity: true),
                        MachineId = c.Int(nullable: false),
                        EventTime = c.DateTime(nullable: false),
                        ReceivedTime = c.DateTime(),
                        Category = c.Int(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.LogEntryId)
                .ForeignKey("dbo.Machine", t => t.MachineId, cascadeDelete: true)
                .Index(t => t.MachineId);
            
            CreateTable(
                "dbo.Machine",
                c => new
                    {
                        MachineId = c.Int(nullable: false, identity: true),
                        Code = c.String(nullable: false, maxLength: 12),
                        Description = c.String(),
                        ExpiryDate = c.DateTime(nullable: false),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.MachineId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.Code, unique: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Takings",
                c => new
                    {
                        TakingsId = c.Long(nullable: false, identity: true),
                        MachineId = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Currency = c.String(nullable: false, maxLength: 3),
                        SumTotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SumCash = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SumCashless = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SumProduct1 = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SumProduct2 = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SumProduct3 = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SumProduct4 = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SumProduct5 = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SumProduct6 = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SumProduct7 = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SumProduct8 = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SumProduct9 = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SumProduct10 = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SumProduct11 = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SumProduct12 = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.TakingsId)
                .ForeignKey("dbo.Machine", t => t.MachineId, cascadeDelete: true)
                .Index(t => t.MachineId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        IsActive = c.Boolean(nullable: false),
                        Description = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Machine", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Takings", "MachineId", "dbo.Machine");
            DropForeignKey("dbo.LogEntry", "MachineId", "dbo.Machine");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Takings", new[] { "MachineId" });
            DropIndex("dbo.Machine", new[] { "UserId" });
            DropIndex("dbo.Machine", new[] { "Code" });
            DropIndex("dbo.LogEntry", new[] { "MachineId" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Takings");
            DropTable("dbo.Machine");
            DropTable("dbo.LogEntry");
        }
    }
}
