using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using BLL;

namespace EEducation.Controllers
{
    [Authorize(Roles = "Admin,Student,Faculty,HOD")]
    public class ChatroomController : Controller
    {
        ChatBLL _chatBLL = new ChatBLL();
        // GET: Chatroom
        public ActionResult Index()
        {
           // Roles.GetRolesForUser("Emp103");
            return View();
        }

        public ActionResult ChatRoom()
        {
            return View();

        }

        [HttpGet]
        public ActionResult ViewReplies()
        {
            int cId = Convert.ToInt32(Request.QueryString["cId"]);
            ViewBag.CommentId = cId;
            return View(_chatBLL.GetReplies(cId));
           
        }

    }
}