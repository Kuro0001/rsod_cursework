using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System.Data.Entity;

namespace Lab1.Models
{
    public class MyIdentityUser : IdentityUser
    {
        public MyIdentityUser()
        {
        }
    }
    public class MyIdentityRole : IdentityRole
    {
        public MyIdentityRole()
        {

        }
        public MyIdentityRole(string roleName, string description) : base(roleName)
        {
            this.Description = description;
        }
        public string Description { get; set; }
    }

    public class MyIdentityDbContext : IdentityDbContext<IdentityUser>
    {
        public MyIdentityDbContext() : base("IdentityDb") { }
        public static MyIdentityDbContext Create()
        {
            return new MyIdentityDbContext();
        }
    }

    public class MyUserManager : UserManager<MyIdentityUser>
    {
        public MyUserManager(IUserStore<MyIdentityUser> store) : base(store) { }

        public static MyUserManager Create(IdentityFactoryOptions<MyUserManager> options, IOwinContext context)
        {
            MyIdentityDbContext db = context.Get<MyIdentityDbContext>();
            MyUserManager manager = new MyUserManager(new UserStore<MyIdentityUser>(db));

            return manager;
        }
    }


    public class AppDbInitializer : CreateDatabaseIfNotExists<MyIdentityDbContext>
    {
        private TouristAgencyDbEntities db = new TouristAgencyDbEntities();
        protected override void Seed(MyIdentityDbContext context)
        {
            var userManager = new MyUserManager(new UserStore<MyIdentityUser>(context));

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            var role1 = new IdentityRole { Name = "Employee" };
            var role2 = new IdentityRole { Name = "TourOperator" };
            var role3 = new IdentityRole { Name = "Client" };

            roleManager.Create(role1);
            roleManager.Create(role2);
            roleManager.Create(role3);

            var admin = new MyIdentityUser { 
                Email = db.Employees.Where(e => e.Login == "admin" ).FirstOrDefault().Email, 
                UserName = db.Employees.Where(e => e.Login == "admin").FirstOrDefault().Email
            };

            
            string passwordManager = "admin";

            var resultManager = userManager.Create(admin, passwordManager);

            if (resultManager.Succeeded)
            {
                userManager.AddToRole(admin.Id, role1.Name);
            }
        }
    }
}