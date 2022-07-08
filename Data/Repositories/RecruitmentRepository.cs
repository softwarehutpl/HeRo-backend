using Data.Entities;

namespace Data.Repositories
{
    public class RecruitmentRepository : BaseRepository<Recruitment>
    {
        private readonly DataContext context;
        public RecruitmentRepository(DataContext context) : base(context)
        {
            this.context = context;
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
