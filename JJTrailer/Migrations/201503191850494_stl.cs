namespace JJTrailer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class stl : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.STLfilemanagers",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        filepath = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.STLfilemanagers");
        }
    }
}
