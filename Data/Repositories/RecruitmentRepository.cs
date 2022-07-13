using AutoMapper;
using Common.Enums;
using Data.Entities;
using Microsoft.Data.SqlClient;
using Services.DTOs.Recruitment;
using System.Security.Claims;


namespace Data.Repositories
{
    public class RecruitmentRepository : BaseRepository<Recruitment>
    {
        private readonly IMapper _mapper;
        public RecruitmentRepository(DataContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }
        public RecruitmentDetailsDTO GetRecruitmentById(int id) //RecruitmentDetailsDTO
        {
            Recruitment recruitment = GetById(id);

            RecruitmentDetailsDTO result = _mapper.Map<RecruitmentDetailsDTO>(recruitment);

            return result;
        }

        public IQueryable<RecruitmentDetailsDTO> GetAllRecruitments()
        {
            IQueryable<Recruitment> recruitments = GetAll().
                Where(e => !(e.EndedDate.HasValue) && !(e.DeletedDate.HasValue));

            IQueryable<RecruitmentDetailsDTO> result=recruitments
                .Select(x =>_mapper.Map<RecruitmentDetailsDTO>(recruitments));

            return result;
        }
    }
}
