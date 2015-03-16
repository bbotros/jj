namespace JJTrailer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class department : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Categories", "DepartmentID", c => c.Guid(nullable: false));
            CreateIndex("dbo.Categories", "DepartmentID");
            AddForeignKey("dbo.Categories", "DepartmentID", "dbo.Departments", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Categories", "DepartmentID", "dbo.Departments");
            DropIndex("dbo.Categories", new[] { "DepartmentID" });
            DropColumn("dbo.Categories", "DepartmentID");
            DropTable("dbo.Departments");
        }
    }
}
