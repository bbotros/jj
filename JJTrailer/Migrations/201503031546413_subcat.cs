namespace JJTrailer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class subcat : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Categories", new[] { "CategoryID" });
            AlterColumn("dbo.Categories", "CategoryID", c => c.Guid());
            CreateIndex("dbo.Categories", "CategoryID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Categories", new[] { "CategoryID" });
            AlterColumn("dbo.Categories", "CategoryID", c => c.Guid(nullable: false));
            CreateIndex("dbo.Categories", "CategoryID");
        }
    }
}
