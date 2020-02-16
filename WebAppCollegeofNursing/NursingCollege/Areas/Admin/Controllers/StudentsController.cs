using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ModelLayer;
using BLL;

namespace NursingCollege.Areas.Admin.Controllers
{
    public class StudentsController : Controller
    {
        AdminBLL _adminBLL = new AdminBLL();
        ClassInfoBLL _classBLL = new ClassInfoBLL();
        // GET: Admin/Students
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Students()
        {
            return View();
        }

        public ActionResult ViewAllStudents()
        {
            ViewBag.DepartmentId = new SelectList(_adminBLL.GetDepartments(), "Id", "DeptName");
            ViewBag.CurrentClass = new SelectList(_classBLL.GetAllClasses(), "Id", "ClassName");
            return View();
        }

        public ActionResult GetStudents(string deptId, string currentClass)
        {
            if (deptId!="")
            {
                IEnumerable<UserAccount> users = _adminBLL.GetAllStudents(Convert.ToInt32(deptId), currentClass);
                if (users.Any())
                return View(users);
            }
            return null;
        }

        public ActionResult EditStudent()
        {
            return RedirectToAction("ViewAllStudents");
        }

        public ActionResult BlockedStudents()
        {
            return View(_adminBLL.GetBlockedStudents());
        }
        

        public ActionResult UnblockStudents(int id)
        {
            _adminBLL.BlockUnblock(id);
            return RedirectToAction("BlockedStudents");
        }
    }
}