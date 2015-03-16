namespace JJTrailer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class departmentmenu : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DepartmentMenus",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        DepartmentID = c.Guid(nullable: false),
                        Order = c.Int(nullable: false),
                        show = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Departments", t => t.DepartmentID, cascadeDelete: true)
                .Index(t => t.DepartmentID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DepartmentMenus", "DepartmentID", "dbo.Departments");
            DropIndex("dbo.DepartmentMenus", new[] { "DepartmentID" });
            DropTable("dbo.DepartmentMenus");
        }
    }
}
