using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using DAL.Repository;
using ModelLayer;

namespace BLL
{
    public  class AccountBLL
    {
        GenericRepository<UserAccount> _userAccount = new GenericRepository<UserAccount>();
        GenericRepository<Department> _department = new GenericRepository<Department>();

        public IEnumerable<Department> GetDepartments()
        {
            return _department.GetAll().OrderByDescending(x => x.Id);
        }

        public bool IsAvailable(string value)
        {
            if(_userAccount.FindBy(x => x.UserCode == value || x.Email == value || x.Student.RollNo == value).Any())
            {
                return false;
            }
            return true;
        }

        public int Register(UserAccount userAccount)
        {
            userAccount.IsActive = false;
            userAccount.ImagePath = "~/Images/DP/default.jpg";
            userAccount.UserRole = "Student";
            userAccount.Salt = EncryptPassword.CreateSalt();
            userAccount.Password = EncryptPassword.CreatePasswordHash(userAccount.Password, userAccount.Salt);
            return _userAccount.Add(userAccount);
        }


        public UserAccount Login(Login login)
        {
            UserAccount account = _userAccount.FindBy(x => x.UserCode == login.UserCode).SingleOrDefault();
            if (account != null)
            {
                string newhashedpassword = EncryptPassword.CreatePasswordHash(login.Password, account.Salt);
                if (newhashedpassword == account.Password)
                {
                    return account;
                }
            }
            return null;
        }

        public string GetUserRole(string userCode)
        {
            return _userAccount.FindBy(x => x.UserCode == userCode).SingleOrDefault().UserRole;
        }

        public string ForgetPassword(string guid, string userCode)
        {
            UserAccount account = _userAccount.FindBy(x => x.UserCode == userCode).FirstOrDefault();
            if (account != null)
            {
                account.ResetCode = guid;
                _userAccount.Update(account);
                return account.Email;
            }
            return null;
        }

        public int ResetPassword(ResetPasswordViewModel resetPassword)
        {
            UserAccount account = _userAccount.FindBy(x => x.ResetCode == resetPassword.GUID).SingleOrDefault();
            if (account != null)
            {
                account.Salt = EncryptPassword.CreateSalt();
                account.Password = EncryptPassword.CreatePasswordHash(resetPassword.Password, account.Salt);
                account.ResetCode = "";
                return _userAccount.Update(account);
            }
            return 0;
        }
        public int ChangePassword(ChangePasswordVM changePasswordVM)
        {
            string userCode = HttpContext.Current.User.Identity.Name;
            UserAccount userAccount = _userAccount.FindBy(x => x.UserCode == userCode).FirstOrDefault();
         
            string oldUIPassword = EncryptPassword.CreatePasswordHash(changePasswordVM.OldPassword, userAccount.Salt);
            if(userAccount.Password == oldUIPassword)
            {
                userAccount.Salt = EncryptPassword.CreateSalt();
                userAccount.Password= EncryptPassword.CreatePasswordHash(changePasswordVM.NewPassword, userAccount.Salt);
               return   _userAccount.Update(userAccount);
            }
            return -1;
        }
    }
}
