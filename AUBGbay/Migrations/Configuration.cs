namespace AUBGbay.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using AUBGbay.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<AUBGbay.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "AUBGbay.Models.ApplicationDbContext";
        }

        bool AddUserAndRole(AUBGbay.Models.ApplicationDbContext context)
        {
            IdentityResult ir;
            var rm = new RoleManager<IdentityRole>
                (new RoleStore<IdentityRole>(context));
            ir = rm.Create(new IdentityRole("admin"));
            var um = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(context));
            var user = new ApplicationUser()
            {
                UserName = "admin",
                Email = "iii120@aubg.edu",
                EmailConfirmed = true,
                FirstName = "Admin",    
            };
            ir = um.Create(user, "PassworD1");
            if (ir.Succeeded == false)
                return ir.Succeeded;
            ir = um.AddToRole(user.Id, "admin");
            return ir.Succeeded;
        }

        protected override void Seed(AUBGbay.Models.ApplicationDbContext context)
        {
            AddUserAndRole(context);
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.

            context.Categories.AddOrUpdate(
              p => p.Name,
              new Category { Name = "Electronics" },
              new Category { Name = "Books" },
              new Category { Name = "Clothing, Shoes and Accessories" },
              new Category { Name = "Miscellaneous" }
            );
            
        }
    }
}
