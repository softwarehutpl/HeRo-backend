using AutoMapper;
using Data.Entities;
using HeRoBackEnd.ViewModels.Candidate;
using Data.DTOs;
using Data.DTOs.Candidate;

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
            CreateMap<CandidateChangeStageViewModel, CandidateChangeStageAndStatusDTO>();
            CreateMap<Candidate, CandidateProfileDTO>();
            CreateMap<Candidate, CandidateInfoForListDTO>();

        }
    }
}
