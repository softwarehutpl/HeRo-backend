namespace Data.DTOs.Email
{
    public class ImapAccountDTO
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}