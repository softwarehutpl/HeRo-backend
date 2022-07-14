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
            CreateMap<CreateRecruitmentDTO, Recruitment>().ForMember(
                dest=>dest.Skills,
                opt=>opt.MapFrom(src=>(ICollection<RecruitmentSkill>)src));
            CreateMap<RecruitmentCreateViewModel, CreateRecruitmentDTO>();
            CreateMap<RecruitmentEditViewModel, UpdateRecruitmentDTO>();
            CreateMap<Recruitment, RecruitmentDetailsDTO>();
        }
    }
}
