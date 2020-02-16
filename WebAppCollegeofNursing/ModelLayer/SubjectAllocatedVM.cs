using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer
{
    public class SubjectAllocatedVM
    {
        public int Id { get; set; }
        [DataType(DataType.Time)]
        public Nullable<System.TimeSpan> StartPeriod { get; set; }
        [DataType(DataType.Time)]
        public Nullable<System.TimeSpan> EndPeriod { get; set; }
        public Nullable<int> Batch { get; set; }
        public string Year { get; set; }
        [Display(Name ="Subject")]
        public string SubjectName { get; set; }
        public string Teacher { get; set; }
        public int? SubjectId { get; set; }
        
    }
}
