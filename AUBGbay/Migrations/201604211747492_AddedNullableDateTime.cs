namespace AUBGbay.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedNullableDateTime : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Classifieds", "DateCreated", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Classifieds", "DateCreated", c => c.DateTime(nullable: false));
        }
    }
}
