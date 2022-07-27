using Common.ServiceRegistrationAttributes;
using Data.DTOs.Email;
using Data.Entities;
using Data.IRepositories;

namespace Data.Repositories
{
    [ScopedRegistrationWithInterface]
    public class EmailRepository : BaseRepository<MailMessage>, IEmailRepository
    {
        public EmailRepository(DataContext context) : base(context)
        {
        }

        public void AddImapServer(ImapAccount account)
        {
            DataContext.ImapAccounts.Add(account);
            DataContext.SaveChanges();
        }

        public void AddSmtpServer(SmtpAccount account)
        {
            DataContext.SmtpAccounts.Add(account);
            DataContext.SaveChanges();
        }

        public bool CheckIfSmtpServerExists(string login)
        {
            bool result = DataContext.SmtpAccounts.Any(x => x.Login == login);

            return result;
        }

        public bool CheckIfImapServerExists(string login)
        {
            bool result = DataContext.SmtpAccounts.Any(x => x.Login == login);

            return result;
        }

        public SmtpAccountDTO? GetSmtpAccount(string name)
        {
            SmtpAccountDTO? dto = DataContext.SmtpAccounts
                .Where(x => x.Name == name)
                .Select(x => new SmtpAccountDTO()
                {
                    Login = x.Login,
                    Password = x.Password,
                    Port = x.Port,
                    Name = x.Name,
                    Sender = x.Sender,
                    Host = x.Host
                }).FirstOrDefault();

            return dto;
        }

        public List<ImapAccountDTO>? GetImapAccounts()
        {
            List<ImapAccountDTO>? result = DataContext.ImapAccounts.Select(x => new ImapAccountDTO
            {
                Host = x.Host,
                Port = x.Port,
                Login = x.Login,
                Password = x.Password
            }).ToList();

            return result;
        }

        public List<string> GetAllMessagedId()
        {
            var result = DataContext.MailMessages.Select(x => x.MessageId).ToList();

            return result;
        }

        public EmailDetailsDTO? GetEmailDetailsDTO(string messageId)
        {
            EmailDetailsDTO? dto = DataContext.MailMessages
                .Where(x => x.MessageId == messageId)
                .Select(x => new EmailDetailsDTO()
                {
                    MessageId = x.MessageId,
                    Bcc = x.Bcc,
                    Cc = x.Cc,
                    Body = x.Body,
                    HtmlBody = x.HtmlBody,
                    Date = x.Date,
                    Subject = x.Subject,
                    InReplyTo = x.InReplyTo,
                    To = x.To,
                    From = x.From
                }).FirstOrDefault();

            return dto;
        }

        public List<EmailListDataDTO> GetAllEmailList()
        {
            var result = DataContext.MailMessages.Select(x => new EmailListDataDTO()
            {
                Date = x.Date,
                MessageId = x.MessageId,
                Subject = x.Subject,
                To = x.To
            }).ToList();

            return result;
        }
    }
}