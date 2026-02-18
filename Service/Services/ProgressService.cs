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
    public class ProgressService : IServiceDouble<ProgressDto>
    {
        private readonly IRepositoryDouble<Progress> repository;
        private readonly IMapper mapper;
        public ProgressService(IRepositoryDouble<Progress> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        public ProgressDto AddItem(ProgressDto item)
        {
            return mapper.Map<Progress, ProgressDto>(repository.AddItem(mapper.Map<ProgressDto, Progress>(item)));
        }

        public void DeleteItem(int id1, int id2)
        {
            repository.DeleteItem(id1, id2);
        }

        public List<ProgressDto> GetAll()
        {
            return mapper.Map<List<Progress>, List<ProgressDto>>(repository.GetAll());
        }

        public ProgressDto GetById(int id1, int id2)
        {
            return mapper.Map<Progress, ProgressDto>(repository.GetById(id1, id2));
        }

        public void UpdateItem(int id1, int id2, ProgressDto item)
        {
            repository.UpdateItem(id1, id2, mapper.Map<ProgressDto, Progress>(item));
        }
    }
}
