using Common.ServiceRegistrationAttributes;
using Data.DTOs.Skill;
using Data.Entities;
using Data.IRepositories;
using Microsoft.Extensions.Logging;

namespace Services.Services
{
    [ScopedRegistration]
    public class SkillService
    {
        private readonly ILogger<SkillService> _logger;
        private readonly ISkillRepository _repo;
        private readonly IRecruitmentSkillRepository _recruitmentSkillRepo;

        public SkillService(ISkillRepository repo, ILogger<SkillService> logger, IRecruitmentSkillRepository recruitmentSkillRepo)
        {
            _repo = repo;
            _logger = logger;
            _recruitmentSkillRepo = recruitmentSkillRepo;
        }

        public IEnumerable<Skill> GetSkills()
        {
            IEnumerable<Skill> result;
            try
            {
                result = _repo.GetAll();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }

            return result;
        }

        public IEnumerable<Skill> GetSkillsFilteredByName(string name)
        {
            IEnumerable<Skill> result;
            try
            {
                result = _repo.GetAll();
                result = result
                    .Where(e => e.Name.ToLower().Contains(name.ToLower()))
                    .OrderBy(e => e.Name)
                    .Take(5);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }

            return result;
        }

        public Skill GetSkill(int id)
        {
            Skill result;
            try
            {
                result = _repo.GetById(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }

            return result;
        }

        public int AddSkill(string skillName)
        {
            try
            {
                bool exists = _repo.Exists(skillName);

                if (exists == true) return 0;

                Skill skill = new Skill(skillName);
                _repo.AddAndSaveChanges(skill);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return -1;
            }

            return 1;
        }

        public int UpdateSkill(UpdateSkillDTO dto)
        {
            try
            {
                Skill skill = _repo.GetById(dto.Id);

                if (skill == null) return -2;

                bool exists = _repo.Exists(dto.Id, dto.Name);

                if (exists == true) return 0;
  
                skill.Name = dto.Name;
                _repo.UpdateAndSaveChanges(skill);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return -1;
            }

            return 1;
        }

        public int DeleteSkill(int id)
        {
            try
            {
                Skill skill = _repo.GetById(id);

                if (skill == null) return -1;

                bool isUsed = _recruitmentSkillRepo.IsSkillUsed(id);

                if (isUsed == true) return 0;

                _repo.RemoveByIdAndSaveChanges(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return -2;
            }

            return 1;
        }
    }
}