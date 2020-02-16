using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ModelLayer;
using BLL;

namespace NursingCollege.Areas.Student.Controllers
{
    [Authorize(Roles ="Student")]
    public class ResultController : Controller
    {
        ResultBll _resultBLL = new ResultBll();
        // GET: Student/Result
        public ActionResult ViewResult()
        {

            return View();
        }

        public JsonResult GetResultByYear(string year)
        {
            HttpCookie userData = Request.Cookies["UserData"];
            long studentId = Convert.ToInt64(userData["UserId"]);
            var result = _resultBLL.GetResultByYearAndStudent(year,studentId).Select(x => new
            {
                Subject = x.Subject.SubName,
                Paper1 = x.Paper1,
                Paper2 = x.Paper2,
                Paper3 = x.Paper3,
                ModelPaper = x.ModelPaper,
                PPT = x.PPT,
                Assignment = x.Assignment,
                ClassPresentation = x.ClassPresentation,
                Attendence = x.Attendence
            });
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}