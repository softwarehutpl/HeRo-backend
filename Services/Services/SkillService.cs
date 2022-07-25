using Common.Helpers;
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

        public bool AddSkill(string skillName, out string errorMessage)
        {
            try
            {
                bool exists = _repo.Exists(skillName);

                if (exists == true) 
                {
                    errorMessage = ErrorMessageHelper.SkillExists;
                    return false; 
                }

                Skill skill = new Skill(skillName);
                _repo.AddAndSaveChanges(skill);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                errorMessage = ErrorMessageHelper.WrongData;
                return false;
            }
            errorMessage = "";
            return true;
        }

        public bool UpdateSkill(UpdateSkillDTO dto, out string errorMessage)
        {
            try
            {
                Skill skill = _repo.GetById(dto.Id);

                if (skill == null)
                {
                    errorMessage = ErrorMessageHelper.NoSkill;
                    return false;
                }

                bool exists = _repo.Exists(dto.Id, dto.Name);

                if (exists == true)
                {
                    errorMessage = ErrorMessageHelper.SkillExists;
                    return false;
                }
  
                skill.Name = dto.Name;
                _repo.UpdateAndSaveChanges(skill);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                {
                    errorMessage = ErrorMessageHelper.WrongData;
                    return false;
                }
            }
            errorMessage = "";
            return true;
        }

        public bool DeleteSkill(int id, out string error)
        {
            try
            {
                Skill skill = _repo.GetById(id);

                if (skill == null)
                {   
                    error = ErrorMessageHelper.NoSkill;
                    return false;
                }

                bool isUsed = _recruitmentSkillRepo.IsSkillUsed(id);

                if (isUsed == true)
                {
                    error = ErrorMessageHelper.SkillIsUsed;
                    return false;
                }

                _repo.RemoveByIdAndSaveChanges(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                error = ErrorMessageHelper.ErrorDeletingSkill;
                return false;
            }
            error = "";
            return true;
        }
    }
}