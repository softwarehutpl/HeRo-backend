using Common.Listing;
using Common.ServiceRegistrationAttributes;
using Data.DTOs.Candidate;
using Services.Listing;

namespace Services.IServices
{
    [ScopedRegistrationWithInterface]
    public interface ICandidateService
    {
        int AddHRNote(int candidateId, CandidateAddHRNoteDTO dto);
        int AddTechNote(int candidateId, CandidateAddTechNoteDTO dto);
        int AllocateRecruiterAndTech(int id, CandidateAssigneesDTO dto);
        int AllocateRecruitmentInterview(int candidateId, CandidateAllocateInterviewDateDTO dto);
        int AllocateTechInterview(int candidateId, CandidateAllocateInterviewDateDTO dto);
        int ChangeStatus(int id, string status);
        int CreateCandidate(CreateCandidateDTO dto);
        int DeleteCandidate(DeleteCandidateDTO dto);
        CandidateProfileDTO? GetCandidateProfileById(int id);
        CandidateListing GetCandidates(Paging paging, SortOrder? sortOrder, CandidateFilteringDTO candidateFilteringDTO);
        int UpdateCandidate(int id, UpdateCandidateDTO dto);
    }
}