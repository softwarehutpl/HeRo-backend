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
        private ISkillRepository _skillRepository;

        public ReportService(ILogger<ReportService> logger, ICandidateRepository candidateRepository,
            IRecruitmentRepository recruitmentRepository,
            IRecruitmentSkillRepository recruitmentSkillRepository, ISkillRepository skillRepository)
        {
            _logger = logger;
            _candidateRepository = candidateRepository;
            _recruitmentRepository = recruitmentRepository;
            _recruitmentSkillRepository = recruitmentSkillRepository;
            _skillRepository = skillRepository;
        }

        public List<ReportDailyNewCandidatesDTO> CountNewCandidates(ReportCountDTO reportDTO)
        {
            IQueryable<Candidate> candidates = _candidateRepository.GetAll();

            DateTime currentDayTemp = new DateTime(reportDTO.FromDate.Year, reportDTO.FromDate.Month, reportDTO.FromDate.Day);
            List<ReportDailyNewCandidatesDTO> reportDailies = new List<ReportDailyNewCandidatesDTO>();

            if (reportDTO.Ids != null && reportDTO.Ids.Count > 0)
            {
                while (currentDayTemp <= reportDTO.ToDate)
                {
                    List<RaportRecruitmentDTO> raportRecruitments = new List<RaportRecruitmentDTO>();

                    foreach (var id in reportDTO.Ids)
                    {
                        IQueryable<Candidate> tempCandidates = candidates;

                        tempCandidates = tempCandidates.Where(c => c.RecruitmentId == id);
                        tempCandidates = tempCandidates.Where(c => c.ApplicationDate >= currentDayTemp);
                        tempCandidates = tempCandidates.Where(c => c.ApplicationDate <= currentDayTemp.AddDays(1));

                        RaportRecruitmentDTO raportRecruitment = new RaportRecruitmentDTO
                        {
                            RecruitmentId = id,
                            RecruitmentName = _recruitmentRepository.GetById(id).Name,
                            NumberOfCandidate = tempCandidates.Count()
                        };
                        raportRecruitments.Add(raportRecruitment);
                    }

                    reportDailies.Add(new ReportDailyNewCandidatesDTO
                    {
                        Date = currentDayTemp,
                        raportPopularRecruitmentDTOs = raportRecruitments
                    });

                    currentDayTemp = currentDayTemp.AddDays(1);
                }
            }

            return reportDailies;
        }

        public IEnumerable<RaportRecruitmentDTO> GetPopularRecruitments()
        {
            IQueryable<Recruitment> recruitments = _recruitmentRepository.GetAll();

            recruitments = recruitments.Where(r => r.Candidates.Count > 0).OrderByDescending(r => r.Candidates.Count);

            var popularRecruitments = recruitments.Select(r => new RaportRecruitmentDTO
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

            recruitments = recruitments.Where(r => r.EndingDate > DateTime.UtcNow)
                .Where(r => !r.EndedDate.HasValue)
                .Where(r => !r.DeletedDate.HasValue);

            IQueryable<int>? idsOfActiveRecruitments = recruitments.Select(r => r.Id);
            IQueryable<RecruitmentSkill> recruitmentSkills = _recruitmentSkillRepository.GetAll();
            IQueryable<Skill> skills = _skillRepository.GetAll();

            recruitmentSkills = recruitmentSkills.Where(s => idsOfActiveRecruitments.Contains(s.RecruitmentId));

            var requestedSkill = recruitmentSkills
                .GroupBy(rs => rs.SkillId)
                .OrderByDescending(rs => rs.Count())
                .Select(rs => new ReportRequestedSkillDTO
                {
                    SkillId = rs.Key,
                    SkillName = skills.Where(s => s.Id == rs.Key).Select(s => s.Name).FirstOrDefault(),
                    Quantity = rs.Count(),
                }).Take(5);

            return requestedSkill;
        }
    }
}