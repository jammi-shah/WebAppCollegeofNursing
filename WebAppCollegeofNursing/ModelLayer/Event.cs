using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ModelLayer
{
    public partial class Event
    {
        public int Id { get; set; }
        [Required,Display(Name ="Date")]
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> EventDate { get; set; }
        [Required]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        [Display(Name ="File")]
        public string ImagePath { get; set; }
        public HttpPostedFileBase File { get; set; }
        public Nullable<bool> IsActive { get; set; }
    }
}
