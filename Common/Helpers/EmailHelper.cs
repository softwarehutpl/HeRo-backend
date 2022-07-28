using MailKit.Net.Smtp;
using Common.ServiceRegistrationAttributes;
using MimeKit;
using MimeKit.Text;
using MailKit.Net.Imap;
using MailKit;

namespace Common.Helpers
{
    [ScopedRegistration]
    public class EmailHelper
    {
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

        public List<MimeMessage> GetAllMessagesList(string host,
                                                    int port,
                                                    string login,
                                                    string password,
                                                    List<string> IdList)
        {
            using (var client = new ImapClient())
            {
                client.Connect(host, port, true);
                client.Authenticate(login, password);

                var folder = client.GetFolder(SpecialFolder.All);

                folder.Open(FolderAccess.ReadOnly);

                List<MimeMessage> emailList = new();

                foreach (var item in folder)
                {
                    if (!IdList.Contains(item.MessageId))
                    {
                        emailList.Add(item);
                        Console.WriteLine("zapisano");

                        if (emailList.Count == 50) return emailList;
                    }
                }

                client.Disconnect(true);

                return emailList;
            }
        }
    }
}