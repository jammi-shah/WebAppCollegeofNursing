using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using ModelLayer;
namespace NursingCollege.Areas.Faculty.Controllers
{
    [Authorize(Roles = "Faculty")]
    public class FacultyHomeController : Controller
    {
        ProfileBLL proBll = new ProfileBLL();
        public ActionResult Index()
        {
            return View();
        }
       

    }
}