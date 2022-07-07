using Common.Listing;

namespace HeRoBackEnd.ViewModels
{
    public class UserListFilterViewModel
    {
        public string Email { get; set; }
        public string UserStatus { get; set; }
        public string RoleName { get; set; }

        public Paging Paging { get; set; }

        public SortOrder SortOrder { get; set; }
    }
}
