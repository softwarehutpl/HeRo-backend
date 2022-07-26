using MailKit.Net.Smtp;
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
        private readonly EmailConfiguration _config;

        public EmailHelper(EmailConfiguration config)
        {
            _config = config;
        }

        public EmailHelper()
        {
        }

        public bool CheckIfMailBoxDomainIsValid(string userEmail, out string smtp, out int port, out int imapPort, out string imap)
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
                    imap = "imap.gmail.com";
                    imapPort = (int)ImapPorts.Gmail;

                    return true;
                }

                if (userDomain == PossibleMailBoxDomains.MICROSOFT.ToString()
                    || userDomain == PossibleMailBoxDomains.SOFTWAREHUT.ToString())
                {
                    smtp = "smtp.office365.com";
                    port = (int)PossibleMailBoxDomains.MICROSOFT;
                    imap = "outlook.office365.com";
                    imapPort = (int)ImapPorts.Microsoft;

                    return true;
                }
            }

            smtp = string.Empty;
            imap = string.Empty;
            imapPort = 0;
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

        public void SendCustomEmail(MimeMessage mailMessage, SmtpServerConfig config)
        {
            mailMessage.From.Add(new MailboxAddress(config.FullName, config.MailBoxLogin));

            using (SmtpClient smtp = new())
            {
                try
                {
                    smtp.Connect(config.Smtp, config.SmptPort, true);
                    smtp.Authenticate(config.MailBoxLogin, config.MailBoxPassword);
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