namespace JJTrailer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class categories : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Categories", "Order", c => c.Int(nullable: false,defaultValueSql: "0"));
            AddColumn("dbo.Categories", "show", c => c.Boolean(nullable: false, defaultValue: true));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Categories", "show");
            DropColumn("dbo.Categories", "Order");
        }
    }
}
