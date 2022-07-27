using Common.ServiceRegistrationAttributes;
using Data.DTOs.Email;
using Data.Entities;

namespace Data.IRepositories
{
    [ScopedRegistrationWithInterface]
    public interface IEmailRepository : IBaseRepository<MailMessage>
    {
        public void AddImapServer(ImapAccount account);

        public void AddSmtpServer(SmtpAccount account);

        public bool CheckIfSmtpServerExists(string login);

        public bool CheckIfImapServerExists(string login);

        public SmtpAccountDTO? GetSmtpAccount(string name);

        public List<ImapAccountDTO>? GetImapAccounts();

        public List<string> GetAllMessagedId();

        public EmailDetailsDTO GetEmailDetailsDTO(string messageId);

        public List<EmailListDataDTO> GetAllEmailList();
    }
}