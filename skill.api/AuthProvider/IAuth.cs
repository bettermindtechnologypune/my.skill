using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace skills.AuthProvider
{
   public interface IAuth
   {
      Task<string> Authentication(string username, string password);
   }
}
