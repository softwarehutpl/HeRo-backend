using Common.Listing;
using Services.DTOs.User;

namespace Services.Listing
{
    public class UserListing
    {
        public int TotalCount { get; set; }
        public IEnumerable<UserDTO> UserDTOs { set; get; }
        public UserFiltringDTO UserFiltringDTO { set; get; }
        public SortOrder SortOrder { get; set; }
        public Paging Paging { set; get; }
    }
}