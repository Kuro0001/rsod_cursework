using Lab1.Models;
using Lab1.Models.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Lab1.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<MyIdentityUser> userManager;
        private RoleManager<MyIdentityRole> roleManager;

        public AccountController()
        {
            MyIdentityDbContext db = new MyIdentityDbContext();
            UserStore<MyIdentityUser> userStore = new UserStore<MyIdentityUser>(db);
            userManager = new UserManager<MyIdentityUser>(userStore);
            RoleStore<MyIdentityRole> roleStore = new RoleStore<MyIdentityRole>(db);
            roleManager = new RoleManager<MyIdentityRole>(roleStore);
        }

        public ActionResult Register()
        {
            return View();
        }

        private void AccountValidationEdit(Register model)
        {
            TouristAgencyDbEntities db = new TouristAgencyDbEntities();
            foreach (var item in db.Clients.AsNoTracking().ToList())
            {
                if (item.Email == model.Email)
                {
                    ModelState.AddModelError("Email", "Email уже указывался другим пользователем");
                }
            }
            foreach (var item in db.Employees.AsNoTracking().ToList())
            {
                if (item.Email == model.Email)
                {
                    ModelState.AddModelError("Email", "Email уже указывался другим пользователем");
                }
            }
            foreach (var item in db.TourOperators.AsNoTracking().ToList())
            {
                if (item.Email == model.Email)
                {
                    ModelState.AddModelError("Email", "Email уже указывался другим пользователем");
                }
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Register model)
        {
            AccountValidationEdit(model);

            if (ModelState.IsValid)
            {
                MyIdentityUser user = new MyIdentityUser();

                user.UserName = model.UserName;
                user.Email = model.Email;

                IdentityResult result = userManager.Create(user, model.Password);

                if (result.Succeeded)
                {
                    userManager.AddToRole(user.Id, "Client");

                    return RedirectToAction("Create", "Clients", new { model.Email });
                }
                else
                {
                    ModelState.AddModelError("UserName", "Ошибка при создании пользователя!");
                }
            }
            return View(model);
        }




        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Login model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                MyIdentityUser user = userManager.Find(model.UserName, model.Password);
                if (user != null)
                {
                    IAuthenticationManager authenticationManager = HttpContext.GetOwinContext().Authentication;

                    authenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
                    ClaimsIdentity identity = userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);

                    AuthenticationProperties props = new AuthenticationProperties();
                    props.IsPersistent = model.RememberMe;
                    authenticationManager.SignIn(props, identity);

                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        if (userManager.GetRoles(user.Id).Contains("Client"))
                        {
                            TouristAgencyDbEntities db = new TouristAgencyDbEntities();
                            var client = db.Clients.Where(n => n.Email.Equals(user.Email)).First();

                            return RedirectToAction("Details", "Clients", new { id = client.ID });
                        }
                        else if (userManager.GetRoles(user.Id).Contains("Employee") || userManager.GetRoles(user.Id).Contains("TourOperator"))
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Неправильный логин или пароль");
                }
            }
            return View(model);
        }



        [Authorize]
        public ActionResult ChangePassword()
        {
            return View();
        }



        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(ChangePassword model)
        {
            if (ModelState.IsValid)
            {
                MyIdentityUser user = userManager.FindByName(HttpContext.User.Identity.Name);
                IdentityResult result = userManager.ChangePassword(user.Id, model.OldPassword, model.NewPassword);

                if (result.Succeeded)
                {
                    IAuthenticationManager authenticationManager =
                   HttpContext.GetOwinContext().Authentication;
                    authenticationManager.SignOut();
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    ModelState.AddModelError("", " Ошибка при смене пароля");
                }
            }
            return View(model);
        }




        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult LogOut()
        {
            IAuthenticationManager authenticationManager =
            HttpContext.GetOwinContext().Authentication;
            authenticationManager.SignOut();
            return RedirectToAction("Login", "Account");
        }
    }
}