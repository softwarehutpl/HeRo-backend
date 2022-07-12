using System.Net.Mail;
using Common.Helpers;
using Data.Repositories;

namespace Services.Services
{
    public class EmailService
    {
        private readonly EmailHelper _emailHelper;
        private readonly UserRepository _userRepository;
        private readonly AuthService _authService;

        public EmailService(EmailHelper emailHelper, UserRepository userRepository, AuthService authService)
        {
            _emailHelper = emailHelper;
            _userRepository = userRepository;
            _authService = authService;
        }

        public void SendConfirmationEmail(int id)
        {          
            string subject = "Registration";
            string body = "Sucessfull registration. Click confiramtion link to complete the process";
            //string email = "";
            string email = _userRepository.GetUserEmail(id);
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

        public void SendRecoveredPassword(string email)
        {

            //string newPassword = _authService.GenerateNewPassword(email);
            //string subject = "Password";
            //string body = $"Your new password is {newPassword}";

            // MailMessage mail = _emailHelper.CreateEmail(email, subject, body);
            //_emailHelper.SendEmail(mail);
        }
    }
}
