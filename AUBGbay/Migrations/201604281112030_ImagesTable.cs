namespace AUBGbay.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ImagesTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Images", "ImageName", c => c.String());
            AddColumn("dbo.Images", "ImageContent", c => c.Binary());
            DropColumn("dbo.Images", "ImagePath");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Images", "ImagePath", c => c.String());
            DropColumn("dbo.Images", "ImageContent");
            DropColumn("dbo.Images", "ImageName");
        }
    }
}
