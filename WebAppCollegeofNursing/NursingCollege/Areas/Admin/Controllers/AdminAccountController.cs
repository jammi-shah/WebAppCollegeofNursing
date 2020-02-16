using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ModelLayer;
using BLL;

namespace NursingCollege.Areas.Admin.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class AdminAccountController : Controller
    {
        AdminBLL _adminBLL = new AdminBLL();
        ProfileBLL proBll = new ProfileBLL();
     
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ViewDepartment()
        {
            return View();
        }
        [HttpPost]
        public JsonResult CreateDepartment(Department department)
        {
            if (ModelState.IsValid)
            {
                department = _adminBLL.CreateDepartment(department);
            }
            return Json(department, JsonRequestBehavior.AllowGet);

        } 
        public JsonResult GetAllDepartment()
        {
            return Json(_adminBLL.GetDepartments().Select(x=>new { Id=x.Id, DeptName=x.DeptName}), JsonRequestBehavior.AllowGet);
        }
        public ActionResult EditDepartment(Department department)
        {
            if (ModelState.IsValid)
            {
                int returnValue = _adminBLL.EditDepartment(department);
            }
            return Json(department, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Employees()
        {
            return View();
        }
        public ActionResult BlockUser(long id)
        {
            _adminBLL.BlockUnblock(id);
            return RedirectToAction("GetStudents");
        }
        public ActionResult ViewAllEmployees()
        {
            IEnumerable<UserAccount> users = _adminBLL.GetAllEmployees();
            return  View(users);
        }

        public ActionResult GetStudents()
        {
            IEnumerable<UserAccount> users = _adminBLL.GetStudents();
            return View(users);
        }
        [HttpGet]
        public ActionResult CreateEmployee()
        {
            ViewBag.DepartmentId = new SelectList(_adminBLL.GetDepartments(), "Id", "DeptName");
            return View();
        }
      
        [HttpPost]
        public ActionResult CreateEmployee(UserAccount userAccount)
        {
            if (ModelState.IsValid)
            {
                string returnValue = _adminBLL.CreateEmployee(userAccount); 
                if(returnValue == "Success")
                {
                    return RedirectToAction("ViewAllEmployees");
                }
                 if(returnValue== "CodeExists")
                {
                    ModelState.AddModelError("UserCode", "Employee Code already Exists");
                  
                }
                if (returnValue == "HODExists")
                {
                    ModelState.AddModelError("UserRole", "HOD is already assigned to this Department");
                }
                    
               else if (returnValue == "EmailExists")
                {
                    ModelState.AddModelError("Email", "Email already Exists");

                }
            }
            ViewBag.DepartmentId = new SelectList(_adminBLL.GetDepartments(), "Id", "DeptName");
            return View();
        }

        public ActionResult EditEmployee(int id)
        {
            ViewBag.DepartmentId = new SelectList(_adminBLL.GetDepartments(), "Id", "DeptName");
            return View(_adminBLL.GetEmployeeById(id).SingleOrDefault());
        }

        [HttpPost]
        public ActionResult EditEmployee(UserAccount account)
        {
            _adminBLL.EditEmployee(account);
            return RedirectToAction("ViewAllEmployees");
        }
        public ActionResult DeleteEmployee(int id)
        {
            _adminBLL.DeleteEmployee(id);
            TempData["Message"] = "Employee Deleted";
            return RedirectToAction("ViewAllEmployees");
        }
        public ActionResult EmployeeDetails(int id)
        {
            ViewBag.DepartmentId = new SelectList(_adminBLL.GetDepartments(), "Id", "DeptName");
            UserAccount user = _adminBLL.GetEmployeeById(id).SingleOrDefault();
            return View(user);
        }
        ContactBLL conBll = new ContactBLL();
        public ActionResult GetFeedback()
        {

            return View(conBll.GetFeedback());
        }
        public ActionResult DeleteFeedback(int id)
        {
            Contact contact = new Contact();
            contact.Id = id;
            conBll.DeleteFeedback(contact);
            return RedirectToAction("GetFeedback");
        }
    }
}