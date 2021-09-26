using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skill.repository.Entity
{  
   [Table("Global_Config")]
   public class GlobalConfig : IBaseEntity
   {
      public Guid Id { get; set; }

      [StringLength(20)]     
      public string Catagory { get; set; }

      [StringLength(50)]
      public string Name { get; set; }

      [StringLength(10)]
      public string ParametType { get; set; }

      [StringLength(50)]
      public string Value { get; set; }

      [StringLength(50)]
      public string DefaultValue {get;set;}
   }
}
