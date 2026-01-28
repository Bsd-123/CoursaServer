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
    public class OwnerService : IService<OwnerDto>
    {
        private readonly IRepository<Owner> repository;
        private readonly IMapper mapper;
        public OwnerService(IRepository<Owner> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        public OwnerDto AddItem(OwnerDto item)
        {
            return mapper.Map<Owner, OwnerDto>(repository.AddItem(mapper.Map<OwnerDto, Owner>(item)));
        }

        public void DeleteItem(int id)
        {
            repository.DeleteItem(id);
        }

        public List<OwnerDto> GetAll()
        {
            return mapper.Map<List<Owner>, List<OwnerDto>>(repository.GetAll());
        }

        public OwnerDto GetById(int id)
        {
            return mapper.Map<Owner, OwnerDto>(repository.GetById(id));
        }

        public void UpdateItem(int id, OwnerDto item)
        {
            repository.UpdateItem(id, mapper.Map<OwnerDto, Owner>(item));
        }
    }
}
