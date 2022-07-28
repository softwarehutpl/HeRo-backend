namespace Data.DTOs.Report
{
    public class ReportCountDTO
    {
        public List<int> Ids { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
}