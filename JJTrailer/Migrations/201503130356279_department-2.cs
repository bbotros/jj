namespace JJTrailer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class department2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Departments", "departmentType", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Departments", "departmentType");
        }
    }
}
