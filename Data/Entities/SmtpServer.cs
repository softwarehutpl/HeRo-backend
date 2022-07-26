using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Data.IRepositories;

namespace Data.Entities
{
    public class SmtpServer : IEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public string Smtp { get; set; }

        [Required]
        public int Port { get; set; }

        public string MailBoxLogin { get; set; }

        [Required]
        public string MailBoxPassword { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}