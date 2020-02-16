using DAL.Repository;
using ModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BLL
{
   public class SubjectBLL
    {
        IGenericRepository<Subject> _subjectDAL = new GenericRepository<Subject>();
        IGenericRepository<SubjectAllocation> _subjectAllocDAL = new GenericRepository<SubjectAllocation>();
        IGenericRepository<UserAccount> _employeeDal = new GenericRepository<UserAccount>();
        IGenericRepository<Student> _studentDAL = new GenericRepository<Student>();
        IGenericRepository<Attendance> _attendanceDal = new GenericRepository<Attendance>();
        public int AddSubject(Subject subject)
        {
            if (_subjectDAL.IsExist(x => x.SubCode == subject.SubCode))
            {
                return -1;
            }
            return _subjectDAL.Add(subject);
        }
        public int EditSubject(Subject subject)
        {
            if (_subjectDAL.IsExist(x => x.SubCode == subject.SubCode && x.Id !=subject.Id))
            {
                return -1;
            }
            return _subjectDAL.Update(subject);
        }
        public List<Subject> GetAllSubjects(int deptId)
        {
            return _subjectDAL.FindBy(x=>x.DepartmentId==deptId && x.IsDeleted==false).ToList();
        }
        //public List<SubjectResultVM> GetSubjectForResult(int deptId)
        //{
        //    return _subjectDAL.FindBy(x => x.DepartmentId == deptId && x.IsDeleted == false).Select(x => new SubjectResultVM { Id = x.Id, SubName = x.SubName };);
        //}
        public int DeleteSubject(Subject subject)
        {
            subject.IsDeleted = true;
            return _subjectDAL.Update(subject);
        }
        public Subject GetSubjectById(int id)
        {
            return _subjectDAL.GetById(id);
        }
        public List<UserAccount> GetFaculty()
        {
           
            return _employeeDal.FindBy(x => x.UserRole=="Faculty" || x.UserRole=="HOD" ).ToList();
        }
        public List<SubjectAllocatedVM> GetSubjectAllocation(int dept)
        {

            //int? dept=  // _employeeDal.FindBy(x => x.UserCode == userCode).FirstOrDefault().DepartmentId;


           List<SubjectAllocation> subjectAllocation = _subjectAllocDAL.FindBy(x=>x.Subject.DepartmentId==dept).ToList();
            List<SubjectAllocatedVM> vmList = new List<SubjectAllocatedVM>();
            foreach (SubjectAllocation item in subjectAllocation)
            {
                SubjectAllocatedVM vm = new SubjectAllocatedVM();
                vm.Id = item.Id;
                vm.StartPeriod = item.StartPeriod;
                vm.EndPeriod = item.EndPeriod;
                vm.Batch = item.Batch;
                vm.Year = item.Subject.ClassInfo.ClassName;
                vm.SubjectName = item.Subject.SubName;
                vm.Teacher = item.Employee.UserAccount.Name;
                vm.SubjectId = item.SubjectId;
                vmList.Add(vm);
            }
            return vmList;


        }
        public int AllocateSubject(SubjectAllocation subAlloc)
        {
            if(_subjectAllocDAL.IsExist(x=>x.EmpId==subAlloc.EmpId && x.EndPeriod < subAlloc.StartPeriod && subAlloc.EndPeriod <= x.StartPeriod))
            {
                return -1;
            }
            HttpCookie cookie = HttpContext.Current.Request.Cookies["UserData"];
           int deptId= Convert.ToInt32(cookie["DeptId"]);
            AddStudentToAttendance(deptId, subAlloc.Batch,subAlloc.SubjectId);
            return _subjectAllocDAL.Add(subAlloc);
        }
        public int AddStudentToAttendance(int deptId, int? batch, int? subId)
        {
            if(_attendanceDal.IsExist(x=>x.SubjectId == subId))
            {
                return -1;
            }
             List<Attendance> att=  _studentDAL.FindBy(x => x.UserAccount.DepartmentId == deptId && x.Batch == batch).Select(x=>new Attendance { StudentId=x.Id, SubjectId=subId}).ToList();
            return _attendanceDal.InsertList(att);
           

        }
        public int DeAllocateSubject(int id)
        {

            return _subjectAllocDAL.Delete(_subjectAllocDAL.GetById(id));
        }
        public SubjectAllocation GetSubjectAllocationById(int id)
        {
            return _subjectAllocDAL.GetById(id);
        }
        public int EditAllocation(SubjectAllocation subAlloc)
        {
            if (_subjectAllocDAL.IsExist(x => x.EmpId == subAlloc.EmpId && x.StartPeriod == subAlloc.StartPeriod))
            {
                return -1;
            }
            return _subjectAllocDAL.Update(subAlloc);
        }
        //public IEnumerable<int> GetAllBatches()
        //{

        //    return _studentDAL.FindBy(x => true).OrderBy(x => x.Batch).SelectMany(x => new { });
        //}
        public IEnumerable<int?> GetAllBatches()
        {
            return _studentDAL.FindBy(x => true).Select(x => x.Batch).Distinct();
        }
        public List<Subject> GetAllSubjectsbyEmpId(int empid, int deptId)
        {
            return _subjectDAL.FindBy(x => x.SubjectAllocations.FirstOrDefault().EmpId == empid && x.DepartmentId==deptId).ToList();
        }
        //public List<Subject> GetAllSubjectsbyEmpId(int empid, int deptId)
        //{
        //    return _subjectDAL.FindBy(x => x.SubjectAllocations.FirstOrDefault().EmpId == empid && x.DepartmentId == deptId);
        //}

        public List<Subject> GetAllSubjectsByYear(int year)
        {
            HttpCookie userData = HttpContext.Current.Request.Cookies["UserData"];
            int deptId = Convert.ToInt32(userData["DeptId"]);
            return _subjectDAL.FindBy(x => x.DepartmentId== deptId && x.ClassId == year && x.IsDeleted == false).ToList();
        }
    }
}
