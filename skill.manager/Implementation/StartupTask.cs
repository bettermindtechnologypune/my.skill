using skill.manager.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skill.manager.Implementation
{
   public class StartupTask : IStartupTask
   {
      public StartupTask()
      {
      }

      public Task<bool> Execute()
      {
         throw new NotImplementedException();
      }
   }
}
