using Common.Enums;
using Common.Helpers;
using Common.ServiceRegistrationAttributes;
using Data.DTOs.Email;
using Data.DTOs.User;
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

        public bool AddSmtpAccountToDb(SmtpAccountDTO dto, out string errorMessage)
        {
            bool check = _emailRepository.CheckIfSmtpServerExists(dto.Login);

            if (check)
            {
                errorMessage = "Account already in Database";
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
                errorMessage = "Account already in Database";
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

            _emailHelper.SendPredefinedEmail(mail, dto.Login, dto.Password, dto.Sender, dto.Host, dto.Port);
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

        //public List<EmailListDataDTO>? GetAllEmailsInFolder(int id, string folderName)
        //{
        //    EmailServiceDTO? dto = _userService.GetUserEmailServiceData(id);
        //    string password = PasswordHashHelper.DecodeFrom64(dto.userSmtpData.MailBoxPassword);

        //    ImapServerConfig config = new()
        //    {
        //        Imap = dto.userSmtpData.Imap,
        //        MailBoxLogin = dto.userSmtpData.MailBoxLogin,
        //        MailBoxPassword = password,
        //        ImapPort = dto.userSmtpData.ImapPort,
        //    };

        //    List<EmailListDataDTO>? list = GetAllEmailListInFolder(config, folderName);

        //    return list;
        //}

        //public EmailDetailsDTO? ReadEmailDetails(int id, string messageId, string folderName)
        //{
        //    EmailServiceDTO? dto = _userService.GetUserEmailServiceData(id);
        //    if (dto == null) return null;

        //    string password = PasswordHashHelper.DecodeFrom64(dto.userSmtpData.MailBoxPassword);

        //    ImapServerConfig config = new()
        //    {
        //        Imap = dto.userSmtpData.Imap,
        //        MailBoxLogin = dto.userSmtpData.MailBoxLogin,
        //        MailBoxPassword = password,
        //        ImapPort = dto.userSmtpData.ImapPort,
        //    };

        //    EmailDetailsDTO? email = ReadEmail(config, messageId, folderName);

        //    if (email == null)
        //        return null;

        //    return email;
        //}

        //private EmailDetailsDTO? ReadEmail(ImapServerConfig config, string messageId, string folderName)
        //{
        //    using (ImapClient imap = new())
        //    {
        //        imap.Connect(config.Imap, config.ImapPort, true);
        //        imap.Authenticate(config.MailBoxLogin, config.MailBoxPassword);

        //        var folder = imap.GetFolder(folderName);
        //        folder.Open(FolderAccess.ReadOnly);

        //        MimeMessage? email = folder.FirstOrDefault(m => m.MessageId == messageId);

        //        if (email == null) return null;

        //        EmailDetailsDTO emailDetails = new()
        //        {
        //            From = email.From.ToString(),
        //            To = email.To.ToString(),
        //            Subject = email.Subject,
        //            Body = email.HtmlBody,
        //            Date = email.Date
        //        };

        //        imap.Disconnect(true);

        //        return emailDetails;
        //    }
        //}

        //private List<EmailListDataDTO>? GetAllEmailListInFolder(ImapServerConfig config, string folderName)
        //{
        //    using (ImapClient imap = new())
        //    {
        //        imap.Connect(config.Imap, config.ImapPort, true);
        //        imap.Authenticate(config.MailBoxLogin, config.MailBoxPassword);

        //        var folder = imap.GetFolder(folderName);

        //        folder.Open(FolderAccess.ReadOnly);

        //        List<EmailListDataDTO>? emails = new();

        //        foreach (var item in folder)
        //        {
        //            emails.Add(new EmailListDataDTO()
        //            {
        //                Id = item.MessageId,
        //                From = item.From.ToString(),
        //                Subject = item.Subject,
        //                Date = item.Date.ToLocalTime()
        //            });
        //        }

        //        imap.Disconnect(true);

        //        return emails;
        //    }
        //}

        //private List<string>? GetFolderNames(ImapServerConfig config)
        //{
        //    using (ImapClient imap = new())
        //    {
        //        imap.Connect(config.Imap, config.ImapPort, true);
        //        imap.Authenticate(config.MailBoxLogin, config.MailBoxPassword);

        //        IList<IMailFolder>? folders = imap.GetFolders(imap.PersonalNamespaces[0]);

        //        List<string> folderNames = new();

        //        foreach (var folder in folders)
        //        {
        //            folderNames.Add(folder.FullName);
        //        }

        //        imap.Disconnect(true);

        //        return folderNames;
        //    }
        //}
    }
}