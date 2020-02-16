using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLayer;
using DAL.Repository;
using System.Web;

namespace BLL
{
    public class ResultBll
    {
        IGenericRepository<Result> _resultDal = new GenericRepository<Result>();
        IGenericRepository<Subject> _subjectDal = new GenericRepository<Subject>();
        IGenericRepository<Student> _studentDal = new GenericRepository<Student>();
        

        public int AddResult(Result result)
        {
           return _resultDal.Add(result);
        }
        public List<Subject>  GetMySubjects(long empId)
        {
            return _subjectDal.FindBy(x=>x.SubjectAllocations.FirstOrDefault().EmpId==empId).ToList();
        }

        public List<ResultVM> GetStudents(int batch,int subjectId,int deptId)
        {
            List<Student> studs = _studentDal.FindBy(x =>  x.UserAccount.DepartmentId==deptId &&  x.Batch == batch).ToList();
            List<ResultVM> results =new List<ResultVM>();
            foreach(Student stud in studs)
            {
                ResultVM result = new ResultVM();
                result.Name = stud.UserAccount.Name;
                result.RollNo = stud.RollNo;
                result.StudentId = stud.Id;
                result.SubId = subjectId;
                if(stud.Results.Any(x=>x.SubId==subjectId))
                {
                    result.Id = stud.Results.FirstOrDefault(y => y.SubId == subjectId).Id;
                    result.Paper1 = stud.Results.FirstOrDefault(y => y.SubId == subjectId).Paper1;
                    result.Paper2 = stud.Results.FirstOrDefault(y => y.SubId == subjectId).Paper2;
                    result.Paper3 = stud.Results.FirstOrDefault(y => y.SubId == subjectId).Paper3;
                    result.ModelPaper = stud.Results.FirstOrDefault(y => y.SubId == subjectId).ModelPaper;
                    result.Assignment = stud.Results.FirstOrDefault(y => y.SubId == subjectId).Assignment;
                    result.PPT = stud.Results.FirstOrDefault(y => y.SubId == subjectId).PPT;
                    result.ClassPresentation = stud.Results.FirstOrDefault(y => y.SubId == subjectId).ClassPresentation;
                    result.Attendence = stud.Results.FirstOrDefault(y => y.SubId == subjectId).Attendence;
                }
                results.Add(result);
            }
            return results;
            //return _studentDal.FindBy(x =>x.Batch==batch).Select(x=>new ResultVM {
            //    Id = x.Results.FirstOrDefault(y => y.SubId == subjectId).Id,
            //    RollNo =x.RollNo,
            //    Name=x.UserAccount.Name,
            //    StudentId=x.Id,
            //    SubId=subjectId,
            //    Paper1=x.Results.FirstOrDefault(y=>y.SubId==subjectId).Paper1,
            //    Paper2 = x.Results.FirstOrDefault(y => y.SubId == subjectId).Paper2,
            //    Paper3 = x.Results.FirstOrDefault(y=> y.SubId == subjectId).Paper3,
            //    ModelPaper = x.Results.FirstOrDefault(y => y.SubId == subjectId).ModelPaper,
            //    Assignment = x.Results.FirstOrDefault(y => y.SubId == subjectId).Assignment,
            //    PPT = x.Results.FirstOrDefault(y => y.SubId == subjectId).PPT,
            //    ClassPresentation = x.Results.FirstOrDefault(y => y.SubId == subjectId).ClassPresentation,
            //    Attendence = x.Results.FirstOrDefault(y => y.SubId == subjectId).Attendence
                
            //}).ToList();
        }
        public List<ResultVM> GetResult(string rollno,int batch, int subjectId, int deptId)
        {
            List<Student> studs = _studentDal.FindBy(x =>x.RollNo.Contains(rollno)&& x.UserAccount.DepartmentId == deptId && x.Batch == batch).ToList();
            List<ResultVM> results = new List<ResultVM>();
            foreach (Student stud in studs)
            {
                ResultVM result = new ResultVM();
                result.Name = stud.UserAccount.Name;
                result.RollNo = stud.RollNo;
                result.StudentId = stud.Id;
                result.SubId = subjectId;
                if (stud.Results.Any(x => x.SubId == subjectId))
                {
                    result.Id = stud.Results.FirstOrDefault(y => y.SubId == subjectId).Id;
                    result.Paper1 = stud.Results.FirstOrDefault(y => y.SubId == subjectId).Paper1;
                    result.Paper2 = stud.Results.FirstOrDefault(y => y.SubId == subjectId).Paper2;
                    result.Paper3 = stud.Results.FirstOrDefault(y => y.SubId == subjectId).Paper3;
                    result.ModelPaper = stud.Results.FirstOrDefault(y => y.SubId == subjectId).ModelPaper;
                    result.Assignment = stud.Results.FirstOrDefault(y => y.SubId == subjectId).Assignment;
                    result.PPT = stud.Results.FirstOrDefault(y => y.SubId == subjectId).PPT;
                    result.ClassPresentation = stud.Results.FirstOrDefault(y => y.SubId == subjectId).ClassPresentation;
                    result.Attendence = stud.Results.FirstOrDefault(y => y.SubId == subjectId).Attendence;
                }
                results.Add(result);
            }
            return results;
        }
            public int AddResults(List<Result> results)
        {
            return _resultDal.InsertList(results);
        }
        public int EditResult(Result result)
        {
            return _resultDal.Update(result);
        }
        //public int EditResults(List<Result> results)
        //{
        //    return _resultDal.(results);
        //}

        public bool ResultExists(int? subId, long? studId)
        {
            return _resultDal.IsExist(x=>x.SubId==subId&&x.StudentId==studId);
        }

        public List<Result> GetResultByYearAndStudent(string year,long studentId)
        {
             var res=_resultDal.FindBy(x => x.StudentId == studentId && x.Subject.ClassInfo.ClassName==year);
            return res.ToList();
        }
    }
}
