using System.ComponentModel.DataAnnotations;

namespace HeRoBackEnd.ViewModels.Interview
{
    public class InterviewCreateViewModel
    {
        [Required]
        public DateTime Date { get; set; }

        [Required]
        public int CandidateId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public string Type { get; set; }
    }
}