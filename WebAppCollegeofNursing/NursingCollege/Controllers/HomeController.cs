using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using ModelLayer;

namespace NursingCollege.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            HomeBLL homeBll = new HomeBLL();
           return View(homeBll.GetDataForHome());
        }
        public ActionResult NotificationDetails(int id)
        {
            SliderAndNotificationBLL notBll = new SliderAndNotificationBLL();
            return View(notBll.GetNotificationById(id));
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            

            return View();
        }
        [HttpPost]
        public ActionResult Contact(Contact contact)
        {
            ContactBLL conBll = new ContactBLL();
            if(ModelState.IsValid)
            {

                conBll.SendFeedback(contact);
            }
            ViewBag.Message = "Feedback sent Successfully";
            return View();
        }
        public ActionResult EventDetails(int id)
        {
            HomeBLL homeBll = new HomeBLL();
            return View(homeBll.EventDetails(id));
        }
        public ActionResult GetAllFaculties()
        {
            HomeBLL homeBll = new HomeBLL();
            return View(homeBll.GetAllFaculties());
        }
    }
}