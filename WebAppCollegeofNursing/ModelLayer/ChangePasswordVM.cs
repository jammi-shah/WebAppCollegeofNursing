using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer
{
   public class ChangePasswordVM
    {
        [Required(ErrorMessage ="Enter OLD Password")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "Enter new Password")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Enter Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("NewPassword",ErrorMessage ="New Password and ConfirmPassword do not match")]
        public string ConfirmPassword { get; set; }
    }
}
