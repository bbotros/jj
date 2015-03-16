namespace JJTrailer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class depmenuimage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DepartmentMenus", "ImagePath", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.DepartmentMenus", "ImagePath");
        }
    }
}
