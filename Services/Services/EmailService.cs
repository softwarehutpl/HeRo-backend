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
            string subject = MessageHelper.RegistrationSubject;
            string body = MessageHelper.RegistrationBody(url);
            MailMessage mail = _emailHelper.CreateEmail(email, subject, body);
            _emailHelper.SendEmail(mail);
        }

        public void SendPasswordRecoveryEmail(string email, string url)
        {
            string subject = MessageHelper.PasswordRecoverySubject;
            string body = MessageHelper.PasswordRecoveryBody(url);

            MailMessage mail = _emailHelper.CreateEmail(email, subject, body);
            _emailHelper.SendEmail(mail);
        }
    }
}