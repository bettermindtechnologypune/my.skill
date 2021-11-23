using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using skill.common.ResponseModel;
using skill.repository.Interface;
using skill.common.Operation;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using skill.common.Enum;
using skill.repository.Entity;

namespace skill.AuthProvider
{
   public class Auth : IAuth
   {
      private readonly IConfiguration _configuration;
      IUserIdentityRepository _userIdentityRepository;
      private readonly IEmailSettingsRepository _emailSettingsRepository;
      IBusinessUnitRepository _businessUnitRepository;
      IEmployeeRepository _employeeRepository;

      public Auth(IConfiguration configuration, IUserIdentityRepository userIdentityRepository, IEmailSettingsRepository emailSettingsRepository,
         IBusinessUnitRepository businessUnitRepository, IEmployeeRepository employeeRepository)
      {
         _configuration = configuration;
         _userIdentityRepository = userIdentityRepository;
         _emailSettingsRepository = emailSettingsRepository;
         _businessUnitRepository = businessUnitRepository;
         _employeeRepository = employeeRepository;
      }
      public async Task<AuthResponseModel> Authentication(string username, string password)
      {

         var key = await _emailSettingsRepository.GetSymmetricKey();
         var dpass = AesOperation.EncryptString(key, password);
         var user = await _userIdentityRepository.GetUserIdentityByEmail(username, dpass);

         if (user != null)
         {

            // 1. Create Security Token Handler
            var tokenHandler = new JwtSecurityTokenHandler();

            // 2. Create Private Key to Encrypted
            var tokenKey = Encoding.ASCII.GetBytes(_configuration["Jwt:key"]);
            BusinessUnitEntity businessUnitEntity = null;
            EmployeeEntity employeeEntity = null;
            Claim buIdClaim = null;
            if (user.UserType == UserType.Hr_Admin || user.UserType == UserType.Manager || user.UserType == UserType.Worker)
            {
               buIdClaim = new Claim("BUID", user.BUID.ToString());
               if (user.UserType == UserType.Manager || user.UserType == UserType.Worker)
               {
                  businessUnitEntity = await _businessUnitRepository.GetAsync(user.BUID);
                  employeeEntity = _employeeRepository.GetByBUIDAndEmail(user.BUID, user.Email);
               }

            }
            //3. Create JETdescriptor
            var tokenDescriptor = new SecurityTokenDescriptor()
            {

               Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()), new Claim("orgId", user.OrgId.ToString()), new Claim("userType", user.UserType.ToString()), buIdClaim }),
               Issuer = _configuration["Jwt:Issuer"],
               Audience = _configuration["Jwt:Audience"],
               Expires = DateTime.UtcNow.AddHours(1),
               SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };
            //4. Create Token
            var token = tokenHandler.CreateToken(tokenDescriptor);

            // 5. Return Token from method
            AuthResponseModel authResponseModel = new AuthResponseModel
            {
               Token = tokenHandler.WriteToken(token),
               UserType = user.UserType
            };

            if (user.UserType == UserType.Manager || user.UserType == UserType.Worker || user.UserType == UserType.Hr_Admin)
            {
               if (user.UserType == UserType.Hr_Admin)
               {
                  authResponseModel.BUID = user.BUID;
               }
               else
               {
                  authResponseModel.BUID = businessUnitEntity.Id;
                  authResponseModel.BUName = businessUnitEntity.Name;

                  authResponseModel.EmpId = employeeEntity.Id;
                  authResponseModel.EmpName = employeeEntity.FirstName;
               }


            }
            return authResponseModel;
         }
         return null;
      }
   }
}
