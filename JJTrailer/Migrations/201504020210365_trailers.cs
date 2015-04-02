namespace JJTrailer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class trailers : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Trailers",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        TrailerName = c.String(),
                        CategoryID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Categories", t => t.CategoryID, cascadeDelete: true)
                .Index(t => t.CategoryID);
            
            CreateTable(
                "dbo.Options",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Name = c.String(),
                        Trailer_ID = c.Guid(),
                        CustomTrailer_ID = c.Guid(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Trailers", t => t.Trailer_ID)
                .ForeignKey("dbo.CustomTrailers", t => t.CustomTrailer_ID)
                .Index(t => t.Trailer_ID)
                .Index(t => t.CustomTrailer_ID);
            
            CreateTable(
                "dbo.Specifications",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Name = c.String(),
                        Trailer_ID = c.Guid(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Trailers", t => t.Trailer_ID)
                .Index(t => t.Trailer_ID);
            
            CreateTable(
                "dbo.CustomTrailers",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Name = c.String(),
                        TrailerID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Trailers", t => t.TrailerID, cascadeDelete: true)
                .Index(t => t.TrailerID);
            
            AddColumn("dbo.ImageLibs", "Trailer_ID", c => c.Guid());
            AddColumn("dbo.ImageLibs", "CustomTrailer_ID", c => c.Guid());
            CreateIndex("dbo.ImageLibs", "Trailer_ID");
            CreateIndex("dbo.ImageLibs", "CustomTrailer_ID");
            AddForeignKey("dbo.ImageLibs", "Trailer_ID", "dbo.Trailers", "ID");
            AddForeignKey("dbo.ImageLibs", "CustomTrailer_ID", "dbo.CustomTrailers", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CustomTrailers", "TrailerID", "dbo.Trailers");
            DropForeignKey("dbo.Options", "CustomTrailer_ID", "dbo.CustomTrailers");
            DropForeignKey("dbo.ImageLibs", "CustomTrailer_ID", "dbo.CustomTrailers");
            DropForeignKey("dbo.Trailers", "CategoryID", "dbo.Categories");
            DropForeignKey("dbo.Specifications", "Trailer_ID", "dbo.Trailers");
            DropForeignKey("dbo.Options", "Trailer_ID", "dbo.Trailers");
            DropForeignKey("dbo.ImageLibs", "Trailer_ID", "dbo.Trailers");
            DropIndex("dbo.CustomTrailers", new[] { "TrailerID" });
            DropIndex("dbo.Specifications", new[] { "Trailer_ID" });
            DropIndex("dbo.Options", new[] { "CustomTrailer_ID" });
            DropIndex("dbo.Options", new[] { "Trailer_ID" });
            DropIndex("dbo.Trailers", new[] { "CategoryID" });
            DropIndex("dbo.ImageLibs", new[] { "CustomTrailer_ID" });
            DropIndex("dbo.ImageLibs", new[] { "Trailer_ID" });
            DropColumn("dbo.ImageLibs", "CustomTrailer_ID");
            DropColumn("dbo.ImageLibs", "Trailer_ID");
            DropTable("dbo.CustomTrailers");
            DropTable("dbo.Specifications");
            DropTable("dbo.Options");
            DropTable("dbo.Trailers");
        }
    }
}
