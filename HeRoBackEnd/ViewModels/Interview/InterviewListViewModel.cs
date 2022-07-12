using Common.Listing;
using System.ComponentModel.DataAnnotations;

namespace HeRoBackEnd.ViewModels.Interview
{
    public class InterviewListViewModel
    {
        [Required]
        public DateTime FromDate { get; set; }

        [Required]
        public DateTime ToDate { get; set; }

        public int? CandidateId { get; set; }

        public int? UserId { get; set; }

        public string? Type { get; set; }

        public Paging Paging { get; set; }

        public SortOrder SortOrder { get; set; }
    }
}
