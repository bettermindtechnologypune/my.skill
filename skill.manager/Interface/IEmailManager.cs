using skill.common.Model;
using skill.repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skill.manager.Interface
{
   public interface IEmailManager
   {
      Task SendEmailAsync(EmailRequest mailRequest);
   }
}
