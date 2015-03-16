namespace JJTrailer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class departmentmenu2 : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.DepartmentMenus", "Order", unique: true, name: "Order");
        }
        
        public override void Down()
        {
            DropIndex("dbo.DepartmentMenus", "Order");
        }
    }
}
