using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System.Data.Entity;

namespace Lab1.Models.Security
{
    public class MyIdentityUser : IdentityUser
    {
        public MyIdentityUser()
        {

        }
    }
}