using Microsoft.AspNetCore.Mvc;
using Repository.Entities;
using Service.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Service.Interfaces
{
    public interface ILogin
    {
        public Task<UserDto> Authenticate(UserForLogin user);
        public Task<UserDto> GetByEmail(string email);
        public Task<UserDto> AddUser(User item);
    }
}
