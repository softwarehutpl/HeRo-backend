using Common.Enums;
using Data.Entities;
using System.Security.Claims;

namespace Data.Repositories
{
    public class RecruitmentRepository : BaseRepository<Recruitment>
    {
        private readonly DataContext context;
        private readonly UserRepository userRepo;
        public RecruitmentRepository(DataContext context, UserRepository userRepo) : base(context)
        {
            this.context = context;
            this.userRepo = userRepo;
        }
        public Recruitment GetRecruitmentById(int id)
        {
            Recruitment result = GetById(id);

            if (result == default) return null;

            return result;
        }

        public List<Recruitment> GetAllRecruitments()
        {
            List<Recruitment> result = GetAll().ToList();

            return result;
        }

        public int ChangeStatus(Recruitment recruitment)
        {
            
            try
            {
                List<Claim> claims = ClaimsPrincipal.Current.Claims.ToList();
                Claim emailClaim = claims.FirstOrDefault(e=>e.Type==ClaimTypes.Email);
                User user = userRepo.GetUserByEmail(emailClaim.Value);

                recruitment.LastUpdatedById = user.Id;
                if (recruitment.Status == RecruitmentStatusEnum.Closed.ToString()) recruitment.EndedById = user.Id;

                UpdateAndSaveChanges(recruitment);
            }catch(Exception ex)
            {
                return - 1;
            }
            return 1;
        }

        public int AddRecruitment(Recruitment recruitment)
        {
            try
            {
                AddAndSaveChanges(recruitment);
            }catch(Exception ex)
            {
                return -1;
            }
            return 1;
        }
        public int UpdateRecruitment(Recruitment recruitment)
        {
            try
            {
                UpdateAndSaveChanges(recruitment);
            }catch(Exception ex)
            {
                return -1;
            }
            return 1;
        }

        public int RemoveRecruitment(int id)
        {
            try
            {
                RemoveByIdAndSaveChanges(id);
            }catch(Exception ex)
            {
                return -1;
            }
                
            return 1;
        }
    }
}
