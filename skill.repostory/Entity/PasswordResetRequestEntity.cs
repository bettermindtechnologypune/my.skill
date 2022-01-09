using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skill.repository.Entity
{
  public class PasswordResetRequestEntity : IBaseEntity
   {
      /// <summary>
      ///  Unique identifier for a forgotten password reset request
      /// </summary>
      public Guid Id { get; set; }

      /// <summary>
      /// login name of user
      /// </summary>
      public string Login { get; set; }         

      /// <summary>
      /// Unique Identity of user
      /// </summary>
      public Guid UserId { get; set; }

      public DateTime CreatedDate { get ; set; }

      public string ResetCode { get; set; }
   }
}
