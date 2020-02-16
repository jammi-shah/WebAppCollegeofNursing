using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using ModelLayer;

namespace NursingCollege.Areas.Admin.Controllers
{
   // [Authorize(Roles = "Admin")]
    public class NotificationAndSliderController : Controller
    {
        SliderAndNotificationBLL bll = new SliderAndNotificationBLL();
        public ActionResult GetNotifications()
        {
            return View(bll.GetNotifications());
        }
        public ActionResult GetSlider()
        {
            return View(bll.GetSlider());
        }
        public ActionResult AddNotification()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddNotification(Notification not)
        {
            if (ModelState.IsValid)
            {
                bll.AddNotification(not);
                return RedirectToAction("GetNotifications");
            }
            return View();
        }

        public ActionResult EditNotification(int id)
        {
            return View(bll.GetNotificationById(id));
        }

        [HttpPost]
        public ActionResult EditNotification(Notification not)
        {
            if (ModelState.IsValid)
            {
                bll.EditNotification(not);
                return RedirectToAction("GetNotifications");
            }
            return View();
        }
        public ActionResult DeleteNotification(int id)
        {
            return View(bll.GetNotificationById(id));
        }

        [HttpPost,ActionName("DeleteNotification")]
        public ActionResult DelNotification(int id)
        {
            Notification not = new Notification();
            not.Id = id;
            bll.DeleteNotification(not);
            return RedirectToAction("GetNotifications");

        }

        public ActionResult AddSlider()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddSlider(Slider slider)
        {
            if (ModelState.IsValid)
            {
                bll.AddSlider(slider);
                return RedirectToAction("GetSlider");
            }
            return View();
        }


        public ActionResult DeleteSlider(int id)
        {
            bll.DeleteSlider(id);
            return RedirectToAction("GetSlider");
        }

        public ActionResult BlockSlider(int id)
        {
            bll.BlockSlider(id);
            return RedirectToAction("GetSlider");
        }
        public ActionResult AddEvents()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddEvents(Event events)
        {
            events.IsActive = true;
            if (ModelState.IsValid)
            {
                bll.AddEvent(events);
                return RedirectToAction("GetAllEvents");
            }
            return View();
        }
        public ActionResult GetAllEvents()
        {
            return View(bll.GetEvents());
        }
        public ActionResult DeleteEvent(int id)
        {
            bll.DeleteEvent(id);
            return RedirectToAction("GetAllEvents");
        }
        public ActionResult BlockEvent(int id)
        {
            bll.BlockEvent(id);
            return RedirectToAction("GetAllEvents");
        }
    }
}