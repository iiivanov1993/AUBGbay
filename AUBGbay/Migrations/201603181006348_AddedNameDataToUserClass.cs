namespace AUBGbay.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedNameDataToUserClass : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User", "FirstName", c => c.String());
            AddColumn("dbo.User", "LastName", c => c.String());
            DropColumn("dbo.User", "HomeTown");
        }
        
        public override void Down()
        {
            AddColumn("dbo.User", "HomeTown", c => c.String());
            DropColumn("dbo.User", "LastName");
            DropColumn("dbo.User", "FirstName");
        }
    }
}
