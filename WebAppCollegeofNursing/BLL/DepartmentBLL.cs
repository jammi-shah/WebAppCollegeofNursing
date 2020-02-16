using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Repository;
using ModelLayer;

namespace BLL
{
   public class DepartmentBLL
    {
        IGenericRepository<Department> _department = new GenericRepository<Department>();
        public IEnumerable<Department> GetDepartments()
        {
            return _department.GetAll().OrderByDescending(x => x.Id);
        }

        public int CreateDepartment(Department department)
        {
            bool isavail = _department.IsExist(x => x.DeptName == department.DeptName);
            if (!isavail)
            {
                department.DeptName = department.DeptName.ToUpper();
                return _department.Add(department);
            }
            return -1;

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
    }
}
