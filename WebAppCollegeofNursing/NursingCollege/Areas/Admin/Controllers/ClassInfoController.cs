using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ModelLayer;
using BLL;

namespace NursingCollege.Areas.Admin.Controllers
{
    [Authorize(Roles ="Admin")]
    public class ClassInfoController : Controller
    {
        // GET: Admin/ClassInfo
        ClassInfoBLL bll = new ClassInfoBLL();
        public ActionResult GetClasses()
        {
            return View();
        }
        public JsonResult CreateClass(ClassInfo classInfo)
        {
            if (ModelState.IsValid)
            {
                bll.AddClass(classInfo);
            }
            return Json(classInfo, JsonRequestBehavior.AllowGet);

        }
        public ActionResult EditClass(ClassInfo classInfo)
        {
            if (ModelState.IsValid)
            {
                int returnValue = bll.EditClass(classInfo);
            }
            return Json(classInfo, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteClass(int id)
        {
            bll.DeleteClass(id);
            return Json(id, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetAllClasses()
        {
            return Json(bll.GetAllClasses(),JsonRequestBehavior.AllowGet);
        }
    }
}