using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System.Data.Entity;
using Lab1.Models.Security;

[assembly: OwinStartup(typeof(Lab1.App_Start.Startup))]

namespace Lab1.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Дополнительные сведения о настройке приложения см. на странице https://go.microsoft.com/fwlink/?LinkID=316888
            CookieAuthenticationOptions options = new CookieAuthenticationOptions();
            options.AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie;
            options.LoginPath = new PathString("/account/login");
            app.UseCookieAuthentication(options);
        }
    }
}
