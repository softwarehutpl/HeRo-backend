using Common.Helpers;
using Common.ServiceRegistrationAttributes;
using System.Net.Mail;

namespace Services.Services
{
    [ScopedRegistration]
    public class EmailService
    {
        private readonly EmailHelper _emailHelper;

        public EmailService(EmailHelper emailHelper)
        {
            _emailHelper = emailHelper;
        }

        public void SendConfirmationEmail(string email, string url)
        {
            string subject = "Registration";
            string body = $"Registration successfull. Click confirmation link to complete the process. \n {url}";
            MailMessage mail = _emailHelper.CreateEmail(email, subject, body);
            _emailHelper.SendEmail(mail);
        }

        public void SendPasswordRecoveryEmail(string email, string url)
        {
            string subject = "PasswordRecovery";
            string body = $"Password recovery link: {url}";

            MailMessage mail = _emailHelper.CreateEmail(email, subject, body);
            _emailHelper.SendEmail(mail);
        }
    }
}