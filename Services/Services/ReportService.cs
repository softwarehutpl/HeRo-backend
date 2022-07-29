using Common.ServiceRegistrationAttributes;
using Data.DTOs.Report;
using Data.Repositories;
using Microsoft.Extensions.Configuration;

namespace Services.Services
{
    [ScopedRegistration]
    public class ReportService
    {
        private ReportRepository _reportRepository;
        private IConfiguration Configuration { get; }

        public ReportService(ReportRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }

        public List<ReportDailyRecruitmentsDTO> CountNewCandidates(ReportCountDTO reportDTO)
        {
            IQueryable<ReportDailyRecruitmentDTO> newCandidates = _reportRepository.CountNewCandidates(reportDTO);

            List<ReportDailyRecruitmentsDTO> reportDailies = new List<ReportDailyRecruitmentsDTO>();

            IEnumerable<IGrouping<DateTime, ReportDailyRecruitmentDTO>>? days = newCandidates.ToList().GroupBy(c => c.Date);

            foreach (var day in days)
            {
                List<ReportRecruitmentDTO> raportRecruitments = new List<ReportRecruitmentDTO>();
                foreach (var recruitment in day)
                {
                    ReportRecruitmentDTO raportRecruitment = new ReportRecruitmentDTO
                    {
                        RecruitmentId = recruitment.RecruitmentId,
                        RecruitmentName = recruitment.RecruitmentName,
                        NumberOfCandidate = recruitment.NumberOfCandidate
                    };

                    raportRecruitments.Add(raportRecruitment);
                }

                reportDailies.Add(new ReportDailyRecruitmentsDTO
                {
                    Date = day.Key,
                    raportPopularRecruitmentDTOs = raportRecruitments
                });
            }

            return reportDailies;
        }

        public IQueryable<ReportRecruitmentDTO> GetPopularRecruitments()
        {
            IQueryable<ReportRecruitmentDTO> popularRecruitments = _reportRepository.GetPopularRecruitments();

            return popularRecruitments;
        }

        public IQueryable<ReportRequestedSkillDTO> GetRequestedSkills()
        {
            IQueryable<ReportRequestedSkillDTO> requestedSkills = _reportRepository.GetRequestedSkills();

            return requestedSkills;
        }
    }
}