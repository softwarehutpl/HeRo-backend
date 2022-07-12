using AutoMapper;
using Data.Entities;
using Services.DTOs.Skill;

namespace HeRoBackEnd.Profiles
{
    public class SkillProfile : Profile
    {
        public SkillProfile()
        {
            CreateMap<CreateSkillDTO, Skill>();
            CreateMap<Skill, ReadSkillDTO>();
        }
    }
}
