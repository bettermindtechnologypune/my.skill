using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skill.common.Model
{
  public  class RatingResource
   {
      
      public Guid Id { get; set; }

      public string Name { get; set; }

      [Required]
      public Guid TaskId { get; set; }

      [Required]
      public Guid EmpId { get; set; }

      public int Rating { get; set; }

      public int ManagerRating { get; set; }

   }
}
