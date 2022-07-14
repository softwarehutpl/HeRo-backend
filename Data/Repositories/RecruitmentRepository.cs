using Common.Enums;
using Data.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Data.Repositories
{
    public class RecruitmentRepository : BaseRepository<Recruitment>
    {
        public RecruitmentRepository(DataContext context) : base(context)
        {
        }

        public int GetRecruiterId(int id)
        {
            Recruitment recruitment = GetById(id);

            int result = recruitment.RecruiterId;

            return result;
        }

        public Recruitment GetRecruitmentById(int id)
        {
            Recruitment result = DataContext.Recruitments
                .Include(x => x.Candidates)
                .FirstOrDefault();

            if (result == default) return null;

            return result;
        }

        public IEnumerable<Recruitment> GetAllRecruitments()
        {
            IEnumerable<Recruitment> result = DataContext.Recruitments.Include(x => x.Candidates).ToList();

            return result;
        }
    }
}