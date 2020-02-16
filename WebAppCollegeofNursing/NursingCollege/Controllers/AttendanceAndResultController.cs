using BLL;
using ModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NursingCollege.Controllers
{
    [Authorize(Roles ="HOD,Faculty")]
    public class AttendanceAndResultController : Controller
    {
        ResultBll resBll = new ResultBll();
        AttendanceBLL attBll = new AttendanceBLL();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AddResult()
        {
            SubjectBLL subBLL = new SubjectBLL();
            ViewBag.Batch = new SelectList(subBLL.GetAllBatches());
            HttpCookie cookie = Request.Cookies["UserData"];
            int empId = Convert.ToInt32(cookie["UserId"]);
            ViewBag.SubjectId = new SelectList(resBll.GetMySubjects(empId), "Id", "SubName");
            return View("AddResult", GetLayout());

        }
        [HttpPost]
        public ActionResult AddResult(Result result)
        {
            if (resBll.ResultExists(result.SubId, result.StudentId))
            {
                resBll.EditResult(result);
            }
            else
            {
                resBll.AddResult(result);
            }

            return View("AddResult", GetLayout());
        }
        public ActionResult CheckResult()
        {
            SubjectBLL subBLL = new SubjectBLL();
            ViewBag.Batch = new SelectList(subBLL.GetAllBatches());
            HttpCookie cookie = Request.Cookies["UserData"];
            int empId = Convert.ToInt32(cookie["UserId"]);
            ViewBag.SubjectId = new SelectList(resBll.GetMySubjects(empId), "Id", "SubName");
            return View("CheckResult", GetLayout());
        }
        public JsonResult GetResult(string rollno, int subjectId, int batch)
        {
            HttpCookie cookie = Request.Cookies["UserData"];
            int deptId = Convert.ToInt32(cookie["DeptId"]);
            return Json(resBll.GetResult(rollno, batch, subjectId, deptId), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetStudentsForResult(int batch, int subjectId)
        {
            HttpCookie cookie = Request.Cookies["UserData"];
            int deptId = Convert.ToInt32(cookie["DeptId"]);
            return Json(resBll.GetStudents(batch, subjectId, deptId), JsonRequestBehavior.AllowGet);
        }
        SubjectBLL subjectBLL = new SubjectBLL();
    
        public ActionResult GetStudentsByClass()
        {
            SelectList batch = new SelectList(subjectBLL.GetAllBatches());
            ViewBag.Batch = batch;

            HttpCookie cookie = Request.Cookies["UserData"];
            int empId = Convert.ToInt32(cookie["UserId"]);
            int deptId = Convert.ToInt32(cookie["DeptId"]);
            SelectList subjects = new SelectList(subjectBLL.GetAllSubjectsbyEmpId(empId, deptId), "Id", "SubName");
            ViewBag.Subject = subjects;



            return View("GetStudentsByClass", GetLayout());
        }
        public ActionResult CheckAttendance()
        {
            SubjectBLL subBLL = new SubjectBLL();
            ViewBag.Batch = new SelectList(subBLL.GetAllBatches());
            HttpCookie cookie = Request.Cookies["UserData"];
            int empId = Convert.ToInt32(cookie["UserId"]);
            ViewBag.SubjectId = new SelectList(resBll.GetMySubjects(empId), "Id", "SubName");
            return View("CheckAttendance", GetLayout());
        }
        public JsonResult GetAttendance(string rollNo,int batch, int subjectId)
        {
            HttpCookie cookie = Request.Cookies["UserData"];
            int deptId = Convert.ToInt32(cookie["DeptId"]);
            return Json(attBll.GetAttendance(rollNo,batch,subjectId), JsonRequestBehavior.AllowGet);
        }
       
        public JsonResult GetStudents(int batch, int subId, string attDate)
        {
            return Json(attBll.GetStudents(subId, batch, Convert.ToDateTime(attDate).Date), JsonRequestBehavior.AllowGet);
        }
        public JsonResult AddAttendance(int Id, string AttendanceStatus, string AttDate)
        {
            return Json(attBll.AddAttendance(Id, Convert.ToDateTime(AttDate).Date, AttendanceStatus), JsonRequestBehavior.AllowGet);
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