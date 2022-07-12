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
            CreateMap<Candidate, CandidateProfileDTO>();
            CreateMap<Candidate, CandidateInfoForListDTO>();

        }
    }
}
