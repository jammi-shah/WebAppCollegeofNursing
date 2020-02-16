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
    public class AttendanceBLL
    {
        IGenericRepository<Attendance> attendanceDal = new GenericRepository<Attendance>();
        IGenericRepository<AttendanceSheet> attendanceSheetDal = new GenericRepository<AttendanceSheet>();

        public List<StudentAttVM> GetStudents(int SubId, int batch, DateTime date)
        {
            List<Attendance> attList = attendanceDal.FindBy(x => x.SubjectId == SubId && x.Student.Batch == batch).ToList();

            if (!attList.Where(x => x.AttendanceSheets.Any(y => y.AttDate == date)).Any())
            {
                return attList.Select(x => new StudentAttVM
                {
                    Id = x.Id,
                    Name = x.Student.UserAccount.Name,
                    RollNo = x.Student.RollNo
                }).ToList();
            }
            return attList.Select(x => new StudentAttVM
            {
                Id = x.Id,
                Name = x.Student.UserAccount.Name,
                RollNo = x.Student.RollNo,
                AttendanceStatus = x.AttendanceSheets.FirstOrDefault(z => z.AttendanceId == x.Id) == null ? "" : x.AttendanceSheets.FirstOrDefault(y => y.AttendanceId == x.Id && y.AttDate == date).AttStatus
            }).ToList();
        }

        public int AddAttendance(int attendenceId, DateTime AttDate, string AttendanceStatus)
        {
            var att=attendanceSheetDal.FindBy(x => x.AttendanceId == attendenceId && x.AttDate == AttDate);            
            if (att.Any())
            {
                var at=att.FirstOrDefault();
                at.AttendanceId = attendenceId;
                at.AttDate = AttDate;
                at.AttStatus = AttendanceStatus;
                return attendanceSheetDal.Update(at);
            }
            AttendanceSheet attendenceSheet = new AttendanceSheet
            {
                AttendanceId = attendenceId,
                AttDate = AttDate,
                AttStatus = AttendanceStatus,
            };
            return attendanceSheetDal.Add(attendenceSheet);
        }

        public List<AttendanceSheet> GetStudentAttendence(int subId, DateTime fromDate, DateTime toDate)
        {
            HttpCookie userData = HttpContext.Current.Request.Cookies["UserData"];
            long userId= Convert.ToInt64(userData["UserId"]);
            return   attendanceSheetDal.FindBy(x =>x.Attendance.Student.UserAccount.Id== userId && x.Attendance.SubjectId == subId && (x.AttDate >= fromDate && x.AttDate <= toDate)).ToList();
        }

        public dynamic GetAttendance(string rollNo, int batch,int subjectId)
        {
           return attendanceSheetDal.FindBy(x => x.Attendance.Student.RollNo == rollNo && x.Attendance.Student.Batch == batch && x.Attendance.Subject.Id == subjectId).Select(x => new
            {
                RollNo = x.Attendance.Student.RollNo,
                Name = x.Attendance.Student.UserAccount.Name,
                Subject = x.Attendance.Subject.SubName,
                Date = x.AttDate.ToString(),
                Status = x.AttStatus

            });

        }

    }
}
