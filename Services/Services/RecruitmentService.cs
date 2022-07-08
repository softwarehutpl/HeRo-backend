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
using System.Security.Claims;

namespace Services.Services
{
    public class RecruitmentService
    {
        private readonly IMapper mapper;
        private readonly RecruitmentRepository repo;
        private readonly UserRepository userRepo;
        public RecruitmentService(IMapper map, RecruitmentRepository repo, UserRepository userRepo)
        {
            mapper = map;
            this.repo = repo;
            this.userRepo = userRepo;
        }
        private User GetUser()
        {
            List<Claim> claims = ClaimsPrincipal.Current.Claims.ToList();
            Claim emailClaim = claims.FirstOrDefault(e => e.Type == ClaimTypes.Email);
            User user = userRepo.GetUserByEmail(emailClaim.Value);

            return user;
        }
        public int AddRecruitment(CreateRecruitmentDTO dto)
        {
            User user = GetUser();

            Recruitment recruitment = mapper.Map<Recruitment>(dto);
            recruitment.Status = RecruitmentStatusEnum.Open.ToString();
            recruitment.CreatedById = user.Id;
            recruitment.LastUpdatedById = user.Id;
            recruitment.CreatedDate = DateTime.Now;
            recruitment.LastUpdatedDate = DateTime.Now;


            int result=repo.AddRecruitment(recruitment);

            return result;
        }
        public int UpdateRecruitment(int recruitmentId, UpdateRecruitmentDTO dto)
        {
            User user = GetUser();

            Recruitment recruitment = mapper.Map<Recruitment>(dto);
            recruitment.Id = recruitmentId;
            recruitment.LastUpdatedById = user.Id;
            recruitment.LastUpdatedDate = DateTime.Now;

            int result=repo.UpdateRecruitment(recruitment);

            return result;
        }
        public int ChangeStatus(ChangeRecruitmentStatusDTO dto)
        {
            User user = GetUser();

            Recruitment recruitment = repo.GetById(dto.Id);
            recruitment.Status = dto.Status;
            recruitment.LastUpdatedById = user.Id;
            if (recruitment.Status == RecruitmentStatusEnum.Closed.ToString()) recruitment.EndedById = user.Id;
            recruitment.LastUpdatedDate = dto.LastUpdatedDate;

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
