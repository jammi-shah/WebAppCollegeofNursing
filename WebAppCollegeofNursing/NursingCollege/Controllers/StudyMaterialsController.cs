using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using ModelLayer;

namespace NursingCollege.Controllers
{
    [Authorize(Roles ="HOD,Faculty,Student")]
    public class StudyMaterialsController : Controller
    {
      
        StudyMaterialBll studBll = new StudyMaterialBll();
        SubjectBLL subBll = new SubjectBLL();
        // GET: StudyMaterials
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult AddStudyMaterial()
        {
            HttpCookie cookie = Request.Cookies["UserData"];
            int deptId = Convert.ToInt32(cookie["DeptId"]);
            ViewBag.SubjectId = new SelectList(subBll.GetAllSubjects(deptId), "Id", "SubName");
            return View("AddStudyMaterial", GetLayout());
        }
        [HttpPost]
        public ActionResult AddStudyMaterial(StudyMaterial studyMaterial)
        {
           
                string returnMessage = "";
                string lastFileName = "";
            HttpCookie cookie = Request.Cookies["UserData"];
            int deptId = Convert.ToInt32(cookie["DeptId"]);
            studyMaterial.UserId  = Convert.ToInt64(cookie["UserId"]);
            ViewBag.SubjectId = new SelectList(subBll.GetAllSubjects(deptId), "Id", "SubName");
            if (ModelState.IsValid)
            {
                if (studyMaterial.Files[0]!=null)
                {
                    studBll.AddStudyMaterial(studyMaterial);
                    foreach (HttpPostedFileBase file in studyMaterial.Files)
                    {
                        StudyMaterialFile sMFile = new StudyMaterialFile();

                        returnMessage = CommonBLL.FileUpload(file);

                        if (returnMessage != "Format" && returnMessage != "Size")
                        {
                            sMFile.StudyMaterialId = studyMaterial.Id;
                            sMFile.FilePath = returnMessage;
                            studBll.AddStudyMaterialFile(sMFile);
                            file.SaveAs(Server.MapPath(returnMessage));

                        }
                        else
                        {
                            if (lastFileName != "")
                            {
                                System.IO.File.Delete(Server.MapPath(lastFileName));
                            }
                            studBll.DeleteStudyMaterialFile(studyMaterial.Id);
                            ViewBag.Message = returnMessage;
                            return View();
                        }
                        lastFileName = returnMessage;
                    }
                }
                else
                {
                    ModelState.AddModelError("Files", "Select Files");
                }

                ViewBag.Message = returnMessage;
            }
            if (studyMaterial.Files[0] == null)
            {
                ModelState.AddModelError("Files", "Select Files");
            }





            return View("AddStudyMaterial", GetLayout());

        }

        public ActionResult GetStudyMaterial()
        {
            return View("GetStudyMaterial", GetLayout(), studBll.GetStudyMaterial());
        }
        public ActionResult GetMyStudyMaterial()
        {
            HttpCookie cookie = Request.Cookies["UserData"];
            long userId = Convert.ToInt64(cookie["UserId"]);
            return View("GetMyStudyMaterial", GetLayout(), studBll.GetMyStudyMaterial(userId));
        }
        public ActionResult MyStudyMaterialDetails(int id)
        {
            ViewBag.Layout = GetLayout();
            return View("MyStudyMaterialDetails", GetLayout(), studBll.GetStudyMaterialById(id));
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
            else if (User.IsInRole("Faculty"))
            {
                return "~/Areas/Faculty/Views/Shared/_Layout.cshtml";
            }
            return "~/Views/Shared/_Layout.cshtml"; 
        }
       [HttpGet]
        public ActionResult DeleteMaterial(int id)
        {
            return View("DeleteMaterial", GetLayout(), studBll.GetStudyMaterialById(id));
        }
        [HttpPost, ActionName("DeleteMaterial")]
        public ActionResult DeleteStudyMaterial(int id)
        {
            studBll.DeleteStudyMaterial(id);
            return RedirectToAction("GetStudyMaterial");
        }

        public ActionResult DownloadFile (string filePath)
        {
            CommonBLL.DownloadFile(filePath);
            return View();
        }
    }
}