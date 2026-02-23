using AutoMapper;
using Microsoft.AspNetCore.Http;
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
    public class OwnerService : IService<OwnerDto>
    {
        private readonly IRepository<Owner> repository;
        private readonly IRepository<User> repository2;
        private readonly IMapper mapper;
        public OwnerService(IRepository<Owner> repository, IRepository<User> repository2, IMapper mapper)
        {
            this.repository = repository;
            this.repository2 = repository2;
            this.mapper = mapper;
        }
        public async Task<OwnerDto> AddItem(OwnerDto item)
        {
            var owner = mapper.Map<OwnerDto, Owner>(item);
            var user = owner.User;
            user.Role = "owner";
            await repository2.UpdateItem(user.Id,user);
            var result = mapper.Map<Owner, OwnerDto>( await repository.AddItem(owner));
            return result;
        }

        public async Task DeleteItem(int id)
        {
            var owner =await repository.GetById(id);
            owner.Percentage = -1;
            owner.User.Role = "user";
            await repository2.UpdateItem(owner.User.Id, owner.User);
            await repository.UpdateItem(id, owner);
        }

        public async Task<List<OwnerDto>> GetAll()
        {
            return mapper.Map<List<Owner>, List<OwnerDto>>(await repository.GetAll());
        }

        public async Task<OwnerDto> GetById(int id)
        {
            return mapper.Map<Owner, OwnerDto>(await repository.GetById(id));
        }

        public async Task UpdateItem(int id, OwnerDto item)
        {
            await repository.UpdateItem(id, mapper.Map<OwnerDto, Owner>(item));
        }
    }
}
