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

        public ReportService(ILogger<ReportService> logger, ICandidateRepository candidateRepository)
        {
            _logger = logger;
            _candidateRepository = candidateRepository;
        }

        public int CountNewCandidates(ReportDTO reportDTO)
        {
            IQueryable<Candidate> candidates = _candidateRepository.GetAll();

            candidates = candidates.Where(c => c.RecruitmentId == reportDTO.Id);
            candidates = candidates.Where(c => c.ApplicationDate >= reportDTO.FromDate);
            candidates = candidates.Where(c => c.ApplicationDate <= reportDTO.ToDate);

            int totalCount = candidates.Count();

            return totalCount;
        }
    }
}