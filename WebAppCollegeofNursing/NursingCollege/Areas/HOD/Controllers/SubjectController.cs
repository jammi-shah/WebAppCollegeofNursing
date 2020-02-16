using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ModelLayer;
using BLL;

namespace NursingCollege.Areas.HOD.Controllers
{
    [Authorize(Roles ="HOD")]
    public class SubjectController : Controller
    {
        SubjectBLL subBll = new SubjectBLL();
        DepartmentBLL depBll = new DepartmentBLL();
        ClassInfoBLL classBll = new ClassInfoBLL();
        public ActionResult GetSubjects()
        {
            HttpCookie cookie = Request.Cookies["UserData"];
            int deptId = Convert.ToInt32(cookie["DeptId"]);
            return View(subBll.GetAllSubjects(deptId));
        }
        [HttpGet]
        public ActionResult AddSubject()
        {
            ViewBag.ClassId = new SelectList(classBll.GetAllClasses(), "Id", "ClassName");
            return View();
        }
        [HttpPost]
        public ActionResult AddSubject(Subject subject)
        {
            subject.IsDeleted = false;
            HttpCookie cookie = Request.Cookies["UserData"];
            int deptId = Convert.ToInt32(cookie["DeptId"]);
            subject.DepartmentId = deptId;
            if (ModelState.IsValid)
            {
                int returnValue=subBll.AddSubject(subject);
                if(returnValue>0)
                {
                    return RedirectToAction("GetSubjects");
                }
                ModelState.AddModelError("SubCode", "Subject with Sub-Code " + subject.SubCode + " Already Exists");
            }
            ViewBag.DepartmentId = new SelectList(depBll.GetDepartments(), "Id", "DeptName");
            ViewBag.ClassId = new SelectList(classBll.GetAllClasses(), "Id", "ClassName");
            return View();
            
        }
        public ActionResult EditSubject(int id)
        {
          Subject subject=  subBll.GetSubjectById(id);
            ViewBag.DepartmentId = new SelectList(depBll.GetDepartments(), "Id", "DeptName",subject.Department.DeptName);
            ViewBag.ClassId = new SelectList(classBll.GetAllClasses(), "Id", "ClassName", subject.ClassInfo.ClassName);
            return View(subject);
        }
        [HttpPost]
        public ActionResult EditSubject(Subject subject)
        {
            if (ModelState.IsValid)
            {
                int returnValue = subBll.EditSubject(subject);
                if (returnValue > 0)
                {
                    return RedirectToAction("GetSubjects");
                }
                ModelState.AddModelError("SubCode", "Subject with Sub-Code " + subject.SubCode + " Already Exists");
            }
            ViewBag.DepartmentId = new SelectList(depBll.GetDepartments(), "Id", "DeptName");
            ViewBag.ClassId = new SelectList(classBll.GetAllClasses(), "Id", "ClassName");
            return View();

        }
        public ActionResult DeleteSubject(int id)
        {
            return View(subBll.GetSubjectById(id));
        }
        [HttpPost,ActionName("DeleteSubject")]
        public ActionResult DeleteSubj(int id)
        {
            Subject subject= subBll.GetSubjectById(id);
            subBll.DeleteSubject(subject);
            return RedirectToAction("GetSubjects");
        }
        [HttpGet]
        public ActionResult SubjectAllocation(int id)
        {
            HttpCookie cookie = Request.Cookies["UserData"];
            int deptId = Convert.ToInt32(cookie["DeptId"]);
            ViewBag.SubjectId = id;

            if (subBll.GetSubjectAllocation(deptId).Any(x=>x.SubjectId==id))
            {
                TempData["Error"] = "Subject Already Allocated";
                return RedirectToAction("GetSubjects");
            }
            SelectList list= new SelectList(subBll.GetAllBatches());
            ViewBag.Batch = list;
            ViewBag.EmpId = new SelectList(subBll.GetFaculty(), "Id", "Name");
            return View();
        }
        [HttpPost]
        public ActionResult SubjectAllocation(SubjectAllocation subAlloc)
        {
           if(ModelState.IsValid)
            {

                int returnValue = subBll.AllocateSubject(subAlloc);
                if(returnValue>0)
                {
                   
                    return RedirectToAction("AllocatedSubjects");
                }
            }
            ModelState.AddModelError("EmpId", "Teacher is busy at this Time");
            
            SelectList batch = new SelectList(subBll.GetAllBatches());
          
            ViewBag.Batch = batch;
            ViewBag.EmpId = new SelectList(subBll.GetFaculty(), "Id", "Name");
            return View();
        }
        public ActionResult AllocatedSubjects()
        {
            HttpCookie cookie = Request.Cookies["UserData"];
            int deptId = Convert.ToInt32(cookie["DeptId"]);
            return View(subBll.GetSubjectAllocation(deptId));
        }
        public ActionResult DeAllocateSubject(int id)
        {
            return View(subBll.GetSubjectAllocationById(id));
        }
        [HttpPost, ActionName("DeAllocateSubject")]
        public ActionResult DeAllocateSubj(int id)
        {
            subBll.DeAllocateSubject(id);
            return RedirectToAction("AllocatedSubjects");
        }
        [HttpGet]
        public ActionResult EditAllocation(int id)
        {

            SubjectAllocation subAlloc = subBll.GetSubjectAllocationById(id);
           
            SelectList batch = new SelectList(subBll.GetAllBatches());
            ViewBag.Batch = batch;
            ViewBag.EmpId = new SelectList(subBll.GetFaculty(), "Id", "Name");
            return View(subAlloc);
        }
        [HttpPost]
        public ActionResult EditAllocation(SubjectAllocation subAlloc)
        {
            if (ModelState.IsValid)
            {
                int returnValue = subBll.EditAllocation(subAlloc);
                if (returnValue > 0)
                {
                    return RedirectToAction("AllocatedSubjects");
                }
            }
            ModelState.AddModelError("EmpId", "Teacher is busy at this Time");
          
            SelectList batch = new SelectList(subBll.GetAllBatches());
            ViewBag.Batch = batch;
            ViewBag.EmpId = new SelectList(subBll.GetFaculty(), "Id", "Name");
            return View();
        }
    }
}