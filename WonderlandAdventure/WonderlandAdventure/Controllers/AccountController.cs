using System;
using System.Web.Mvc;
using System.Web.Security;
using WonderlandAdventure.Models;

namespace WonderlandAdventure.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account/Login
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        // POST: Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            bool isAdmin = false;
            bool adminTabActive = Request.Form["adminTabActive"] == "true";

            // Admin login validation
            if (adminTabActive)
            {
                if ((model.Username == "yx" && model.Password == "123") ||
                    (model.Username == "sss" && model.Password == "aaa"))
                {
                    isAdmin = true;

                    // 设置身份验证 Cookie
                    FormsAuthentication.SetAuthCookie(model.Username, false);

                    // 存储管理员标识（可以用 Session 或自定义 Cookie）
                    Session["IsAdmin"] = true; // 使用 Session 存储管理员状态

                    // 返回 JSON 表示登录成功，前端用 JavaScript 弹窗
                    return Json(new { success = true, isAdmin = true, message = "管理员登录成功！" });
                }
            }
            // Visitor login validation (simplified for demo)
            else if (!string.IsNullOrEmpty(model.Username) && !string.IsNullOrEmpty(model.Password))
            {
                // Valid visitor login
            }
            else
            {
                ModelState.AddModelError("", "Invalid username or password");
                return View(model);
            }

            // Set authentication cookie
            FormsAuthentication.SetAuthCookie(model.Username, model.RememberMe);

            if (isAdmin)
            {
                return RedirectToAction("Index", "Admin");
            }

            return Redirect(returnUrl ?? Url.Action("Index", "Home"));
        }

        // GET: Account/Register
        public ActionResult Register()
        {
            return View();
        }

        // POST: Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                if (!Roles.RoleExists("Visitor"))
                    Roles.CreateRole("Visitor");

                if (Membership.GetUser(model.Email) == null)
                {
                    Membership.CreateUser(model.Email, model.Password, model.Email);
                    Roles.AddUserToRole(model.Email, "Visitor");
                }

                FormsAuthentication.SetAuthCookie(model.Email, false);
                TempData["RegistrationMessage"] = "Registration successful! Welcome to Wonderland Adventure!";
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Registration failed: " + ex.Message);
                return View(model);
            }
        }

        // POST: Account/Logout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }
    }
}