using Common.Enums;
using Data.Entities;
using Microsoft.Data.SqlClient;
using System.Security.Claims;

namespace Data.Repositories
{
    public class RecruitmentRepository : BaseRepository<Recruitment>
    {
        public RecruitmentRepository(DataContext context) : base(context)
        {
            
        }
        public Recruitment GetRecruitmentById(int id)
        {
            Recruitment result = GetById(id);

            if (result == default) return null;

            return result;
        }

        public IQueryable<Recruitment> GetAllRecruitments()
        {
            IQueryable<Recruitment> result = GetAll();

            return result;
        }
    }
}
