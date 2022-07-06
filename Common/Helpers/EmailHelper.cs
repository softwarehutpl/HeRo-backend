using System.Net.Mail;
using System.Net;
using Newtonsoft.Json;

namespace Common.Helpers
{
    public class EmailHelper : IEmailHelper
    {
        private static Dictionary<string, string> _ReadEmailJson()
        {
            string path = Path.GetFullPath("..\\HeRoBackEnd\\Email.json");

            string file = File.ReadAllText(path);

            Dictionary<string, string> jsonEmailDict = JsonConvert.DeserializeObject<Dictionary<string, string>>(file);

            return jsonEmailDict;
        }

        private MailMessage CreateEmail()
        {
            Dictionary<string, string> jsonEmailDict = _ReadEmailJson();

            //Tu będzie metoda pobierająca E-mail z bazy danych. i wrzucimy go do mailMessage.To.Add()

            MailMessage mailMessage = new();

            mailMessage.From = new MailAddress(jsonEmailDict["CompanyEmail"]);
            mailMessage.To.Add("");
            mailMessage.Subject = jsonEmailDict["Subject"];
            mailMessage.Body = jsonEmailDict["Body"];

            return mailMessage;
        }
        public void SendEmail()
        {
            Dictionary<string, string> jsonDict = _ReadEmailJson();

            string port = jsonDict["Port"];
            int portStringParsed = int.Parse(port);

            MailMessage mailMessage = CreateEmail();

            using ( SmtpClient smtp = new(jsonDict["Smpt"], portStringParsed))
            {
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(jsonDict["CompanyEmail"], jsonDict["CompanyEmailPassword"]);
                smtp.EnableSsl = true;

                smtp.Send(mailMessage);
            }
        }

    }
}
