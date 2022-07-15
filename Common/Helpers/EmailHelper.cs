using System.Net.Mail;
using System.Net;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using Common.ConfigClasses;
using Common.ServiceRegistrationAttributes;

namespace Common.Helpers
{
    [ScopedRegistration]
    public class EmailHelper 
    {
         
        private EmailConfiguration _config;

        public EmailHelper(EmailConfiguration config)
        {
            _config = config;
        }

        public MailMessage CreateEmail(string email, string subject, string body)
        {

            MailMessage mailMessage = new();
            mailMessage.From = new MailAddress(_config.CompanyEmail);
            mailMessage.To.Add(email);
            mailMessage.Subject = subject;
            mailMessage.Body = body;

            return mailMessage;
        }
        public void SendEmail(MailMessage mailMessage)
        {
            using ( SmtpClient smtp = new(_config.Smpt, _config.Port))
            {
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(_config.CompanyEmail, _config.CompanyEmailPassword);
                smtp.EnableSsl = true;

                smtp.Send(mailMessage);
            }
        }

    }
}
