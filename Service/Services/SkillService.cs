using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<SkillDto> AddItem(SkillDto item)
        {
            return mapper.Map<Skill, SkillDto>(await repository.AddItem(mapper.Map<SkillDto, Skill>(item)));
        }

        public async Task DeleteItem(int id)
        {
            await repository.DeleteItem(id);
        }

        public async Task<List<SkillDto>> GetAll()
        {
            return mapper.Map<List<Skill>, List<SkillDto>>(await repository.GetAll());
        }

        public async Task<SkillDto> GetById(int id)
        {
            return mapper.Map<Skill, SkillDto>(await repository.GetById(id));
        }

        public async Task UpdateItem(int id, SkillDto item)
        {
            await repository.UpdateItem(id, mapper.Map<SkillDto, Skill>(item));
        }
    }
}
