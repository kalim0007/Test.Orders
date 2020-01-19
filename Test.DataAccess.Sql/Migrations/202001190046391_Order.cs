namespace Test.DataAccess.Sql.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Order : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "CreatedAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.Orders", "Filename", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "Filename");
            DropColumn("dbo.Orders", "CreatedAt");
        }
    }
}
