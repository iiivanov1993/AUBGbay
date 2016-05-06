namespace AUBGbay.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedNewTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.CategoryID);
            
            CreateTable(
                "dbo.Classifieds",
                c => new
                    {
                        ClassifiedID = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        CategoryID = c.Int(nullable: false),
                        Title = c.String(),
                        Description = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ClassifiedID)
                .ForeignKey("dbo.Categories", t => t.CategoryID, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.User_Id)
                .Index(t => t.CategoryID)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        ImageID = c.Int(nullable: false, identity: true),
                        ClassifiedID = c.Int(nullable: false),
                        ImagePath = c.String(),
                    })
                .PrimaryKey(t => t.ImageID)
                .ForeignKey("dbo.Classifieds", t => t.ClassifiedID, cascadeDelete: true)
                .Index(t => t.ClassifiedID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Classifieds", "User_Id", "dbo.User");
            DropForeignKey("dbo.Images", "ClassifiedID", "dbo.Classifieds");
            DropForeignKey("dbo.Classifieds", "CategoryID", "dbo.Categories");
            DropIndex("dbo.Images", new[] { "ClassifiedID" });
            DropIndex("dbo.Classifieds", new[] { "User_Id" });
            DropIndex("dbo.Classifieds", new[] { "CategoryID" });
            DropTable("dbo.Images");
            DropTable("dbo.Classifieds");
            DropTable("dbo.Categories");
        }
    }
}
