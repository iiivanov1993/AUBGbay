namespace AUBGbay.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedShortCaption : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Classifieds", "ShortCaption", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Classifieds", "ShortCaption");
        }
    }
}
