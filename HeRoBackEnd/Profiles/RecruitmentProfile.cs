using AutoMapper;
using Data.DTOs.Recruitment;
using Data.Entities;
using HeRoBackEnd.ViewModels.Recruitment;
using Data.DTOs.Skill;

namespace HeRoBackEnd.Profiles
{
    public class RecruitmentProfile : Profile
    {
        public RecruitmentProfile()
        {
            CreateMap<CreateRecruitmentDTO, Recruitment>();
            CreateMap<RecruitmentCreateViewModel, CreateRecruitmentDTO>();
            CreateMap<RecruitmentEditViewModel, UpdateRecruitmentDTO>();
            CreateMap<Recruitment, RecruitmentDetailsDTO>();
        }
    }
}
