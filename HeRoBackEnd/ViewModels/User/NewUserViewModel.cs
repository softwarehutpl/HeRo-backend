using System.ComponentModel.DataAnnotations;

namespace HeRoBackEnd.ViewModels.User
{
    public class NewUserViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Compare("SecondPassword", ErrorMessage = "Passwords are not the same!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        public string SecondPassword { get; set; }
    }
}