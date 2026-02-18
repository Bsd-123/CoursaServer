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
    public class UserService : IService<UserDto>
    {
        private readonly IRepository<User> repository;
        private readonly IMapper mapper;
        public UserService(IRepository<User> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public Task<IActionResult> AddItem(UserDto item)
        {
            var result =  mapper.Map<User, UserDto>(repository.AddItem(mapper.Map <UserDto, User> (item)));
            return Task.FromResult<IActionResult>(new CreatedAtActionResult(null, null, null, result));
        }
        public void DeleteItem(int id)
        {
            repository.DeleteItem(id);
        }

        public List<UserDto> GetAll()
        {
            return mapper.Map<List<User>, List<UserDto>>(repository.GetAll()).Where(x=> x.Role != "Delete").ToList();
        }

        public UserDto GetById(int id)
        {
            return mapper.Map<User, UserDto>(repository.GetById(id));
        }

        public Task<IActionResult> UpdateItem(int id, UserDto item)
        {
            repository.UpdateItem(id, mapper.Map<UserDto, User>(item));
            return Task.FromResult<IActionResult>(new NoContentResult());
        }
    }
}
