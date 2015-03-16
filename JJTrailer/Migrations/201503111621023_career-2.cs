namespace JJTrailer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class career2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        StreetNumber = c.Int(nullable: false),
                        StreetName = c.String(),
                        UnitNumber = c.String(),
                        CityName = c.String(),
                        ProvinceName = c.String(),
                        PostalCode = c.String(),
                        Country = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Careers",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        JobTitle = c.String(),
                        JobDescription = c.String(),
                        JobLocatoinID = c.Guid(nullable: false),
                        JobCategoryID = c.Guid(nullable: false),
                        DatePosted = c.DateTime(nullable: false),
                        StillAvailable = c.Boolean(nullable: false),
                        employmentType = c.Int(nullable: false),
                        JobQualification = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.JobCategories", t => t.JobCategoryID, cascadeDelete: true)
                .ForeignKey("dbo.JobLocations", t => t.JobLocatoinID, cascadeDelete: true)
                .Index(t => t.JobLocatoinID)
                .Index(t => t.JobCategoryID);
            
            CreateTable(
                "dbo.JobCategories",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        JobCategoryName = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.JobLocations",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        AddressID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Addresses", t => t.AddressID, cascadeDelete: true)
                .Index(t => t.AddressID);
            
            CreateTable(
                "dbo.Resumes",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        FilePath = c.String(),
                        DateUploaded = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Careers", "JobLocatoinID", "dbo.JobLocations");
            DropForeignKey("dbo.JobLocations", "AddressID", "dbo.Addresses");
            DropForeignKey("dbo.Careers", "JobCategoryID", "dbo.JobCategories");
            DropIndex("dbo.JobLocations", new[] { "AddressID" });
            DropIndex("dbo.Careers", new[] { "JobCategoryID" });
            DropIndex("dbo.Careers", new[] { "JobLocatoinID" });
            DropTable("dbo.Resumes");
            DropTable("dbo.JobLocations");
            DropTable("dbo.JobCategories");
            DropTable("dbo.Careers");
            DropTable("dbo.Addresses");
        }
    }
}
