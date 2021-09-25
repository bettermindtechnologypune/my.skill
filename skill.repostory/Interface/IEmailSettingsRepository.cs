using skill.repository.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skill.repository.Interface
{
   public interface IEmailSettingsRepository
   {
      Task<EmailSettingEntity> GetEmailSetting();

      Task<string> GetSymmetricKey();
   }
}
