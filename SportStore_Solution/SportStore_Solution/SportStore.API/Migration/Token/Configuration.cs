namespace SportStore.API.Migration.Token
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using SportStore.API.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SportStore.API.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Migration\Token";
        }

        protected override void Seed(SportStore.API.Models.ApplicationDbContext context)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            if (!context.Users.Any(u => u.UserName == "admin@techshop.com"))
            {
                var u = new ApplicationUser { UserName = "admin@techshop.com" };
                var result = userManager.Create(u, "@techOpen123");
            }
        }
    }
}
