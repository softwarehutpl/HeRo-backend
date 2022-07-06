namespace Services.DTOs.User
{
    public class UserPagingDTO
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }

        public UserPagingDTO(int pageSize, int pageNumber)
        {
            PageSize = pageSize;
            PageNumber = pageNumber;
        }
    }
}
