using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Common.ServiceRegistrationAttributes;
using Data.Entities;
using Data.Repositories;
using Microsoft.Extensions.Logging;
using Services.DTOs.Skill;

namespace Services.Services
{
    [ScopedRegistration]
    public class SkillService
    {
        private readonly ILogger<SkillService> _logger;
        private readonly SkillRepository _repo;
        private readonly IMapper _mapper;
        private readonly RecruitmentSkillService _recruitmentSkillService;
        public SkillService(SkillRepository repo, ILogger<SkillService> logger, IMapper mapper, RecruitmentSkillService recruitmentSkillService)
        {
            _repo = repo;
            _logger = logger;
            _mapper = mapper;
            _recruitmentSkillService = recruitmentSkillService;
        }
        public IEnumerable<Skill> GetSkills()
        {
            IEnumerable<Skill> result;
            try
            {
                result = _repo.GetAll();
            }
            catch(Exception ex)
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
                    .Where(e => e.Name.Contains(name))
                    .OrderBy(e=>e.Name)
                    .Take(5);
            }
            catch(Exception ex)
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
            catch(Exception ex)
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
            catch(Exception ex)
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
                bool exists = _repo.Exists(dto.Name);

                if (exists == true) return 0;

                Skill skill = _repo.GetById(dto.Id);
                skill.Name = dto.Name;
                _repo.UpdateAndSaveChanges(skill);
            }
            catch(Exception ex)
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
                bool isUsed = _recruitmentSkillService.IsSkillUsed(id);

                if (isUsed == true) return 0;

                _repo.RemoveByIdAndSaveChanges(id);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return -1;
            }

            return 1;
        }
    }
}
