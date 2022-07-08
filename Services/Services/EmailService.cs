using Common.Helpers;
using Data.Repositories;

namespace Services.Services
{
    public class EmailService
    {
        private EmailHelper _emailHelper;
        private UserRepository _userRepository;

        public EmailService(EmailHelper emailHelper, UserRepository userRepository)
        {
            _emailHelper = emailHelper;
            _userRepository = userRepository;
        }

        public void SendConfirmationEmail(int id)
        {

            string email = _userRepository.GetUserEmail(id).ToString();

            _emailHelper.SendEmail(email);
        }
    }
}
