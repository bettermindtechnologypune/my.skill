using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using skill.common.Model;
using skill.manager.Interface;
using skill.repository.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace skill.manager.Implementation
{
   public class EmailManagerImpl : IEmailManager
   {
      private readonly IEmailSettingsRepository _emailSettingsRepository;
      
      public EmailManagerImpl(IEmailSettingsRepository emailSettingsRepository)
      {
         _emailSettingsRepository = emailSettingsRepository;
      }

      public async Task SendEmailAsync(EmailRequest mailRequest)
      {
         try
         {
            var emailSettings = await _emailSettingsRepository.GetEmailSetting();
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(emailSettings.Mail);
            email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
            email.Subject = mailRequest.Subject;
            var builder = new BodyBuilder();
            if (mailRequest.Attachments != null)
            {
               byte[] fileBytes;
               foreach (var file in mailRequest.Attachments)
               {
                  if (file.Length > 0)
                  {
                     using (var ms = new MemoryStream())
                     {
                        file.CopyTo(ms);
                        fileBytes = ms.ToArray();
                     }
                     builder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
                  }
               }
            }
            builder.HtmlBody = mailRequest.Body;
            email.Body = builder.ToMessageBody();

            using var smtp = new SmtpClient();
            smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            //smtp.Connect(emailSettings.Host, emailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(emailSettings.Mail, emailSettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
         }
         catch(Exception)
         {
            throw;
         }
      }
   }
}
