using Common.Listing;
using Common.ServiceRegistrationAttributes;
using Data.DTOs.Interview;
using Services.Listing;

namespace Services.IServices
{
    [ScopedRegistrationWithInterface]
    public interface IInterviewService
    {
        void Create(InterviewCreateDTO interviewCreate, int userCreatedId);
        int Delete(int interviewId, int loginUserId);
        InterviewDTO Get(int interviewId);
        InterviewListing GetInterviews(Paging paging, SortOrder? sortOrder, InterviewFiltringDTO interviewFiltringDTO);
        int GetQuantity();
        int Update(InterviewEditDTO interviewEdit, int userEditId);
    }
}