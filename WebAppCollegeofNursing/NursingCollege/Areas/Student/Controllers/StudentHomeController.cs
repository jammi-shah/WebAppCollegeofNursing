using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using ModelLayer;
namespace NursingCollege.Areas.Student.Controllers
{
    [Authorize(Roles = "Student")]
    public class StudentHomeController : Controller
    {
        // GET: Student/StudentHome
        ProfileBLL proBll = new ProfileBLL();
        AccountBLL _accountBll = new AccountBLL();
        public ActionResult Index()
        {
            return View();
        }
       

        public ActionResult StudentDetails()
        {
            //UserAccount userAccount = _studentBLL.GetStudentByUserCode(User.Identity.Name);
            //if (userAccount != null)
            //{
            //    return View(userAccount);
            //}
            //TempData["Message"] = "Account does not exist";
            return RedirectToAction("Index");
        }

        public ActionResult Profile()
        {
            HttpCookie cookie = Request.Cookies["UserData"];
            long id = Convert.ToInt64(cookie["UserId"]);
            return View(proBll.Profile(id));
        }
        public ActionResult EditProfile()
        {
            HttpCookie cookie = Request.Cookies["UserData"];
            long id = Convert.ToInt64(cookie["UserId"]);
            return View(proBll.Profile(id));
        }
        [HttpPost]
        public ActionResult EditProfile(UserAccount account)
        {
            proBll.EditProfile(account);          
            return RedirectToAction("Profile");
        }
        public ActionResult ChangePassword()
        {
            return View();
        }
       
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordVM changePasswordVM)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Message = _accountBll.ChangePassword(changePasswordVM);
            }
            return View();
        }
    }
}