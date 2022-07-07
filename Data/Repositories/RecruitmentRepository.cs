using Data.Entities;

namespace Data.Repositories
{
    public class RecruitmentRepository 
    {
        private readonly DataContext context;
        public RecruitmentRepository(DataContext context)
        {
            this.context = context;
        }
        public Recruitment GetRecruitmentById(int id)
        {
            Recruitment result = context.Recruitments.Find(id);
            if (result == default) return null;

            return result;
        }

        public List<Recruitment> GetAllRecruitments()
        {
            List<Recruitment> result = context.Recruitments.ToList();

            return result;
        }

        public int AddRecruitment(Recruitment recruitment)
        {
            try
            {
                context.Recruitments.Add(recruitment);
                context.SaveChanges();
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
                context.Recruitments.Update(recruitment);
                context.SaveChanges();
            }catch(Exception ex)
            {
                return -1;
            }
            return 1;
        }

        public int RemoveRecruitment(int id)
        {
            Recruitment recruitment = context.Recruitments.Find(id);

            if (recruitment == default) return -1;

            try
            {
                context.Recruitments.Remove(recruitment);
                context.SaveChanges();
            }catch(Exception ex)
            {
                return -1;
            }
                
            return 1;
        }
    }
}
