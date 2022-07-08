using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Data.Entities;
using Services.DTOs.Recruitment;
using Data.Repositories;
using Common.Enums;

namespace Services.Services
{
    public class RecruitmentService
    {
        private readonly IMapper mapper;
        private readonly RecruitmentRepository repo;
        public RecruitmentService(IMapper map, RecruitmentRepository repo)
        {
            mapper = map;
            this.repo = repo;
        }
        public int AddRecruitment(CreateRecruitmentDTO dto)
        {
            Recruitment recruitment = mapper.Map<Recruitment>(dto);
            recruitment.Status = RecruitmentStatusEnum.Open;

            int result=repo.AddRecruitment(recruitment);

            return result;
        }
        public int UpdateRecruitment(int recruitmentId, UpdateRecruitmentDTO dto)
        {
            Recruitment recruitment = mapper.Map<Recruitment>(dto);
            recruitment.Id = recruitmentId;

            int result=repo.UpdateRecruitment(recruitment);

            return result;
        }
        public int ChangeStatus(int recruitmentId, RecruitmentStatusEnum status)
        {
            Recruitment recruitment = repo.GetById(recruitmentId);
            recruitment.Status = status;

            int result = repo.ChangeStatus(recruitment);

            return result;
        }
        public int EndRecruitment(int recruitmentId)
        {
            int result=repo.RemoveRecruitment(recruitmentId);

            return result;
        }
        public ReadRecruitmentDTO GetRecruitment(int recruitmentId)
        {
            Recruitment recruitment=repo.GetRecruitmentById(recruitmentId);
            ReadRecruitmentDTO result = null;

            if(recruitment!=null) result = mapper.Map<ReadRecruitmentDTO>(recruitment);

            return result;
        }
        public List<ReadRecruitmentDTO> GetRecruitments()
        {
            List<Recruitment> recruitmentList = repo.GetAllRecruitments();

            List<ReadRecruitmentDTO> result = mapper.Map <List<ReadRecruitmentDTO>>(recruitmentList);

            return result;
        }
    }
}
