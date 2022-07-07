using Common.Listing;
using Data.Entities;
using Data.Repositories;
using PagedList;
using Services.DTOs;
using Services.DTOs.Recruitment;

namespace Services.Services
{
    public class RecruitmentService
    {
        private RecruitmentRepository recruitmentRepository;

        public RecruitmentService(RecruitmentRepository _recruitmentRepository)
        {
            recruitmentRepository = _recruitmentRepository;
        }

        public async Task<int> AddRecruitment(RecruitmentDTO dto)
        {
            return 0;
        }
        public void UpdateRecruitment(int id, RecruitmentDTO dto)
        {

        }
        public void EndRecruitment(int id)
        {

        }

        public IEnumerable<Recruitment> GetRecruitments(Paging paging, SortOrder sortOrder, RecruitmentFiltringDTO recruitmentFiltringDTO)
        {
            IEnumerable<Recruitment> recruitments = recruitmentRepository.GetAllRecruitments();

            if (!String.IsNullOrEmpty(recruitmentFiltringDTO.Name))
            {
                recruitments = recruitments.Where(s => s.Name.Contains(recruitmentFiltringDTO.Name));
            }
            if (!String.IsNullOrEmpty(recruitmentFiltringDTO.Status))
            {
                recruitments = recruitments.Where(s => s.Status.Equals(recruitmentFiltringDTO.Status));
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
                if (sort.Key == "Name")
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
                else if (sort.Key == "Status")
                {
                    if (sort.Value == "DESC")
                    {
                        recruitments = recruitments.OrderByDescending(u => u.Status);
                    }
                    else
                    {
                        recruitments = recruitments.OrderBy(s => s.Status);
                    }
                }
            }

            var result = recruitments.ToPagedList(paging.PageNumber, paging.PageSize);

            return result;
        }
    }
}
