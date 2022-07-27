namespace Data.DTOs.Email
{
    public class EmailListDataDTO
    {
        public string MessageId { get; set; }
        public string? Subject { get; set; }
        public string To { get; set; }
        public DateTimeOffset? Date { get; set; }
    }
}