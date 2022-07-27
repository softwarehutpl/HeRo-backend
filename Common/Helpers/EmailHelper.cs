using MailKit.Net.Smtp;
using Common.ServiceRegistrationAttributes;
using MimeKit;
using MimeKit.Text;
using Common.Enums;

namespace Common.Helpers
{
    [ScopedRegistration]
    public class EmailHelper
    {
        public EmailHelper()
        {
        }

        public MimeMessage CreateEmail(string to, string subject, string body)
        {
            MimeMessage mailMessage = new();
            mailMessage.To.Add(MailboxAddress.Parse(to));
            mailMessage.Subject = subject;
            mailMessage.Body = new TextPart(TextFormat.Plain) { Text = body };

            return mailMessage;
        }

        //public void SendPredefinedEmail(MimeMessage mailMessage)
        //{
        //    mailMessage.From.Add(new MailboxAddress("HeRo", _config.CompanyEmail));

        //    using (SmtpClient smtp = new())
        //    {
        //        try
        //        {
        //            smtp.Connect(_config.SmptGmail, _config.SmptPortGmail, true);
        //            smtp.Authenticate(_config.CompanyEmail, _config.CompanyEmailPassword);
        //            smtp.Send(mailMessage);
        //        }
        //        catch (Exception ex)
        //        {
        //            throw;
        //        }
        //        finally
        //        {
        //            smtp.Disconnect(true);
        //        }
        //    }
        //}

        //public void SendCustomEmail(MimeMessage mailMessage, SmtpServerConfig config)
        //{
        //    mailMessage.From.Add(new MailboxAddress(config.FullName, config.MailBoxLogin));

        //    using (SmtpClient smtp = new())
        //    {
        //        try
        //        {
        //            smtp.Connect(config.Smtp, config.SmptPort, true);
        //            smtp.Authenticate(config.MailBoxLogin, config.MailBoxPassword);
        //            smtp.Send(mailMessage);
        //        }
        //        catch (Exception ex)
        //        {
        //            throw;
        //        }
        //        finally
        //        {
        //            smtp.Disconnect(true);
        //        }
        //    }
        //}
    }
}