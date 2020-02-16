using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BLL;
using Microsoft.AspNet.SignalR;
using ModelLayer;

namespace NursingCollege
{
    public class ChatHub : Hub
    {
        ChatBLL _bll = new ChatBLL();
       
        public void AddComment(string comment)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies["UserData"];
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<ChatHub>();
            Comment com = _bll.AddComment(comment, HttpContext.Current.User.Identity.Name);
            CommentVM commentVM = new CommentVM {
                Id = com.Id,
                Text = com.Text,
                UserId = com.UserId,
                CommentDate = com.CommentDate,
                ImagePath = cookie["ImagePath"],
                UserName = cookie["Name"]
            };
            context.Clients.All.broadcastMessage(commentVM);
        }
        public List<CommentVM> GetComments()
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies["UserData"];
            List<CommentVM> comments = _bll.GetComments(Convert.ToInt32(cookie["DeptId"]));
            return comments;

        }
        public List<ReplyVM> GetReplies(int commentId)
        {

            List<ReplyVM> replies = _bll.GetReplies(commentId);
            return replies;

        }
        public void AddReply(string replyText, int commentId)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies["UserData"];
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<ChatHub>();
            Reply reply=  _bll.ReplyComment(replyText, commentId, Convert.ToInt64(cookie["UserId"]));
            // ReplyVM replyvm1 = _bll.GetLastReply(commentId);
            ReplyVM replyvm = new ReplyVM
            {
                Id=reply.Id,
                ReplyDate=reply.ReplyDate,
                ImagePath= cookie["ImagePath"],
                Text=reply.Text,
                UserName= cookie["Name"]
            };
            context.Clients.All.broadcastReply(replyvm);
        }
    }
}