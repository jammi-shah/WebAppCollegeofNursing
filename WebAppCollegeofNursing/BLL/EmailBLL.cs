using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BLL
{
    public class EmailBLL
    {
        public static void SendEmail(string guid, string Email, string userCode)
        {
            MailMessage mail = new MailMessage("jammishah@gmail.com", Email);
            mail.Subject = "Change password for Nursing College";
            string link = HttpContext.Current.Request.Url.AbsoluteUri.Replace(HttpContext.Current.Request.Url.PathAndQuery, "/Account/ResetPassword?guid=" + guid);
            StringBuilder sbBody = new StringBuilder();
            sbBody.Append("Dear " + userCode + "<br/><br/>");
            sbBody.Append("Please Click on the following Link To change your password <br/><br/>");
            sbBody.Append(link + " <br/><br/>");
            mail.Body = sbBody.ToString();
            mail.IsBodyHtml = true;
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.Credentials = new System.Net.NetworkCredential("jammishah@gmail.com", "Jammi@321321"); 
            client.EnableSsl = true;
            client.Send(mail);
        }
        public static void SendUserDetails(string email, string userCode,string name)
        {
            MailMessage mail = new MailMessage("jammishah@gmail.com", email);
           
            mail.Subject = "Your User Details with Nursing College is ";
            
            string link = HttpContext.Current.Request.Url.AbsoluteUri.Replace(HttpContext.Current.Request.Url.PathAndQuery, "/Account/Login");
            StringBuilder sbBody = new StringBuilder();
            sbBody.Append("Dear " + name + "<br/><br/>");
            sbBody.Append("Your Account Details are mentioned below <br/><br/>");
            sbBody.Append("UserName : "+userCode+"<br/>");
            sbBody.Append("Password : " + email + "<br/><br/>");
            sbBody.Append("This password is not strong please click on the following link to login and then change password <br/><br/>");
            sbBody.Append(link);

            mail.Body = sbBody.ToString();
            mail.IsBodyHtml = true;

            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.Credentials = new System.Net.NetworkCredential("jammishah@gmail.com", "Jammi@321321");
            client.EnableSsl = true;
            client.Send(mail);
        }
    }
}
