using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLayer;
using DAL.Repository;
using System.IO;
using System.Web;

namespace BLL
{
   public class ProfileBLL
    {
        IGenericRepository<UserAccount> _profileDal = new GenericRepository<UserAccount>();
        IGenericRepository<Student> _studentDal = new GenericRepository<Student>();
        IGenericRepository<Employee> _employeeDal = new GenericRepository<Employee>();

        public UserAccount Profile(long id)
        {
            return _profileDal.FindBy(x => x.Id == id).SingleOrDefault();
        }
        public int EditEmpProfile(UserAccount account)
        {
            if (account.FileUpload != null)
            {
                if (account.ImagePath != "~/Images/DP/default.jpg")
                {
                    File.Delete(HttpContext.Current.Server.MapPath(account.ImagePath));

                }
                account.ImagePath = CommonBLL.FileProfileUpload(account.FileUpload, "~/Images/DP/");
            }
            _employeeDal.Update(account.Employee);
            return _profileDal.Update(account);
        }
        public int EditProfile(UserAccount account)
        {
            if(account.FileUpload != null)
            {
                if(account.ImagePath!="~/Images/DP/default.jpg")
                {
                    File.Delete(HttpContext.Current.Server.MapPath(account.ImagePath));
                   
                }
                account.ImagePath = CommonBLL.FileProfileUpload(account.FileUpload, "~/Images/DP/");
            }
            _studentDal.Update(account.Student);
            return _profileDal.Update(account);
        }
    }
}
