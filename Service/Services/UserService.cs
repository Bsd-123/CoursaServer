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

        public async Task<UserDto> AddItem(UserDto item)
        {
            var result =  mapper.Map<User, UserDto>(await repository.AddItem(mapper.Map <UserDto, User> (item)));
            return result;
        }
        public async Task DeleteItem(int id)
        {
            await repository.DeleteItem(id);
        }

        public async Task<List<UserDto>> GetAll()
        {
            return  mapper.Map<List<User>, List<UserDto>>(await repository.GetAll()).Where(x=> x.Role != "Delete").ToList();
        }

        public async Task<UserDto> GetById(int id)
        {
            return mapper.Map<User, UserDto>(await repository.GetById(id));
        }

        public async Task UpdateItem(int id, UserDto item)
        {
            await repository.UpdateItem(id, mapper.Map<UserDto, User>(item));
        }
        
    }
}
