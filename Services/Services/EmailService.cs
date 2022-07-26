using Common.ConfigClasses;
using Common.Helpers;
using Common.ServiceRegistrationAttributes;
using Data.DTOs.User;
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
            _emailHelper.SendPredefinedEmail(mail);
        }

        public void SendPasswordRecoveryEmail(string email, string url)
        {
            string subject = MessageHelper.PasswordRecoverySubject;
            string body = MessageHelper.PasswordRecoveryBody(url);

            MimeMessage mail = _emailHelper.CreateEmail(email, subject, body);
            _emailHelper.SendPredefinedEmail(mail);
        }

        public bool SendCustomEmail(int id, string to, string subject, string body, out string emailError)
        {
            EmailServiceDTO? dto = _userService.GetUserEmailServicePassword(id);
            string password = PasswordHashHelper.DecodeFrom64(dto.userSmtpData.MailBoxPassword);

            if (dto == null)
            {
                emailError = "string";
                return false;
            }

            CustomEmailConfig config = new()
            {
                FullName = dto.FullName,
                Port = dto.userSmtpData.Port,
                Smtp = dto.userSmtpData.Smtp,
                Email = dto.userSmtpData.MailBoxLogin,
                Password = password
            };

            MimeMessage mail = _emailHelper.CreateEmail(to, subject, body);

            _emailHelper.SendCustomEmail(mail, config);

            emailError = "";
            return true;
        }
    }
}