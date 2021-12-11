using skill.common.Enum;
using skill.common.Helper;
using skill.common.Model;
using skill.common.Operation;
using skill.common.TenantContext;
using skill.manager.Interface;
using skill.manager.Mapper;
using skill.manager.Validator.Interface;
using skill.repository.Entity;
using skill.repository.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skill.manager.Implementation
{
   public class EmployeeManagerImpl : IEmployeeManager
   {
      ITenantContext _tenantContext;
      IEmployeeRepository _employeeRepository;
      IUserIdentityRepository _userIdentityRepository;
      private readonly IEmailSettingsRepository _emailSettingsRepository;
      private readonly IEmailManager _emailManager;
      IEmployeeValidator _validator;
      

      public EmployeeManagerImpl(IEmployeeRepository employeeRepository, ITenantContext tenantContext,
         IUserIdentityRepository userIdentityRepository, IEmployeeValidator validator,
         IEmailSettingsRepository emailSettingsRepository, IEmailManager emailManager)
      {
         _employeeRepository = employeeRepository;
         _tenantContext = tenantContext;
         _validator = validator;
         _userIdentityRepository = userIdentityRepository;
         _emailSettingsRepository = emailSettingsRepository;
         _emailManager = emailManager;
      }


      Guid OrgId
      {
         get
         {
            return _tenantContext == null ? Guid.Empty : _tenantContext.OrgId;
         }
      }

      Guid UserId
      {
         get
         {
            return _tenantContext == null ? Guid.Empty : _tenantContext.UserId;
         }
      }

      Guid BUID
      {
         get
         {
            return _tenantContext == null ? Guid.Empty : _tenantContext.BUId;
         }
      }

      public async Task<EmployeeResource> Create(EmployeeResource resource)
      {
         var errors = _validator.Validate(resource);
         if (errors.Any())
         {
            throw new ValidationException(string.Join(",", errors));
         }

         resource.Id = Guid.NewGuid();
         resource.BUID = BUID;
         var entity = EmployeeMapper.ToEntity(resource);
         entity.CreatedBy = UserId.ToString();
         entity.CreatedDate = DateTime.UtcNow;
         //if(resource.IsManager == false)
         //{
         //   var managerId = _employeeRepository.GetListByDepartmentId(resource.DepartmentId, true).Select(x => x.Id).FirstOrDefault();
         //   if (managerId != Guid.Empty)
         //   {
         //      entity.ManagerId = managerId;
         //   }
         //}
        
         var result = await _employeeRepository.InsertAsync(entity);
         if (result !=null && result.Id !=Guid.Empty)
         {
            var key = await _emailSettingsRepository.GetSymmetricKey();
            var plainText = AesOperation.RandomPassword();
            var encPassword = AesOperation.EncryptString(key, plainText);
            var userEntity = new UserIdentityEntity
            {
               PhoneNumber = entity.ContactNumber,
               CreatedBy = UserId,
               CreatedDate = DateTime.UtcNow,
               Email = entity.Email,
               Id = Guid.NewGuid(),
               FullName = $"{entity.FirstName} {entity.LastName}",
               OrgId = OrgId,
               Name = entity.FirstName,
               IsOrgAdmin = false,
               UserType = resource.IsManager == true ? UserType.Manager:UserType.Worker,
               Password = encPassword,
               IsDeleted = false,
               IsLoginLocked = false,
               BUID = BUID,              
            };
            

            await _userIdentityRepository.CreateUserIdentity(userEntity);

            //if (resource.IsManager)
            //{
            //   var employees = _employeeRepository.GetListByDepartmentId(resource.DepartmentId, false);
            //   if (employees.Any())
            //   {
            //      foreach (var emp in employees)
            //      {
            //         if (emp.ManagerId == Guid.Empty && emp.Id != resource.Id)
            //         {
            //            emp.ManagerId = resource.Id;
            //         }
            //      }

            //      await _employeeRepository.UpdateListAsync(employees);
            //   }
            //}


            var emailRequest = new EmailRequest();

            emailRequest.ToEmail = entity.Email;
            emailRequest.Subject = "Default Password for Skill Application";
            var template = $"<div>Hi {entity.FirstName},<br> <div>Your username is {entity.Email} and Password for skill application login is {plainText} . <br>>Please do not share the password with any one, <br>Thank you!!< div></div>";
            emailRequest.Body = template;


            await _emailManager.SendEmailAsync(emailRequest);

            return resource;
         }

         return null;
      }

      public PagedResult<EmployeeResource> GetListByManagerId(Guid managerId, int pageNumber, int pageSize, string searchText)
      {
         try
         {
            List<EmployeeResource> employeeList = null;
            PagedResult<EmployeeResource> pagedResult = null;
            var pageEntityResult = _employeeRepository.GetListByManagerId(managerId, pageNumber,pageSize,searchText);
            if(pageEntityResult.Results.Any())
            {
               employeeList = new List<EmployeeResource>();
               foreach (var employeeEntity in pageEntityResult.Results)
               {
                  var employeeResource = EmployeeMapper.ToResource(employeeEntity);
                  employeeList.Add(employeeResource);
               }

               pagedResult = new PagedResult<EmployeeResource>();
               pagedResult.Results = employeeList;
               pagedResult.PageCount = pageEntityResult.PageCount;
               pagedResult.PageSize = pageEntityResult.PageSize;
               pagedResult.CurrentPage = pageEntityResult.CurrentPage;
               pagedResult.RowCount = pageEntityResult.RowCount;
            }
           
            return pagedResult;

         }
         catch
         {
            throw;
         }
      }

      public List<EmployeeResource> GetListByDepartmentId(Guid depatmentId)
      {
         var entityList = _employeeRepository.GetListByDepartmentId(depatmentId, true);
         List<EmployeeResource> employeeList = null;
         if(entityList.Any())
         {
            employeeList = new List<EmployeeResource>();
            foreach (var entity in entityList)
            {
               var employeeResource = EmployeeMapper.ToResource(entity);
               employeeList.Add(employeeResource);
            }
         }

         return employeeList;
      }
   }
}
