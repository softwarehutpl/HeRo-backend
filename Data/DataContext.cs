using Microsoft.EntityFrameworkCore;
using Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

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
        public DbSet<RecruitmentRequirement> RecruitmentRequirements { get; set; }
        public DbSet<Recruitment> Recruitments { get; set; }


        public class ApplicationDbContext : IdentityDbContext<IdentityUser>
        {
            public ApplicationDbContext()
                : base()
            {
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Ignore<IdentityUserLogin>();
            modelBuilder.Entity<RecruitmentRequirement>().HasKey(u => new
            {
                u.RecruitmentId,
                u.SkillId
            });

            modelBuilder.Entity<IdentityUserLogin<string>>().HasKey(u => new
            {
                u.UserId
            });
        }
    }
}
