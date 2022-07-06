namespace Services.DTOs.User
{
    public class UserSortOrderDTO
    {
        public List<KeyValuePair<string, string>> Sort { get; set; }

        public UserSortOrderDTO(List<KeyValuePair<string, string>> sort)
        {
            Sort = sort;
        }
    }
}
