using System.ComponentModel.DataAnnotations;
using Data.IRepositories;

namespace Data.Entities
{
    public class MailMessage : IEntity
    {
        [Key]
        public int Id { get; set; }

        public string MessageId { get; set; }
        public string? Subject { get; set; }
        public string To { get; set; }
        public string From { get; set; }
        public string? Body { get; set; }
        public string? HtmlBody { get; set; }
        public string? Bcc { get; set; }
        public string? Cc { get; set; }
        public DateTimeOffset? Date { get; set; }
        public string? InReplyTo { get; set; }
    }
}