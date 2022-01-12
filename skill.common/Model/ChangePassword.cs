using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skill.common.Model
{
   public class ChangePassword
   {
      public string ResetCode { get; set; }


      public string Email {get;set;}

      [Required]
      public string NewPassword { get; set; }

      [Required]
      public string UserId { get; set; }

      public bool IsFirstTimeChange { get; set; }
   }
}
