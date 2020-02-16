using BLL;
using ModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NursingCollege.Areas.HOD.Controllers
{
    [Authorize(Roles ="HOD")]
    public class HODHomeController : Controller
    {
        
        public ActionResult Index()
        {
            return View();
        }

     
    }
}