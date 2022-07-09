using AutoMapper;
using Common.Listing;
using Data.Entities;
using Data.Repositories;
using PagedList;
using Services.DTOs.Recruitment;
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
            recruitment.CreatedById = user.Id;
            recruitment.LastUpdatedById = user.Id;

            int result = repo.AddRecruitment(recruitment);

            return result;
        }
        public int UpdateRecruitment(int recruitmentId, UpdateRecruitmentDTO dto)
        {
            User user = GetUser();

            Recruitment recruitment = repo.GetById(recruitmentId);
            recruitment.LastUpdatedById = user.Id;
            recruitment.BeginningDate = dto.BeginningDate;
            recruitment.EndingDate = dto.EndingDate;
            recruitment.Name = dto.Name;
            recruitment.Description = dto.Description;
            recruitment.RecruiterId = dto.RecruiterId;
            recruitment.LastUpdatedDate = dto.LastUpdatedDate;

            int result = repo.UpdateRecruitment(recruitment);

            return result;
        }

        public int EndRecruitment(int recruitmentId)
        {
            User user = GetUser();

            Recruitment recruitment = repo.GetById(recruitmentId);
            recruitment.LastUpdatedDate = DateTime.Now;
            recruitment.LastUpdatedById = user.Id;
            recruitment.EndingDate = DateTime.Now;
            recruitment.EndedById = user.Id;

            int result = repo.UpdateRecruitment(recruitment);

            return result;
        }

        public int DeleteRecruitment(int recruitmentId)
        {
            User user = GetUser();

            Recruitment recruitment = repo.GetById(recruitmentId);
            recruitment.LastUpdatedDate = DateTime.Now;
            recruitment.LastUpdatedById = user.Id;
            recruitment.DeletedDate = DateTime.Now;
            recruitment.DeletedById = user.Id;

            int result = repo.UpdateRecruitment(recruitment);

            return result;
        }

        public IEnumerable<ReadRecruitmentDTO> GetRecruitments(Paging paging, SortOrder sortOrder, RecruitmentFiltringDTO recruitmentFiltringDTO)
        {
            IEnumerable<Recruitment> recruitments = repo.GetAllRecruitments();

            if (!String.IsNullOrEmpty(recruitmentFiltringDTO.Name))
            {
                recruitments = recruitments.Where(s => s.Name.Contains(recruitmentFiltringDTO.Name));
            }
            if (!String.IsNullOrEmpty(recruitmentFiltringDTO.Description))
            {
                recruitments = recruitments.Where(s => s.Description.Contains(recruitmentFiltringDTO.Description));
            }
            if (recruitmentFiltringDTO.BeginningDate.HasValue)
            {
                recruitments = recruitments.Where(s => s.BeginningDate >= recruitmentFiltringDTO.BeginningDate);
            }
            if (recruitmentFiltringDTO.EndingDate.HasValue)
            {
                recruitments = recruitments.Where(s => s.EndingDate <= recruitmentFiltringDTO.EndingDate);
            }

            foreach (KeyValuePair<string, string> sort in sortOrder.Sort)
            {
                if (sort.Value == "DESC")
                {
                    recruitments = recruitments.OrderByDescending(u => u.Name);
                }
                else
                {
                    recruitments = recruitments.OrderBy(s => s.Name);
                }
            }

            IEnumerable<Recruitment> pagedRecruitments = recruitments.ToPagedList(paging.PageNumber, paging.PageSize);
            IEnumerable<ReadRecruitmentDTO> result = mapper.Map<IEnumerable<ReadRecruitmentDTO>>(pagedRecruitments);

            return result;
        }

        public ReadRecruitmentDTO GetRecruitment(int recruitmentId)
        {
            Recruitment recruitment = repo.GetRecruitmentById(recruitmentId);
            ReadRecruitmentDTO result = null;

            if (recruitment != null) result = mapper.Map<ReadRecruitmentDTO>(recruitment);

            return result;
        }
    }
}
