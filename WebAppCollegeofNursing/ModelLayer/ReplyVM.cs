using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer
{
    public class ReplyVM
    {
        public long Id { get; set; }
        [Required(ErrorMessage = "Enter Comment")]
        public string Text { get; set; }
        //public Nullable<int> CommentId { get; set; }
        public string UserName { get; set; }


        public Nullable<System.DateTime> ReplyDate { get; set; }
        public string FilePath { get; set; }

        [Required(ErrorMessage = "Select Image"), DataType(DataType.ImageUrl)]
        public string ImagePath { get; set; }
        public Nullable<System.DateTime> CommentDate { get; set; }
    }
}
