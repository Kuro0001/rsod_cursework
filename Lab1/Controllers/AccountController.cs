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
using Microsoft.AspNet.Identity.Owin;

namespace Lab1.Controllers
{
    public class AccountController : Controller
    {
        private readonly TouristAgencyDbEntities db = new TouristAgencyDbEntities();

        private MyUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<MyUserManager>();
            }
        }
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
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
        public async Task<ActionResult> Register(Register model)
        {
            AccountValidationEdit(model);

            if (ModelState.IsValid)
            {
                MyIdentityUser user = await UserManager.FindAsync(model.Email, model.Password);

                if (user == null)
                {
                    ModelState.AddModelError("", "Неверный логин или пароль.");
                }
                else
                {
                    ClaimsIdentity claim = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
                    AuthenticationManager.SignOut();
                    AuthenticationManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = true
                    }, claim);

                    return RedirectToAction("Index", "Home");
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
        public async Task<ActionResult> Login(Login model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                MyIdentityUser user = await UserManager.FindAsync(model.UserName, model.Password);

                if (user == null)
                {
                    ModelState.AddModelError("", "Неверный логин или пароль.");
                }
                else
                {
                    ClaimsIdentity claim = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
                    AuthenticationManager.SignOut();
                    AuthenticationManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = true
                    }, claim);

                    return RedirectToAction("Index", "Home");
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
        public async Task<ActionResult> ChangePassword(ChangePassword model)
        {
            if (ModelState.IsValid)
            {
                MyIdentityUser user = await UserManager.FindByEmailAsync(model.Email);

                if (user != null)
                {
                    IdentityResult result = await UserManager.ChangePasswordAsync(user.Id, model.OldPassword, model.NewPassword);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        foreach (string error in result.Errors)
                        {
                            ModelState.AddModelError("", error);
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Пользователь не найден");
                }
            }
            return View(model);
        }




        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult LogOut()
        {
            IAuthenticationManager authenticationManager = HttpContext.GetOwinContext().Authentication;

            authenticationManager.SignOut();

            return RedirectToAction("Index", "Home");
        }
    }
}