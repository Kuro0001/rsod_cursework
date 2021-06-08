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
    public class MyIdentityDbContext : IdentityDbContext<IdentityUser>
    {
        public MyIdentityDbContext() : base("IdentityDb") { }
        public static MyIdentityDbContext Create()
        {
            return new MyIdentityDbContext();
        }
    }
}