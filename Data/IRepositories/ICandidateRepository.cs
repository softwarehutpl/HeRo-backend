using Common.ServiceRegistrationAttributes;
using Data.Entities;

namespace Data.IRepositories
{
    [ScopedRegistrationWithInterface]
    public interface ICandidateRepository : IBaseRepository<Candidate>
    {
        Candidate GetCandidate(int id);
    }
}