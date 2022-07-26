using System.ComponentModel.DataAnnotations;

namespace HeRoBackEnd.ViewModels.Report
{
    public class ReportCountViewModel
    {
        public int Id { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
}