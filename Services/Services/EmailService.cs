using Common.Helpers;
using Data.Repositories;

namespace Services.Services
{
    public class EmailService
    {
        private IEmailHelper _emailHelper;
        private UserRepository _userRepository;

        public EmailService(IEmailHelper emailHelper, UserRepository userRepository)
        {
            _emailHelper = emailHelper;
            _userRepository = userRepository;
        }

        public void SendConfirmationEmail(string id)
        {

            string email = _userRepository.GetUserEmail(id).ToString();

            _emailHelper.SendEmail(email);
        }
    }
}
