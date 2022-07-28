namespace HeRoBackEnd.ViewModels.Email
{
    public class SmtpAccountViewModel
    {
        public int Port { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

        public string Sender { get; set; }
        public string Host { get; set; }
    }
}