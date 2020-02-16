using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ModelLayer;
using BLL;
using System.Web.Security;
using System.Net.Mail;
using System.Text;
using BLL;

namespace NursingCollege.Controllers
{
    public class AccountController : Controller
    {
        AccountBLL _accountBLL = new AccountBLL();
        // GET: Account
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult Register()
        {
            ViewBag.DepartmentId = new SelectList(_accountBLL.GetDepartments(), "Id", "DeptName");
            return View();
        }

        [HttpPost]
        public ActionResult Register(UserAccount userAccount)
        {
            if (_accountBLL.Register(userAccount) > 0)
            {
                TempData["Message"] = "Account Created";
                return RedirectToAction("login");
            }
            ViewBag.DepartmentId = new SelectList(_accountBLL.GetDepartments(), "Id", "DeptName");
            return View();
        }

        public bool IsAvailable(string value)
        {
            return _accountBLL.IsAvailable(value);
        }



        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Login login)
        {
            UserAccount account = _accountBLL.Login(login);
            if (account == null)
            {
                ModelState.AddModelError("", "Invalid UserName and / Or password");
            }
            else
            {
                if (account.IsActive ==false)
                {
                    ModelState.AddModelError("", "Your Account is Blocked");
                }
                else
                {
                    Session["UserId"] = account.Id;
                    HttpCookie UserCookie = new HttpCookie("UserData");
                    UserCookie["DeptId"] = account.DepartmentId.ToString();
                    UserCookie["ImagePath"] = account.ImagePath;
                    UserCookie["Name"] = account.Name;
                    UserCookie["UserId"] = account.Id.ToString();
                    Response.Cookies.Add(UserCookie);
                    FormsAuthentication.Initialize();
                    FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, login.UserCode, DateTime.Now, DateTime.Now.AddDays(1), login.IsChecked, FormsAuthentication.FormsCookiePath);
                    string hash = FormsAuthentication.Encrypt(ticket);
                    HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, hash);

                    if (ticket.IsPersistent)
                    {
                        cookie.Expires = ticket.Expiration;
                    }

                    Response.Cookies.Add(cookie);
                    if (account.UserRole == "Admin")
                    {
                        return RedirectToAction("Index", new { Area = "Admin", Controller = "AdminAccount" });
                    }
                    else if (account.UserRole == "HOD")
                    {
                        Session["EmpId"] = account.Employee.Id;
                        return RedirectToAction("Index", new { Area = "HOD", Controller = "HODHome" });
                    }
                    else if (account.UserRole == "Faculty")
                    {
                        Session["EmpId"] = account.Employee.Id;
                        return RedirectToAction("Index", new { Area = "Faculty", Controller = "FacultyHome" });
                    }
                    else if (account.UserRole == "Student")
                    {
                        return RedirectToAction("Index", new { Area = "Student", Controller = "StudentHome" });
                    }

                }
            }


            return View();
        }

        public ActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgetPassword(string userCode)
        {
            string guid = Guid.NewGuid().ToString();
            string email = _accountBLL.ForgetPassword(guid, userCode);
            if (email != null)
            {
                EmailBLL.SendEmail(guid, email, userCode);
                ViewBag.Message = "success";
            }
            else
            {
                ViewBag.Message = "invalid username";
            }
            return View();
        }


        public ActionResult ResetPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ResetPassword(ResetPasswordViewModel resetPassword)
        {
            if (ModelState.IsValid)
            {
                resetPassword.GUID = Request.QueryString["guid"];
                int returnValue = _accountBLL.ResetPassword(resetPassword);
                if (returnValue > 0)
                {
                    TempData["Message"] = "Password changed successfully";
                    return RedirectToAction("Login");
                }
                else
                {
                    ViewBag.Message = "Error";
                }
            }
            return View();
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
       

    }
}