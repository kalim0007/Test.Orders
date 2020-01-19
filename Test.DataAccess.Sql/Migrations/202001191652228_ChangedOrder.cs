namespace Test.DataAccess.Sql.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedOrder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "FileContent", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "FileContent");
        }
    }
}
