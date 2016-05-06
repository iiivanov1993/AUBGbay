namespace AUBGbay.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedTableName1 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Roles", newName: "Role");
            RenameColumn(table: "dbo.Role", name: "RolesId", newName: "RoleId");
        }
        
        public override void Down()
        {
            RenameColumn(table: "dbo.Role", name: "RoleId", newName: "RolesId");
            RenameTable(name: "dbo.Role", newName: "Roles");
        }
    }
}
