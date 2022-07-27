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

        public ImapAccountDTO? GetImapAccount(string login)
        {
            ImapAccountDTO? dto = DataContext.ImapAccounts
                .Where(x => x.Login == login)
                .Select(x => new ImapAccountDTO()
                {
                    Login = x.Login,
                    Password = x.Password,
                    Port = x.Port,
                    Host = x.Host
                }).FirstOrDefault();

            return dto;
        }
    }
}