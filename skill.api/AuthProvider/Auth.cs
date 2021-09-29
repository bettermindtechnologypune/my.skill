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

namespace skill.AuthProvider
{
   public class Auth : IAuth
   {
      private readonly IConfiguration _configuration;
      IUserIdentityRepository _userIdentityRepository;
      private readonly IEmailSettingsRepository _emailSettingsRepository;
      public Auth(IConfiguration configuration, IUserIdentityRepository userIdentityRepository, IEmailSettingsRepository emailSettingsRepository)
      {
         _configuration = configuration;
         _userIdentityRepository = userIdentityRepository;
         _emailSettingsRepository = emailSettingsRepository;
      }
      public async Task<AuthResponseModel> Authentication(string username, string password)
      {
         AuthResponseModel authResponseModel = null;
         var key = await _emailSettingsRepository.GetSymmetricKey();
         var dpass = AesOperation.EncryptString(key, password);
         var user =await _userIdentityRepository.GetUserIdentityByEmail(username, dpass);

         if (user != null)
         {

            // 1. Create Security Token Handler
            var tokenHandler = new JwtSecurityTokenHandler();

            // 2. Create Private Key to Encrypted
            var tokenKey = Encoding.ASCII.GetBytes(_configuration["Jwt:key"]);

            //3. Create JETdescriptor
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
               Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()), new Claim("orgId", user.OrgId.ToString()), new Claim("userType", user.UserType.ToString()) }),
               Issuer = _configuration["Jwt:Issuer"],
               Audience = _configuration["Jwt:Audience"],
               Expires = DateTime.UtcNow.AddHours(1),
               SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };
            //4. Create Token
            var token = tokenHandler.CreateToken(tokenDescriptor);

            // 5. Return Token from method
            authResponseModel = new AuthResponseModel
            {
               Token = tokenHandler.WriteToken(token),
               UserType = user.UserType
            };
            return authResponseModel;
         }
         return null;
      }
   }
}
