using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data
{

    public class DataContext : DbContext
    {

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<Recruitment> Recruitments { get; set; }
        public DbSet<RecruitmentRequirement> RecruitmentRequirements { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<RecruitmentRequirement>().HasKey(u => new
            {
                u.RecruitmentId,
                u.SkillId
            });
        }
    }
}
