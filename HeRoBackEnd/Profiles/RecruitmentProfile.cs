using AutoMapper;
using Services.DTOs.Recruitment;
using Data.Entities;
using HeRoBackEnd.ViewModels.Recruitment;
using Services.DTOs.Skill;

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
