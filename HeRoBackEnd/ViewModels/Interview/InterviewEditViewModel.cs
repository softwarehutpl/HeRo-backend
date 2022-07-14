using System.ComponentModel.DataAnnotations;

namespace HeRoBackEnd.ViewModels.Interview
{
    public class InterviewEditViewModel
    {
        [Required]
        public DateTime Date { get; set; }

        [Required]
        public int WorkerId { get; set; }

        [Required]
        public string Type { get; set; }
    }
}