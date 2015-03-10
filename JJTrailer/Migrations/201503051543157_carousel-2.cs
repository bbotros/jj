namespace JJTrailer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class carousel2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Carousels", "DateAdded", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Carousels", "DateAdded", c => c.DateTime(nullable: false));
        }
    }
}
