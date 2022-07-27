using System.ComponentModel.DataAnnotations;
using Data.IRepositories;

namespace Data.Entities
{
    public class ImapAccount : IEntity
    {
        [Key]
        public int Id { get; set; }

        public string Host { get; set; }

        public int Port { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}