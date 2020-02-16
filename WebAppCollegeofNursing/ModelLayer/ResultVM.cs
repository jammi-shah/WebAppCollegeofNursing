using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer
{
    public class ResultVM
    {
          public long Id { get; set; }
        public string RollNo { get; set; }
        public string Name { get; set; }
        public Nullable<int> SubId { get; set; }
        public Nullable<long> StudentId { get; set; }
        public Nullable<double> Paper1 { get; set; }
        public Nullable<double> Paper2 { get; set; }
        public Nullable<double> Paper3 { get; set; }
        public Nullable<double> ModelPaper { get; set; }
        public Nullable<double> PPT { get; set; }
        public Nullable<double> Assignment { get; set; }
        public Nullable<double> ClassPresentation { get; set; }
        public Nullable<double> Attendence { get; set; }
    }
}
