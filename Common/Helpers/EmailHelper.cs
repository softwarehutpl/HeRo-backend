using System.Net.Mail;
using System.Net;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using Common.ConfigClasses;

namespace Common.Helpers
{
    public class EmailHelper 
    {
         
        private EmailConfiguration _config;

        public EmailHelper(EmailConfiguration config)
        {
            _config = config;
        }

        private MailMessage CreateEmail(string email)
        {

            MailMessage mailMessage = new();
            mailMessage.From = new MailAddress(_config.CompanyEmail);
            mailMessage.To.Add("mrmaselko83@gmail.com");
            mailMessage.Subject = _config.Subject;
            mailMessage.Body = _config.Body;

            return mailMessage;
        }
        public void SendEmail(string email)
        {
            MailMessage mailMessage = CreateEmail(email);

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
