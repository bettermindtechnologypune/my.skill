﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace skills.Model
{
   public class Login
   {
      [Required]
      public string UserName { get; set; }
      [Required]
      public string Password { get; set; }
   }
}
