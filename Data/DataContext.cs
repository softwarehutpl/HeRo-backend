using Microsoft.EntityFrameworkCore;
using Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Data.Repositories;

namespace Data
{
    
    public class DataContext : DbContext
    {

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Candidate> Candidates { get; set; }
        //public DbSet<Role>  Roles { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<User> AspNetUsers { get; set; }

        public DbSet<IdentityUserClaim<string>> AspNetUserClaims { get; set; }
        public DbSet<IdentityUserLogin<string>> AspNetUserLogins { get; set; }
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
        public void AttachEntity<TEntity>(TEntity entity) where TEntity : class, new()
        {
            Set<TEntity>().Attach(entity);
        }

        public void AddEntity<TEntity>(TEntity entity) where TEntity : class, new()
        {
            Set<TEntity>().Add(entity);
        }



        public void AddEntityAndSaveChanges<TEntity>(TEntity entity) where TEntity : class, new()
        {
            AddEntity(entity);
            SaveChanges();
        }



        public void AddEntitiesRange<TEntity>(IEnumerable<TEntity> entity) where TEntity : class, new()
        {
            Set<TEntity>().AddRange(entity);
        }



        public void AddEntitiesRangeAndSaveChanges<TEntity>(IEnumerable<TEntity> entity) where TEntity : class, new()
        {
            AddEntitiesRange(entity);
            SaveChanges();
        }



        public void UpdateEntity<TEntity>(TEntity entity) where TEntity : class, new()
        {
            Entry(entity).State = EntityState.Modified;
        }



        public void UpdateEntityAndSaveChanges<TEntity>(TEntity entity) where TEntity : class, new()
        {
            UpdateEntity(entity);
            SaveChanges();
        }



        public void UpdateEntitiesRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class, IEntity, new()
        {
            foreach (var entity in entities)
            {
                UpdateEntity(entity);
            }
        }



        public void UpdateEntitiesRangeAndSaveChanges<TEntity>(IEnumerable<TEntity> entities) where TEntity : class, IEntity, new()
        {
            UpdateEntitiesRange(entities);
            SaveChanges();
        }



        public void RemoveEntity<TEntity>(TEntity entity) where TEntity : class, new()
        {
            Set<TEntity>().Remove(entity);
        }



        public void RemoveEntitiesRange<TEntity>(IEnumerable<TEntity> entity) where TEntity : class, new()
        {
            Set<TEntity>().RemoveRange(entity);
        }



        public void RemoveEntitiesRangeAndSaveChanges<TEntity>(IEnumerable<TEntity> entity) where TEntity : class, new()
        {
            RemoveEntitiesRange(entity);
            SaveChanges();
        }
    }
}
