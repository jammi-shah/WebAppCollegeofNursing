using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer
{
    public class StudentAttVM
    {
        public long Id { get; set; }
        public string  RollNo { get; set; }
        public string  Name { get; set; }
        public string AttendanceStatus { get; set; }
        public DateTime AttDate { get; set; }
    }
}
