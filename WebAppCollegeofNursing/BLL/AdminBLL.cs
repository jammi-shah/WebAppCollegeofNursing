using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Repository;
using ModelLayer;

namespace BLL
{
    public class AdminBLL
    {
        IGenericRepository<Department> _department = new GenericRepository<Department>();
        IGenericRepository<UserAccount> _userAccount = new GenericRepository<UserAccount>();
        IGenericRepository<Employee> _employee = new GenericRepository<Employee>();

        ///department related work
        public IEnumerable<Department> GetDepartments()
        {
            return _department.GetAllIQueryable().OrderByDescending(x => x.Id);
        }

        public Department CreateDepartment(Department department)
        {
            bool isavail = _department.IsExist(x => x.DeptName == department.DeptName);
            if (!isavail)
            {
                department.DeptName = department.DeptName.ToUpper();
                _department.Add(department);
                return department;
            }
            department.Id = -1;
            return department;

        }
        public int EditDepartment(Department department)
        {
            bool isavail = _department.IsExist(x => x.DeptName == department.DeptName);
            if (!isavail)
            {
                return _department.Update(department);
            }
            return -1;

        }

        //employee related work
        public string CreateEmployee(UserAccount userAccount)
        {

            UserAccount acc = new UserAccount();

            if (userAccount.UserRole == "HOD")
            {
               // _department.FindBy(x => x.Id==userAccount.DepartmentId).FirstOrDefault().UserAccounts.Any(x=>x.UserRole=="HOD");
                 acc = _userAccount.FindBy(x => x.UserCode == userAccount.UserCode || x.Email == userAccount.Email ||(x.UserRole=="HOD"&& x.DepartmentId==userAccount.DepartmentId)).FirstOrDefault();
            }
            else
            {
                 acc = _userAccount.FindBy(x => x.UserCode == userAccount.UserCode || x.Email == userAccount.Email).FirstOrDefault();
            }
            if (acc == null)
            {

                userAccount.IsActive = true;
                userAccount.ImagePath = "~/Images/DP/default.jpg";
                userAccount.Salt = EncryptPassword.CreateSalt();
                userAccount.Password = EncryptPassword.CreatePasswordHash(userAccount.Email, userAccount.Salt);
                int returnValue = _userAccount.Add(userAccount);
                if (returnValue > 0)
                {
                    EmailBLL.SendUserDetails(userAccount.Email, userAccount.UserCode, userAccount.Name);
                }

                return "Success";
            }
            if(acc.Email==userAccount.Email)
            {
                return "EmailExists";
            }
            if (acc.UserCode == userAccount.UserCode)
            {
                return "CodeExists";
            }
            else
              return  "HODExists";
            

        }
        
        public IEnumerable<UserAccount> GetAllEmployees()
        {
            return _userAccount.FindBy(x => x.UserRole != "Student");
        }
        public IEnumerable<UserAccount> GetStudents()
        {
            return _userAccount.FindBy(x => x.UserRole == "Student");
        }

        public int EditEmployee(UserAccount account)
        {

             _userAccount.Update(account);
            return _employee.Update(account.Employee);
        }

        public List<UserAccount> GetEmployeeById(int id)
        {
            return _userAccount.FindByWithMultipleIncludes(x=>x.Id==id,"Employee", "Department");
        }

        public int DeleteEmployee(int id)
        {
            if(_employee.Delete(_employee.GetById(id))>0)
            {
               return _userAccount.Delete(_userAccount.GetById(id));
            }
            return -1;
        }

        /////student related work
        //public IEnumerable<UserAccount> GetAllStudents(int deptId, string currentClass)
        //{
        //    IEnumerable<UserAccount> users= _userAccount.FindByWithMultipleIncludes(x=>x.DepartmentId==deptId && x.Student.CurrentClass==currentClass,"Student", "Department");
        //    return users;
        //}

        public int EditStudent(UserAccount account)
        {
            return _userAccount.Update(account);
        }

        public List<UserAccount> GetStudentById(int id)
        {
            return _userAccount.FindByWithMultipleIncludes(x => x.Id == id, "Student", "Department");
        }

        public int DeleteStudent(int id)
        {
            if (_employee.Delete(_employee.GetById(id)) > 0)
            {
                return _userAccount.Delete(_userAccount.GetById(id));
            }
            return -1;
        }

        public IEnumerable<UserAccount> GetBlockedStudents()
        {
            return _userAccount.FindBy(x => x.IsActive == true && x.UserRole == "Student");
        }


        ///block or unblock students and employees
        public int BlockUnblock(long id)
        {
            UserAccount user=_userAccount.FindBy(x=>x.Id==id).FirstOrDefault();
            user.IsActive = !user.IsActive;
            return _userAccount.Update(user);
        }

    }
}
