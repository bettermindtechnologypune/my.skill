using Microsoft.Extensions.Configuration;
using skill.repository.Entity;
using skill.repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skill.repository.Implementation
{
    public class PasswordResetRequestRepository : BaseRepository<PasswordResetRequestEntity>, IPasswordResetRequestRepository
   {
      public PasswordResetRequestRepository(IConfiguration configuration, ApplicationDBContext applicationDBContext) : base(applicationDBContext, configuration)
      {

      }
     

      public void DeleteByLogin(string email)
      {
         var values = _entities.Where(x => x.Login == email).AsEnumerable().ToList();

         foreach(var val in values)
         {
            _entities.Remove(val);
         }        
      }

      public PasswordResetRequestEntity GetPasswordResetRequestByResetCode(string resetCode ,string email)
      {
         return _entities.FirstOrDefault(s => s.ResetCode == resetCode && s.Login == email);
      }

   }
}
