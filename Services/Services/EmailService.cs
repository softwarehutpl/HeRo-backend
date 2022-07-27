using Common.Helpers;
using Common.ServiceRegistrationAttributes;
using Data.DTOs.Email;
using Data.DTOs.User;
using MailKit;
using MailKit.Net.Imap;
using MimeKit;

namespace Services.Services
{
    [ScopedRegistration]
    public class EmailService
    {
        private readonly EmailHelper _emailHelper;
        private readonly UserService _userService;

        public EmailService(EmailHelper emailHelper, UserService userService)
        {
            _emailHelper = emailHelper;
            _userService = userService;
        }

        public void SendConfirmationEmail(string email, string url)
        {
            string subject = MessageHelper.RegistrationSubject;
            string body = MessageHelper.RegistrationBody(url);
            MimeMessage mail = _emailHelper.CreateEmail(email, subject, body);
            //_emailHelper.SendPredefinedEmail(mail);
        }

        public void SendPasswordRecoveryEmail(string email, string url)
        {
            string subject = MessageHelper.PasswordRecoverySubject;
            string body = MessageHelper.PasswordRecoveryBody(url);

            MimeMessage mail = _emailHelper.CreateEmail(email, subject, body);
            //_emailHelper.SendPredefinedEmail(mail);
        }

        //public bool SendCustomEmail(int id, string to, string subject, string body, out string emailError)
        //{
        //    emailError = string.Empty;
        //    return true;
        //}

        //public List<string>? GetAllFolderNamesList(int id)
        //{
        //    EmailServiceDTO? dto = _userService.GetUserEmailServiceData(id);

        //    string password = PasswordHashHelper.DecodeFrom64(dto.userSmtpData.MailBoxPassword);

        //    ImapServerConfig config = new()
        //    {
        //        Imap = dto.userSmtpData.Imap,
        //        ImapPort = dto.userSmtpData.ImapPort,
        //        MailBoxLogin = dto.userSmtpData.MailBoxLogin,
        //        MailBoxPassword = password
        //    };

        //    List<string>? folderNames = GetFolderNames(config);

        //    return folderNames;
        //}

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