using AutoMapper;
using Data.DTOs.RecruitmentSkill;
using Data.Entities;

namespace HeRoBackEnd.Profiles
{
    public class RecruitmentSkillProfile : Profile
    {
        public RecruitmentSkillProfile()
        {
            CreateMap<RecruitmentSkill, RecruitmentSkillDTO>().ReverseMap();
        }
    }
}
