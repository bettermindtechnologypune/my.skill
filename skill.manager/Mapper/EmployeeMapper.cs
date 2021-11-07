using skill.common.Model;
using skill.repository.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skill.manager.Mapper
{
   public class EmployeeMapper
   {
      public static EmployeeEntity ToEntity(EmployeeResource resource)
      {
         if (resource == null)
         {
            return null;
         }
         EmployeeEntity entity = new EmployeeEntity()
         {
            Age = resource.Age,
            BUID = resource.BUID,            
            ContactNumber = resource.ContactNumber,
            Email = resource.Email,
            DepartmentId = resource.DepartmentId,
            DOB = resource.DOB,
            DOJ = resource.DOJ,
            Education = resource.Education,
            FirstName = resource.FirstName,
            Grade = resource.Grade,
            IsManager = resource.IsManager,
            LastName = resource.LastName,
            ManagerId = resource.ManagerId,           
            OrgEmpId = resource.OrgEmpId,
            Id = resource.Id ,
            Address = resource.Address,
            City = resource.City,
            State = resource.State
         };

         return entity;
      }

      public static EmployeeResource ToResource(EmployeeEntity entity)
      {
         if (entity == null)
         {
            return null;
         }
         EmployeeResource resource = new EmployeeResource()
         {
            Age = entity.Age,
            BUID = entity.BUID,           
            ContactNumber = entity.ContactNumber,
            Email = entity.Email,
            DepartmentId = entity.DepartmentId,
            DOB = entity.DOB,
            DOJ = entity.DOJ,
            Education = entity.Education,
            FirstName = entity.FirstName,
            Grade = entity.Grade,
            IsManager = entity.IsManager,
            LastName = entity.LastName,
            ManagerId = entity.ManagerId,           
            OrgEmpId = entity.OrgEmpId,
            Id = entity.Id,
            Address = entity.Address,
            City =entity.City,
            State = entity.State
         };

         return resource;
      }
   }
}
      