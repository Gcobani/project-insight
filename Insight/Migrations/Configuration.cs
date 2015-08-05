namespace Insight.Migrations
{
    using Insight.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Insight.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "Insight.Models.ApplicationDbContext";
        }

        protected override void Seed(Insight.Models.ApplicationDbContext context)
        {
            var userStore = new UserStore<ApplicationUser>(context);
            var roleStore = new RoleStore<IdentityRole>(context);//
            var userManager = new UserManager<ApplicationUser>(userStore);
            var roleManager = new RoleManager<IdentityRole>(roleStore);

            var user = new ApplicationUser { UserName = "admin@insight.io", Email = "admin@insight.io" };
            userManager.Create(user, "password");
            roleManager.Create(new IdentityRole { Name = "root" });
            userManager.AddToRole(user.Id, "root");
        }
    }
}
