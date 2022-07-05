using Microsoft.EntityFrameworkCore;
using Data.Entities;

namespace Data
{
    
    public class DataContext : DbContext
    {

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<Role>  Roles { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<CampainRequirement> CampainRequirements { get; set; }
        public DbSet<Recruitment> Recruitments { get; set; }
    }
}
