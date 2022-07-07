using AutoMapper;
using Services.DTOs.Recruitment;
using Data.Entities;

namespace HeRoBackEnd.Profiles
{
    public class RecruitmentProfile : Profile
    {
        public RecruitmentProfile()
        {
            CreateMap<CreateRecruitmentDTO, Recruitment>();
            CreateMap<UpdateRecruitmentDTO, Recruitment>();
            CreateMap<Recruitment, ReadRecruitmentDTO>();
        }
    }
}
