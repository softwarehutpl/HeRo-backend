using Common.ServiceRegistrationAttributes;
using Data.DTOs.Report;
using Data.Entities.Report;
using Data.Repositories;

namespace Services.Services
{
    [ScopedRegistration]
    public class ReportService
    {
        private ReportRepository _reportRepository;

        public ReportService(ReportRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }

        public List<ReportDailyNewCandidatesDTO> CountNewCandidates(ReportCountDTO reportDTO)
        {
            IQueryable<DailyRecruitment> newCandidates = _reportRepository.CountNewCandidates(reportDTO);

            List<ReportDailyNewCandidatesDTO> reportDailies = new List<ReportDailyNewCandidatesDTO>();

            IEnumerable<IGrouping<DateTime, DailyRecruitment>>? days = newCandidates.ToList().GroupBy(c => c.Date);

            foreach (var day in days)
            {
                List<RaportRecruitmentDTO> raportRecruitments = new List<RaportRecruitmentDTO>();
                foreach (var recruitment in day)
                {
                    RaportRecruitmentDTO raportRecruitment = new RaportRecruitmentDTO
                    {
                        RecruitmentId = recruitment.RecruitmentId,
                        RecruitmentName = recruitment.RecruitmentName,
                        NumberOfCandidate = recruitment.NumberOfCandidate
                    };

                    raportRecruitments.Add(raportRecruitment);
                }

                reportDailies.Add(new ReportDailyNewCandidatesDTO
                {
                    Date = day.Key,
                    raportPopularRecruitmentDTOs = raportRecruitments
                });
            }

            return reportDailies;
        }

        public IEnumerable<PopularRecruitment> GetPopularRecruitments()
        {
            IQueryable<PopularRecruitment> popularRecruitments = _reportRepository.GetPopularRecruitments();

            return popularRecruitments;
        }

        public IEnumerable<RequestedSkill> GetRequestedSkills()
        {
            IQueryable<RequestedSkill> requestedSkills = _reportRepository.GetRequestedSkills();

            return requestedSkills;
        }
    }
}