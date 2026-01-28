using AutoMapper;
using Repository.Entities;
using Repository.Interfaces;
using Service.Dto;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class SkillService:IService<SkillDto>
    {
        private readonly IRepository<Skill> repository;
        private readonly IMapper mapper;
        public SkillService(IRepository<Skill> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        public SkillDto AddItem(SkillDto item)
        {
            return mapper.Map<Skill, SkillDto>(repository.AddItem(mapper.Map<SkillDto, Skill>(item)));
        }

        public void DeleteItem(int id)
        {
            repository.DeleteItem(id);
        }

        public List<SkillDto> GetAll()
        {
            return mapper.Map<List<Skill>, List<SkillDto>>(repository.GetAll());
        }

        public SkillDto GetById(int id)
        {
            return mapper.Map<Skill, SkillDto>(repository.GetById(id));
        }

        public void UpdateItem(int id, SkillDto item)
        {
            repository.UpdateItem(id, mapper.Map<SkillDto, Skill>(item));
        }
    }
}
