using AutoMapper;
using Data.Entities;
using HeRoBackEnd.ViewModels.Candidate;
using Services.DTOs.Candidate;

namespace HeRoBackEnd.Profiles
{
    public class CandidateProfile : Profile
    {
        public CandidateProfile()
        {
            CreateMap<CreateCandidateDTO, Candidate>();
            CreateMap<CandidateCreateViewModel, CreateCandidateDTO>();
            CreateMap<CandidateEditViewModel, UpdateCandidateDTO>();
            CreateMap<CandidateAssigneesViewModel, CandidateAssigneesDTO>();
            CreateMap<CandidateAddHRNoteViewModel, CandidateAddHRNoteDTO>();
            CreateMap<CandidateAddTechNoteViewModel, CandidateAddTechNoteDTO>();
            CreateMap<CandidateAllocateInterviewDateViewModel, CandidateAllocateInterviewDateDTO>();
            CreateMap<Candidate, CandidateProfileDTO>();
            CreateMap<Candidate, CandidateInfoForListDTO>();

        }
    }
}
