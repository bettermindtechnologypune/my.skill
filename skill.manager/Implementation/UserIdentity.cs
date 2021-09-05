using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace skills.manager.AuthProvider
{
   public class UserIdentity
   {
      IConfiguration _configuration;
      public UserIdentity(IConfiguration configuration)
      {
         _configuration = configuration;
      }



   }
}
