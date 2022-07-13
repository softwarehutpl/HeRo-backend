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
        private readonly ILogger<SkillService> _logger;
        private readonly SkillRepository _repo;
        private readonly IMapper _mapper;
        public SkillService(SkillRepository repo, ILogger<SkillService> logger, IMapper mapper)
        {
            _repo = repo;
            _logger = logger;
            _mapper = mapper;
        }
        public IEnumerable<ReadSkillDTO> GetSkills()
        {
            IEnumerable<ReadSkillDTO> result;
            try
            {
                IEnumerable<Skill> skills = _repo.GetAllSkills();
                result = _mapper.Map<IEnumerable<ReadSkillDTO>>(skills);
            }catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
            
            return result;
        }
        public IEnumerable<ReadSkillDTO> GetSkillsFilteredByName(string name)
        {
            IEnumerable<ReadSkillDTO> result;
            try
            {
                IEnumerable<Skill> skills = _repo.GetAllSkills();
                skills = skills.Where(e => e.Name.Contains(name));
                skills = skills.Take(5);

                result= _mapper.Map<IEnumerable<ReadSkillDTO>>(skills);
            }catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }

            return result;
        }
        public ReadSkillDTO GetSkill(int id)
        {
            ReadSkillDTO result;
            try
            {
                Skill skill = _repo.GetSkillById(id);
                result = _mapper.Map<ReadSkillDTO>(skill);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }

            return result;
        }
        public int AddSkill(CreateSkillDTO dto)
        {
            try
            {
                Skill skill = _mapper.Map<Skill>(dto);
                _repo.AddAndSaveChanges(skill);
            }catch(Exception ex)
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
                Skill skill = _repo.GetSkillById(dto.Id);
                skill.Name = dto.Name;
                _repo.UpdateAndSaveChanges(skill);
            }catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return -1;
            }

            return 1;
        }

        public void UpdateUsed(int id, bool used)
        {
            try
            {
                Skill skill = _repo.GetSkillById(id);
                skill.isUsed = used;
                _repo.UpdateAndSaveChanges(skill);
            }catch(Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }

        public int DeleteSkill(int id)
        {
            try
            {
                Skill skill = _repo.GetSkillById(id);
                bool isUsed = skill.isUsed;

                if (isUsed == true) return 0;

                _repo.RemoveByIdAndSaveChanges(id);
            }catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return -1;
            }

            return 1;
        }
    }
}
