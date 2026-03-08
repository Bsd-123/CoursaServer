using AutoMapper;
using Repository.Entities;
using Repository.Interfaces;
using Service.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class UserToAddingService
    {
        private readonly IMapper mapper;
        private readonly IRepository<User> repository;
        public UserToAddingService(IRepository<User> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        public async Task<UserDto> AddItem(UserToAdding item)
        {
            return mapper.Map<User, UserDto>(await repository.AddItem(mapper.Map<UserToAdding, User>(item)));
        }
    }
}
