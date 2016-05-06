namespace AUBGbay.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedProfileDataToUserClass : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User", "HomeTown", c => c.String());
            AddColumn("dbo.User", "BirthDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.User", "BirthDate");
            DropColumn("dbo.User", "HomeTown");
        }
    }
}
