using Common.ServiceRegistrationAttributes;
using Data.Entities;

namespace Data.IRepositories
{
    [ScopedRegistrationWithInterface]
    public interface IInterviewRepository : IBaseRepository<Interview>
    {
        Interview GetInterview(int id);
    }
}