using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NursingCollege.Areas.Student.Controllers
{
    [Authorize(Roles = "Student")]
    public class StudentAttendenceController : Controller
    {
        SubjectBLL subjectBLL = new SubjectBLL();
        AttendanceBLL attendanceBLL = new AttendanceBLL();
        // GET: Student/StudentAttendence
        public ActionResult GetAttendence()
        {
            return View();
        }
        public JsonResult GetSubjectsByYear(int year)
        {
            return Json( subjectBLL.GetAllSubjectsByYear(year).Select(x=>new { Id=x.Id, SubName=x.SubName}), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAttendenceByStudent(int subId, string fromDate, string toDate)
        {
            return Json(attendanceBLL.GetStudentAttendence(subId, Convert.ToDateTime(fromDate).Date, Convert.ToDateTime(toDate).Date).Select(x=> new {
                 AttDate=Convert.ToDateTime(x.AttDate).ToShortDateString(),
                 AttStatus=x.AttStatus
                }), JsonRequestBehavior.AllowGet);
        }
    }
}