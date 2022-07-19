using Common.ServiceRegistrationAttributes;

namespace Services.IServices
{
    [ScopedRegistrationWithInterface]
    public interface IEmailService
    {
        void SendConfirmationEmail(string email, string url);
        void SendPasswordRecoveryEmail(string email, string url);
    }
}