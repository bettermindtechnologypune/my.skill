using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skill.repository.Entity
{
   [Table("Employee")]
   public class EmployeeEntity : IBaseEntity
   {
      public Guid Id { get ; set ; }

      [Required]
      public Guid BUID { get; set; }

      [Required]
      public Guid ManagerId { get; set; }

      [Required]
      public Guid DepartmentId { get; set; }

      [Required]
      [StringLength(50)]
      public string FirstName { get; set; }

      [Required]
      [StringLength(50)]
      public string LastName { get; set; }

      [Required]
      [StringLength(50)]
      public string Email { get; set; }

      [Required]
      [StringLength(50)]
      public string ContactNumber { get; set; }

      [Required]
      [StringLength(50)]
      public string OrgEmpId { get; set; }

      [Required]
      public bool IsManager { get; set; }

      [Required]
      [StringLength(5)]
      public string Grade { get; set; }

      [Required]
      [StringLength(20)]
      public string DOJ { get; set; }

      [Required]
      [StringLength(20)]
      public string DOB { get; set; }

      [Required]
      public int Age { get; set; }

      [Required]
      [StringLength(50)]
      public string Education { get; set; }     

      [StringLength(50)]
      public string CreatedBy { get; set; }

      [StringLength(50)]
      public string? ModifiedBy { get; set; }

      [DataType(DataType.DateTime)]
      public DateTime CreatedDate { get; set; }

      [DataType(DataType.DateTime)]
      public DateTime? ModifiedDate { get; set; }

      [StringLength(50)]
      public string Address { get; set; }

      [StringLength(20)]
      public string City { get; set; }

      [StringLength(20)]
      public string State { get; set; }
   }
}
