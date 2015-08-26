namespace Insight.Migrations
{
    using Insight.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using Insight.Data;
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
            var roleStore = new RoleStore<IdentityRole>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);
            var roleManager = new RoleManager<IdentityRole>(roleStore);

            if (!context.Users.Any(admin => admin.UserName == "staff@insight.io"))
            {
                var user = new ApplicationUser { UserName = "staff@insight.io", Email = "staff@insight.io" };
                Lecturer _staff = new Lecturer(); BLogic.BusinessLogicHandler _gateWay = new BLogic.BusinessLogicHandler();
                _staff.User_Id = user.Id; _staff.Surname = "Poswa"; _staff.Name = "Gcobani"; _staff.StaffNumber = "210139889";
                if(_gateWay.InsertLecturer(_staff))
                {
                    userManager.Create(user, "password");
                    roleManager.Create(new IdentityRole { Name = "root" });
                    userManager.AddToRole(user.Id, "root");
                }
            }
        }
    }
}
