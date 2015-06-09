using System.Data.Entity.Migrations;

namespace DatavendingSite.Migrations
{
    public partial class LogDescriptions : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LogDescription",
                c => new
                    {
                        Value = c.Int(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Value);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.LogDescription");
        }
    }
}
