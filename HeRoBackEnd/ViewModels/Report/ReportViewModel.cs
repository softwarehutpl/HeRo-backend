using System.ComponentModel.DataAnnotations;

namespace HeRoBackEnd.ViewModels.Report
{
    public class ReportViewModel
    {
        public int Id { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
}