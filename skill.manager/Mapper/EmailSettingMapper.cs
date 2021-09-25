using skill.common.Model;
using skill.repository.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skill.manager.Mapper
{
   public class EmailSettingMapper
   {
      public static EmailSettingEntity ToEntity(EmailSetting resource)
      {
         if (resource == null)
            return null;

         EmailSettingEntity entity = new EmailSettingEntity
         {
            DisplayName = resource.DisplayName,
            Host = resource.Host,
            Mail = resource.Mail,
            Password = resource.Password,
            Port = resource.Port
         };

         return entity;
      }

      public static EmailSetting ToResource(EmailSettingEntity entity)
      {
         if (entity == null)
            return null;

         EmailSetting resource = new EmailSetting
         {
            DisplayName = entity.DisplayName,
            Host = entity.Host,
            Mail = entity.Mail,
            Password = entity.Password,
            Port = entity.Port
         };

         return resource;
      }
   }
}
