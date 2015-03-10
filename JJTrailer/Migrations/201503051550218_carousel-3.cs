namespace JJTrailer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class carousel3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Carousels", "DateAdded", c => c.DateTime(nullable: false, defaultValueSql: "GETUTCDATE()"));

        }
        
        public override void Down()
        {
        }
    }
}
