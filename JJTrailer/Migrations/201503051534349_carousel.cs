namespace JJTrailer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class carousel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Carousels",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Name = c.String(),
                        DateAdded = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.ImageLibs", "Carousel_ID", c => c.Guid());
            CreateIndex("dbo.ImageLibs", "Carousel_ID");
            AddForeignKey("dbo.ImageLibs", "Carousel_ID", "dbo.Carousels", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ImageLibs", "Carousel_ID", "dbo.Carousels");
            DropIndex("dbo.ImageLibs", new[] { "Carousel_ID" });
            DropColumn("dbo.ImageLibs", "Carousel_ID");
            DropTable("dbo.Carousels");
        }
    }
}
