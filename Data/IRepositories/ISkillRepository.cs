using Common.ServiceRegistrationAttributes;
using Data.Entities;

namespace Data.IRepositories
{
    [ScopedRegistrationWithInterface]
    public interface ISkillRepository : IBaseRepository<Skill>
    {
        bool Exists(int id, string name);

        bool Exists(string name);
    }
}