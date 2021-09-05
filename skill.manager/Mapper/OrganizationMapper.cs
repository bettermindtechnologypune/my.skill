
using skill.repostory.Entity;
using skills.common.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace skill.manager.Mapper
{
   public class OrganizationMapper
   {
      public static OrganizationEntity ToEntity(OrganizationResource resource)
      {
         if(resource == null)
         {
            return null;
         }
         OrganizationEntity entity = new OrganizationEntity()
         {

         };

         return entity;
      }
   }
}
