using MailKit.Net.Smtp;
using Common.ServiceRegistrationAttributes;
using MimeKit;
using MimeKit.Text;

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

        public void SendPredefinedEmail(MimeMessage mailMessage,
                                        string login,
                                        string password,
                                        string sender,
                                        string host,
                                        int port)
        {
            mailMessage.From.Add(new MailboxAddress(sender, login));

            using (SmtpClient smtp = new())
            {
                try
                {
                    smtp.Connect(host, port, true);
                    smtp.Authenticate(login, password);
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