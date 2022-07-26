using MailKit;
using MailKit.Net.Smtp;
using MailKit.Net.Imap;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using Common.ConfigClasses;
using Common.ServiceRegistrationAttributes;
using MimeKit;
using MimeKit.Text;
using Common.Enums;

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

        public bool CheckIfMailBoxDomainIsValid(string userEmail, out string smtp, out int port)
        {
            System.Net.Mail.MailAddress address = new(userEmail);
            string host = address.Host;
            string[]? split = host.Split(".");

            string userDomain = split[0].ToUpper();

            bool check = Enum.IsDefined(typeof(PossibleMailBoxDomains), userDomain);

            if (check)
            {
                if (userDomain == PossibleMailBoxDomains.GMAIL.ToString())
                {
                    smtp = "smtp.gmail.com";
                    port = (int)PossibleMailBoxDomains.GMAIL;

                    return true;
                }

                if (userDomain == PossibleMailBoxDomains.MICROSOFT.ToString()
                    || userDomain == PossibleMailBoxDomains.SOFTWAREHUT.ToString())
                {
                    smtp = "smtp.office365.com";
                    port = (int)PossibleMailBoxDomains.MICROSOFT;

                    return true;
                }
            }

            smtp = string.Empty;
            port = 0;

            return false;
        }

        public MimeMessage CreateEmail(string to, string subject, string body)
        {
            MimeMessage mailMessage = new();
            mailMessage.To.Add(MailboxAddress.Parse(to));
            mailMessage.Subject = subject;
            mailMessage.Body = new TextPart(TextFormat.Plain) { Text = body };

            return mailMessage;
        }

        public void SendPredefinedEmail(MimeMessage mailMessage)
        {
            mailMessage.From.Add(new MailboxAddress("HeRo", _config.CompanyEmail));

            using (SmtpClient smtp = new())
            {
                try
                {
                    smtp.Connect(_config.SmptGmail, _config.SmptPortGmail, true);
                    smtp.Authenticate(_config.CompanyEmail, _config.CompanyEmailPassword);
                    smtp.Send(mailMessage);
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    smtp.Disconnect(true);
                }
            }
        }

        public void SendCustomEmail(MimeMessage mailMessage, CustomEmailConfig config)
        {
            mailMessage.From.Add(new MailboxAddress(config.FullName, config.Email));

            using (SmtpClient smtp = new())
            {
                try
                {
                    smtp.Connect(config.Smtp, config.Port, true);
                    smtp.Authenticate(config.Email, config.Password);
                    smtp.Send(mailMessage);
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    smtp.Disconnect(true);
                }
            }
        }
    }
}