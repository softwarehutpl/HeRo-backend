using System.Net.Mail;
using System.Net;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;

namespace Common.Helpers
{
    public class EmailHelper 
    {

        private  IConfiguration _config;

        public EmailHelper(IConfiguration config)
        {
            _config = config;
        }

        private MailMessage CreateEmail(string email)
        {
            Dictionary<string,string> companyEmailData = _config.GetSection("CompanyEmailData")
                .GetChildren()
                .ToDictionary(x => x.Key, x => x.Value);

            MailMessage mailMessage = new();
            mailMessage.From = new MailAddress(companyEmailData["CompanyEmail"]);
            mailMessage.To.Add(email);
            mailMessage.Subject = companyEmailData["Subject"];
            mailMessage.Body = companyEmailData["Body"];

            return mailMessage;
        }
        public void SendEmail(string email)
        {
            Dictionary<string, string> companyEmailData = _config.GetSection("CompanyEmailData")
                .GetChildren()
                .ToDictionary(x => x.Key, x => x.Value);

            string port = companyEmailData["Port"];
            int portStringParsed = int.Parse(port);

            MailMessage mailMessage = CreateEmail(email);

            using ( SmtpClient smtp = new(companyEmailData["Smpt"], portStringParsed))
            {
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(companyEmailData["CompanyEmail"], companyEmailData["CompanyEmailPassword"]);
                smtp.EnableSsl = true;

                smtp.Send(mailMessage);
            }
        }

    }
}
