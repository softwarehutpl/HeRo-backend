using Common.ServiceRegistrationAttributes;
using Data.DTOs.Report;
using Data.Entities;
using Data.IRepositories;
using Microsoft.Extensions.Logging;

namespace Services.Services
{
    [ScopedRegistration]
    public class ReportService
    {
        private ILogger<ReportService> _logger;
        private ICandidateRepository _candidateRepository;
        private IRecruitmentRepository _recruitmentRepository;
        private IRecruitmentSkillRepository _recruitmentSkillRepository;

        public ReportService(ILogger<ReportService> logger, ICandidateRepository candidateRepository,
            IRecruitmentRepository recruitmentRepository,
            IRecruitmentSkillRepository recruitmentSkillRepository)
        {
            _logger = logger;
            _candidateRepository = candidateRepository;
            _recruitmentRepository = recruitmentRepository;
            _recruitmentSkillRepository = recruitmentSkillRepository;
        }

        public int CountNewCandidates(ReportCountDTO reportDTO)
        {
            IQueryable<Candidate> candidates = _candidateRepository.GetAll();

            candidates = candidates.Where(c => c.RecruitmentId == reportDTO.Id);
            candidates = candidates.Where(c => c.ApplicationDate >= reportDTO.FromDate);
            candidates = candidates.Where(c => c.ApplicationDate <= reportDTO.ToDate);

            int totalCount = candidates.Count();

            return totalCount;
        }

        public IEnumerable<RaportPopularRecruitmentDTO> GetPopularRecruitments()
        {
            IQueryable<Recruitment> recruitments = _recruitmentRepository.GetAll();

            recruitments = recruitments.OrderByDescending(r => r.Candidates.Count);

            var popularRecruitments = recruitments.Select(r => new RaportPopularRecruitmentDTO
            {
                RecruitmentId = r.Id,
                RecruitmentName = r.Name,
                NumberOfCandidate = r.Candidates.Count
            }).Take(10);

            return popularRecruitments;
        }

        public IEnumerable<ReportRequestedSkillDTO> GetRequestedSkills()
        {
            IQueryable<Recruitment> recruitments = _recruitmentRepository.GetAll();
            IQueryable<int>? idsOfActiveRecruitments = recruitments.Where(r => !r.DeletedDate.HasValue).Select(r => r.Id);

            IQueryable<RecruitmentSkill> recruitmentSkills = _recruitmentSkillRepository.GetAll();

            recruitmentSkills = recruitmentSkills.Where(s => idsOfActiveRecruitments.Contains(s.RecruitmentId));

            var requestedSkill = recruitmentSkills
                .Select(s => new ReportRequestedSkillDTO
                {
                    SkillId = s.SkillId
                });

            return requestedSkill;
        }
    }
}