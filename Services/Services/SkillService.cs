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
        private readonly ILogger logger;
        private readonly SkillRepository repo;
        private readonly IMapper mapper;
        public SkillService(SkillRepository repo, ILogger logger, IMapper mapper)
        {
            this.repo = repo;
            this.logger = logger;
            this.mapper = mapper;
        }
        public IEnumerable<SkillDTO> GetSkills()
        {
            IEnumerable<SkillDTO> result;
            try
            {
                IEnumerable<Skill> skills = repo.GetAllSkills();
                result = mapper.Map<IEnumerable<SkillDTO>>(skills);
            }catch(Exception ex)
            {
                logger.LogError(ex.Message);
                return null;
            }
            
            return result;
        }
        public SkillDTO GetSkill(int id)
        {
            SkillDTO result;
            try
            {
                Skill skill = repo.GetSkillById(id);
                result = mapper.Map<SkillDTO>(skill);
            }
            catch(Exception ex)
            {
                logger.LogError(ex.Message);
                return null;
            }

            return result;
        }
        public int AddSkill(SkillDTO dto)
        {
            try
            {
                Skill skill = mapper.Map<Skill>(dto);
                repo.AddSkill(skill);
            }catch(Exception ex)
            {
                logger.LogError(ex.Message);
                return -1;
            }
            
            return 1;
        }
        public int UpdateSkill(SkillDTO dto)
        {
            try
            {
                Skill skill = mapper.Map<Skill>(dto);
                repo.UpdateSkill(skill);
            }catch(Exception ex)
            {
                logger.LogError(ex.Message);
                return -1;
            }

            return 1;
        }
        public int DeleteSkill(int id)
        {
            try
            {
                repo.DeleteSkill(id);
            }catch(Exception ex)
            {
                logger.LogError(ex.Message);
                return -1;
            }

            return 1;
        }
    }
}
