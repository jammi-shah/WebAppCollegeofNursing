using DAL.Repository;
using ModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Web;


namespace BLL
{
    public class ChatBLL
    {
        GenericRepository<Comment> commentDAL = new GenericRepository<Comment>();
        GenericRepository<Reply> replyDAL = new GenericRepository<Reply>();
        GenericRepository<Employee> empDAL = new GenericRepository<Employee>();
        GenericRepository<UserAccount> accDAL = new GenericRepository<UserAccount>();
        GenericRepository<Student> stDAL = new GenericRepository<Student>();
        public List<CommentVM> GetComments(int deptId)
        {
            IEnumerable<Comment> mycomments = new List<Comment>();
            mycomments = commentDAL.FindBy(x => x.UserAccount.DepartmentId == deptId).OrderByDescending(x => x.CommentDate);
            List<CommentVM> comlist = new List<CommentVM>();
            foreach (Comment item in mycomments)
            {
                CommentVM vm = new CommentVM();
                vm.Id = item.Id;
                vm.Text = item.Text;
                vm.UserName = item.UserAccount.Name;
                vm.CommentDate = item.CommentDate;
                vm.ImagePath = GetUserImagePathForComment(item.UserId);
                comlist.Add(vm);
            }
            return comlist;
        }


        public List<ReplyVM> GetReplies(int commentId)
        {
            List<ReplyVM> replyList = new List<ReplyVM>();
            IEnumerable<Reply> myReplies = replyDAL.FindBy(x => x.CommentId == commentId).OrderByDescending(x => x.ReplyDate);
            
            if (myReplies != null)
            {
                foreach (Reply item in myReplies)
                {
                    
                    ReplyVM vm = new ReplyVM();
                   
                    vm.Id = item.Id;
                    vm.Text = item.Text;
                    vm.UserName = item.UserAccount.Name;
                    vm.ReplyDate = item.ReplyDate;
                    vm.ImagePath =  GetUserImagePathForReply(item.UserId);

                    replyList.Add(vm);
                }
                return replyList;
            }
         
            return null;

        }

        private string GetUserImagePathForReply(long? userId)
        {
            return accDAL.FindBy(x => x.Id == userId).SingleOrDefault().ImagePath;
        }

        private string GetUserImagePathForComment(long? userId)
        {
            return accDAL.FindBy(x => x.Id == userId).SingleOrDefault().ImagePath;
        }

        public Comment AddComment(string commentText,string userCode)
        {
            Comment comm = new Comment();
            comm.Text = commentText;
            comm.UserId = accDAL.FindBy(x => x.UserCode == userCode).SingleOrDefault().Id;
            comm.CommentDate = DateTime.Now;
            commentDAL.Add(comm);
            return comm;
        }
        //public CommentVM GetCurrentComment(int cId)
        //{
        //    Comment comment = commentDAL.GetById(cId);
        //    CommentVM vm = new CommentVM()
        //    {
        //        Id = comment.Id,
        //        Text = comment.Text,
        //        UserName = comment.UserAccount.UserName,
        //        CommentDate = comment.CommentDate,
        //        ImagePath = GetUserImagePathForComment(comment.UserId)
        //    };
        //    return vm;
        //}

        //public CommentVM GetLastComment(string userName)
        //{
        //    Comment comment = new Comment();
        //    long? deptId = accDAL.FindBy(x => x.UserName == userName).SingleOrDefault().DepartmentId;
        //    comment = commentDAL.FindBy(x => x.UserAccount.DepartmentId == deptId).ToList().LastOrDefault();// lastordefault
            
        //    CommentVM vm = new CommentVM
        //    {
        //        Id = comment.Id,
        //        Text = comment.Text,
        //        UserName = userName,
        //        CommentDate = comment.CommentDate,
        //        ImagePath = GetUserImagePathForComment(comment.UserId)
        //    };

        //    return vm;
        //}
        //public ReplyVM GetLastReply(int commentId)
        //{
        //    Reply reply = replyDAL.FindBy(x => x.CommentId == commentId).ToList().LastOrDefault();

        //    string userName = HttpContext.Current.User.Identity.Name;
        //    ReplyVM vm = new ReplyVM
        //    {
        //        Id = reply.Id,
        //        Text = reply.Text,
        //        UserName = userName,
        //        ReplyDate = reply.ReplyDate,
        //        ImagePath = GetUserImagePathForReply(reply.UserId)

        //    };
        //    return vm;
        //}
        public Reply ReplyComment(string replyText, int commentId, long userId)
        {
            Reply reply = new Reply();
            reply.UserId = userId;
            reply.Text = replyText;
            reply.ReplyDate = DateTime.Now;
            reply.CommentId = commentId;
            replyDAL.Add(reply);
             return reply;
        }
    }
}
