using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using MachineLogViewer.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MachineLogViewer.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager )
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set 
            { 
                _signInManager = value; 
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.UserName, model.Password, isPersistent: false, shouldLockout: true);
            if (result == SignInStatus.Success)
            {
                var db = new ApplicationDbContext();
                var user = db.Users.First(u => u.UserName == model.UserName);

                if (!user.IsActive)
                {
                    result = SignInStatus.LockedOut;
                }
            }

            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }
        }

 
        //
        // GET: /Account/Register
        [Authorize(Roles = "admin")]
        public ActionResult Register()
        {
            return View(new RegisterViewModel());
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = model.GetUser();
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    var db = new ApplicationDbContext();

                    var adminRole = db.Roles.SingleOrDefault(m => m.Name == "admin");

                    if (model.IsAdmin)
                    {
                        user.Roles.Add(new IdentityUserRole { RoleId = adminRole.Id });
                        db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                        await db.SaveChangesAsync();
                    }

                    //await SignInManager.SignInAsync(user, isPersistent:false, rememberBrowser:false);
                    
                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    return RedirectToAction("Index", "Account");
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            var db = new ApplicationDbContext();
            var users = db.Users.OrderBy(u => u.UserName);
            var model = new List<EditUserViewModel>();
            foreach (var user in users)
            {
                var u = new EditUserViewModel(user);
                u.IsAdmin = UserManager.GetRoles(user.Id).Contains("admin");
                model.Add(u);
            }
            return View(model);
        }

        [Authorize(Roles = "admin")]
        public ActionResult Edit(string id, ManageController.ManageMessageId? Message = null)
        {
            var db = new ApplicationDbContext();
            var user = db.Users.First(u => u.Id == id);
            var model = new EditUserViewModel(user);
            model.IsAdmin = UserManager.GetRoles(user.Id).Contains("admin");
            ViewBag.MessageId = Message;
            return View(model);
        }
        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var db = new ApplicationDbContext();
                var user = db.Users.First(u => u.Id == model.Id);

                var isAdmin = UserManager.GetRoles(user.Id).Contains("admin");

                var adminRole = db.Roles.SingleOrDefault(m => m.Name == "admin");

                if (!isAdmin && model.IsAdmin)
                {
                    user.Roles.Add(new IdentityUserRole {RoleId = adminRole.Id});
                }
                else if (isAdmin && !model.IsAdmin)
                {
                    var role = user.Roles.First(r => r.RoleId == adminRole.Id);
                    user.Roles.Remove(role);
                }

                // Update the user data:
                user.IsActive = model.IsActive;
                db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            // If we got this far, something failed, redisplay form
            return View(model);
        }
        [Authorize(Roles = "admin")]
        public ActionResult Delete(string id = null)
        {
            var db = new ApplicationDbContext();
            var user = db.Users.First(u => u.Id == id);
            if (user == null)
            {
                return HttpNotFound();
            }
            if (user.Machines.Any())
            {
                return View("UnableToDelete"); 
            }
            var model = new EditUserViewModel(user);
            return View(model);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public ActionResult DeleteConfirmed(string id)
        {
            var Db = new ApplicationDbContext();
            var user = Db.Users.First(u => u.Id == id);
            Db.Users.Remove(user);
            Db.SaveChanges();
            return RedirectToAction("Index");
        }

        //
        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var result = await UserManager.ConfirmEmailAsync(userId, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        #region Helpers
 
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        #endregion
    }
}