using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ModelLayer
{
    public class CommentVM
    {
        public long Id { get; set; }
        public string Text { get; set; }
        public Nullable<long> UserId { get; set; }
        public Nullable<System.DateTime> CommentDate { get; set; }

        public string UserName { get; set; }
        public string ImagePath { get; set; }
    }
}
