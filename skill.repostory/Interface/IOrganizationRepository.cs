﻿using skill.repository.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace skill.repository.Interface
{
   public interface IOrganizationRepository
   {
      Task<bool> InsertAsync(OrganizationEntity entity);
      void Get();
   }
}
