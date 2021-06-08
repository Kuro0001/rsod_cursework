using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Lab1.Models;
using Lab1.Models.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Lab1
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            MyIdentityDbContext db = new MyIdentityDbContext();
            RoleStore<MyIdentityRole> roleStore = new RoleStore<MyIdentityRole>(db);
            RoleManager<MyIdentityRole> roleManager = new RoleManager<MyIdentityRole>(roleStore);

            if (!roleManager.RoleExists("Employee"))
            {
                MyIdentityRole newRole = new MyIdentityRole("Employee",
               "Администратор обладает полными правами в системе");
                roleManager.Create(newRole);
            }
            if (!roleManager.RoleExists("TourOperator"))
            {
                MyIdentityRole newRole = new MyIdentityRole("TourOperator",
                    "Туроператоры могут добавлять туры и отели");

                roleManager.Create(newRole);
            }
            if (!roleManager.RoleExists("Client"))
            {
                MyIdentityRole newRole = new MyIdentityRole("Client",
                    "Клиент может смотреть туры и свои путевки");

                roleManager.Create(newRole);
            }
        }
    }
}
