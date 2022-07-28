using Common.Enums;
using Common.Helpers;
using Common.ServiceRegistrationAttributes;
using Data.DTOs.Email;
using Data.Entities;
using Data.IRepositories;
using MailKit;
using MailKit.Net.Imap;
using MimeKit;

namespace Services.Services
{
    [ScopedRegistration]
    public class EmailService
    {
        private readonly EmailHelper _emailHelper;
        private readonly IEmailRepository _emailRepository;

        public EmailService(EmailHelper emailHelper, IEmailRepository emailRepository)
        {
            _emailHelper = emailHelper;
            _emailRepository = emailRepository;
        }

        public EmailDetailsDTO? GetEmailDetails(string messageId)
        {
            var emailDetailsDTO = _emailRepository.GetEmailDetailsDTO(messageId);

            if (emailDetailsDTO == null)
                return null;

            return emailDetailsDTO;
        }

        public bool AddSmtpAccountToDb(SmtpAccountDTO dto, out string errorMessage)
        {
            bool check = _emailRepository.CheckIfSmtpServerExists(dto.Login);

            if (check)
            {
                errorMessage = ErrorMessageHelper.AccountInDatabase;
                return false;
            }

            string password = PasswordHashHelper.EncodePasswordToBase64(dto.Password);

            SmtpAccount smtpAccount = new()
            {
                Login = dto.Login,
                Password = password,
                Name = dto.Name,
                Port = dto.Port,
                Sender = dto.Sender,
                Host = dto.Host,
            };

            _emailRepository.AddSmtpServer(smtpAccount);
            errorMessage = "";
            return true;
        }

        public bool AddImapAccountToDb(ImapAccountDTO dto, out string errorMessage)
        {
            bool check = _emailRepository.CheckIfImapServerExists(dto.Login);

            if (check)
            {
                errorMessage = ErrorMessageHelper.AccountInDatabase;
                return false;
            }

            string password = PasswordHashHelper.EncodePasswordToBase64(dto.Password);

            ImapAccount imapAccount = new()
            {
                Login = dto.Login,
                Password = password,
                Host = dto.Host,
                Port = dto.Port
            };

            _emailRepository.AddImapServer(imapAccount);
            errorMessage = "";
            return true;
        }

        public void SendConfirmationEmail(string reciver, string url)
        {
            string subject = MessageHelper.RegistrationSubject;
            string body = MessageHelper.RegistrationBody(url);
            MimeMessage mail = _emailHelper.CreateEmail(reciver, subject, body);

            SmtpAccountDTO? dto = _emailRepository.GetSmtpAccount(SmptAccountNames.CONFIRMATION.ToString());

            if (dto == null)
                return;

            string password = PasswordHashHelper.DecodeFrom64(dto.Password);

            _emailHelper.SendPredefinedEmail(mail, dto.Login, password, dto.Sender, dto.Host, dto.Port);
        }

        public bool SendPasswordRecoveryEmail(string email, string url)
        {
            string subject = MessageHelper.PasswordRecoverySubject;
            string body = MessageHelper.PasswordRecoveryBody(url);

            MimeMessage mail = _emailHelper.CreateEmail(email, subject, body);

            SmtpAccountDTO? dto = _emailRepository.GetSmtpAccount(SmptAccountNames.RECOVERY.ToString());

            if (dto == null)
                return false;

            string password = PasswordHashHelper.DecodeFrom64(dto.Password);

            _emailHelper.SendPredefinedEmail(mail, dto.Login, password, dto.Sender, dto.Host, dto.Port);

            return true;
        }

        public void SaveAllEmailsToDataBase()
        {
            List<ImapAccountDTO>? dtoList = _emailRepository.GetImapAccounts();
            List<string> messagesIdInDb = _emailRepository.GetAllMessagedId();
            List<MailMessage> allNewMessages = new();

            foreach (var dto in dtoList)
            {
                string password = PasswordHashHelper.DecodeFrom64(dto.Password);
                dto.Password = password;

                List<MailMessage>? newMessagesFromOneAccount = GetAllNewMessagesList(dto, messagesIdInDb);

                allNewMessages.AddRange(newMessagesFromOneAccount);
            }

            _emailRepository.AddRangeAndSaveChanges(allNewMessages);
        }

        private List<MailMessage> GetAllNewMessagesList(ImapAccountDTO dto, List<string> IdList)
        {
            using (var client = new ImapClient())
            {
                client.Connect(dto.Host, dto.Port, true);
                client.Authenticate(dto.Login, dto.Password);

                var folder = client.GetFolder(SpecialFolder.All);

                folder.Open(FolderAccess.ReadOnly);

                List<MailMessage> emailList = new();

                foreach (var item in folder)
                {
                    if (!IdList.Contains(item.MessageId))
                    {
                        emailList.Add(new MailMessage()
                        {
                            MessageId = item.MessageId,
                            Subject = item.Subject,
                            Bcc = item.Bcc.ToString(),
                            Cc = item.Cc.ToString(),
                            Date = item.Date,
                            To = item.To.ToString(),
                            From = item.From.ToString(),
                            InReplyTo = item.InReplyTo,
                            Body = item.TextBody,
                            HtmlBody = item.HtmlBody.ToString()
                        });

                        if (emailList.Count == 50) return emailList;
                    }
                }

                client.Disconnect(true);

                return emailList;
            }
        }

        public List<MailMessage> GetMailMessages()
        {
            List<MailMessage>? result = _emailRepository.GetAll().ToList();

            return result;
        }

        public MailMessage GetMailMessage(int id)
        {
            MailMessage result = _emailRepository.GetById(id);

            return result;
        }

        public List<EmailListDataDTO> GetAllEmailsList()
        {
            var list = _emailRepository.GetAllEmailList();

            return list;
        }
    }
}