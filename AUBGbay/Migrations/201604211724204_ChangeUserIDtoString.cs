namespace AUBGbay.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeUserIDtoString : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Classifieds", new[] { "User_Id" });
            DropColumn("dbo.Classifieds", "UserId");
            RenameColumn(table: "dbo.Classifieds", name: "User_Id", newName: "UserId");
            AlterColumn("dbo.Classifieds", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Classifieds", "UserId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Classifieds", new[] { "UserId" });
            AlterColumn("dbo.Classifieds", "UserId", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Classifieds", name: "UserId", newName: "User_Id");
            AddColumn("dbo.Classifieds", "UserId", c => c.Int(nullable: false));
            CreateIndex("dbo.Classifieds", "User_Id");
        }
    }
}
