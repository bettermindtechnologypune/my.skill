   using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skill.common.CustomException
{
   [Serializable]
   public class InvalidException : Exception
   {
      public InvalidException() { }

      public InvalidException(string name , object propertyName)
          : base(String.Format("Invalid {0}:{1}", propertyName,name))
      {

      }

      public InvalidException(string error)
         : base(String.Format(error))
      {

      }
   }
}
