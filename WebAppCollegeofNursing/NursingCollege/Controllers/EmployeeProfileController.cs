using BLL;
using ModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NursingCollege.Controllers
{
    [Authorize(Roles = "Admin,Faculty,HOD")]
    public class EmployeeProfileController : Controller
    {
          ProfileBLL proBll = new ProfileBLL();
        AccountBLL _accountBll = new AccountBLL();

        public ActionResult Profile()
            {
            HttpCookie userData = Request.Cookies["UserData"];
            long id = Convert.ToInt64(userData["UserId"]);
                return View("Profile",GetLayout(),proBll.Profile(id));
            }
            public ActionResult EditProfile()
            {
            HttpCookie userData = Request.Cookies["UserData"];
            long id = Convert.ToInt64(userData["UserId"]);
            return View("EditProfile", GetLayout(), proBll.Profile(id));
        }
        [HttpPost]
        public ActionResult EditProfile(UserAccount account)
        {
            proBll.EditEmpProfile(account);
            return RedirectToAction("Profile");
        }
        [Authorize(Roles = "Student,Admin,Faculty,HOD")]
        public ActionResult ChangePassword()
        {
            return View("ChangePassword",GetLayout());
        }
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordVM changePasswordVM)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Message = _accountBll.ChangePassword(changePasswordVM);
            }
            return View("ChangePassword", GetLayout());
        }

        public string GetLayout()
        {
            if (User.IsInRole("HOD"))
            {
                return "~/Areas/HOD/Views/Shared/_Layout.cshtml";
            }
            else if (User.IsInRole("Student"))
            {
                return "~/Areas/Student/Views/Shared/_Layout.cshtml";
            }
            else if (User.IsInRole("Admin"))
            {
                return "~/Areas/Admin/Views/Shared/_Layout.cshtml";
            }
            else if (User.IsInRole("Faculty"))
            {
                return "~/Areas/Faculty/Views/Shared/_Layout.cshtml";
            }
            return "~/Views/Shared/_Layout.cshtml";
        }
    }
}