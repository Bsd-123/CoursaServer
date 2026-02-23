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
    public class ProgressService : IServiceDouble<ProgressDto>
    {
        private readonly IRepositoryDouble<Progress> repository;
        private readonly IMapper mapper;
        public ProgressService(IRepositoryDouble<Progress> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        public async Task<ProgressDto> AddItem(ProgressDto item)
        {
            return mapper.Map<Progress, ProgressDto>(await repository.AddItem(mapper.Map<ProgressDto, Progress>(item)));
        }

        public async Task DeleteItem(int id1, int id2)
        {
            await repository.DeleteItem(id1, id2);
        }

        public async Task<List<ProgressDto>> GetAll()
        {
            return mapper.Map<List<Progress>, List<ProgressDto>>(await repository.GetAll());
        }

        public async Task<ProgressDto> GetById(int id1, int id2)
        {
            return mapper.Map<Progress, ProgressDto>(await repository.GetById(id1, id2));
        }

        public async Task UpdateItem(int id1, int id2, ProgressDto item)
        {
            await repository.UpdateItem(id1, id2, mapper.Map<ProgressDto, Progress>(item));
        }
    }
}
