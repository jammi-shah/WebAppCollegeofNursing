using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Repository;
using ModelLayer;

namespace BLL
{
    public class StudentBLL
    {
        GenericRepository<UserAccount> _userAccount = new GenericRepository<UserAccount>();
        GenericRepository<Department> _department = new GenericRepository<Department>();
        public IEnumerable<Department> GetDepartments()
        {
            return _department.GetAll().OrderByDescending(x => x.Id);
        }

        public UserAccount GetStudentByUserCode(string userCode)
        {
           return _userAccount.FindBy(x=>x.UserCode==userCode).FirstOrDefault();
            
        }

        public int UpdateStudent(UserAccount user)
        {
            return _userAccount.Update(user);
        }
    }
}
