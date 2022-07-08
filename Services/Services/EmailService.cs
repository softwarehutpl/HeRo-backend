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
<<<<<<< HEAD
            string email = "";
            //string email = _userRepository.GetUserEmail(id);
=======
            string email = _userRepository.GetUserEmail(id).ToString();

>>>>>>> 68a34c719ccdfc66b5fddd9cd28b6a7058bd9de6
            _emailHelper.SendEmail(email);
        }
    }
}
