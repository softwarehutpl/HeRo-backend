using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Data.Entities;
using Data.Repositories;
using Microsoft.Extensions.Logging;
using Services.DTOs.Skill;

namespace Services.Services
{
    public class SkillService
    {
        private readonly ILogger<SkillService> logger;
        private readonly SkillRepository repo;
        private readonly IMapper mapper;
        public SkillService(SkillRepository repo, ILogger<SkillService> logger, IMapper mapper)
        {
            this.repo = repo;
            this.logger = logger;
            this.mapper = mapper;
        }
        public IEnumerable<ReadSkillDTO> GetSkills()
        {
            IEnumerable<ReadSkillDTO> result;
            try
            {
                IEnumerable<Skill> skills = repo.GetAllSkills();
                result = mapper.Map<IEnumerable<ReadSkillDTO>>(skills);
            }catch(Exception ex)
            {
                logger.LogError(ex.Message);
                return null;
            }
            
            return result;
        }
        public IEnumerable<ReadSkillDTO> GetSkillsFilteredByName(string name)
        {
            IEnumerable<ReadSkillDTO> result;
            try
            {
                IEnumerable<Skill> skills = repo.GetAllSkills();
                skills = skills.Where(e => e.Name.Contains(name));
                skills = skills.Take(5);

                result= mapper.Map<IEnumerable<ReadSkillDTO>>(skills);
            }catch(Exception ex)
            {
                logger.LogError(ex.Message);
                return null;
            }

            return result;
        }
        public ReadSkillDTO GetSkill(int id)
        {
            ReadSkillDTO result;
            try
            {
                Skill skill = repo.GetSkillById(id);
                result = mapper.Map<ReadSkillDTO>(skill);
            }
            catch(Exception ex)
            {
                logger.LogError(ex.Message);
                return null;
            }

            return result;
        }
        public int AddSkill(CreateSkillDTO dto)
        {
            try
            {
                Skill skill = mapper.Map<Skill>(dto);
                repo.AddAndSaveChanges(skill);
            }catch(Exception ex)
            {
                logger.LogError(ex.Message);
                return -1;
            }
            
            return 1;
        }
        public int UpdateSkill(UpdateSkillDTO dto)
        {
            try
            {
                Skill skill = repo.GetSkillById(dto.Id);
                skill.Name = dto.Name;
                repo.UpdateAndSaveChanges(skill);
            }catch(Exception ex)
            {
                logger.LogError(ex.Message);
                return -1;
            }

            return 1;
        }

        public void UpdateUsed(int id, bool used)
        {
            try
            {
                Skill skill = repo.GetSkillById(id);
                skill.isUsed = used;
                repo.UpdateAndSaveChanges(skill);
            }catch(Exception ex)
            {
                logger.LogError(ex.Message);
            }
        }

        public int DeleteSkill(int id)
        {
            try
            {
                Skill skill = repo.GetSkillById(id);
                bool isUsed = skill.isUsed;

                if (isUsed == true) return 0;

                repo.RemoveByIdAndSaveChanges(id);
            }catch(Exception ex)
            {
                logger.LogError(ex.Message);
                return -1;
            }

            return 1;
        }
    }
}
