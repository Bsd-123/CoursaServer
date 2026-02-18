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
        public Task<IActionResult> AddItem(OwnerDto item)
        {
            var owner = mapper.Map<OwnerDto, Owner>(item);
            var user = owner.User;
            user.Role = "owner";
            repository2.UpdateItem(user.Id,user);
            var result = mapper.Map<Owner, OwnerDto>(repository.AddItem(owner));
            return Task.FromResult<IActionResult>(new CreatedAtActionResult(null, null, null, result));
        }

        public void DeleteItem(int id)
        {
            var owner =repository.GetById(id);
            owner.Percentage = -1;
            owner.User.Role = "user";
            repository2.UpdateItem(owner.User.Id, owner.User);
            repository.UpdateItem(id,owner);
        }

        public List<OwnerDto> GetAll()
        {
            return mapper.Map<List<Owner>, List<OwnerDto>>(repository.GetAll());
        }

        public OwnerDto GetById(int id)
        {
            return mapper.Map<Owner, OwnerDto>(repository.GetById(id));
        }

        public Task<IActionResult> UpdateItem(int id, OwnerDto item)
        {
            repository.UpdateItem(id, mapper.Map<OwnerDto, Owner>(item));
            return Task.FromResult<IActionResult>(new NoContentResult());
        }
    }
}
