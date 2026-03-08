using AutoMapper;
using Repository.Entities;
using Repository.Interfaces;
using Service.Dto;
using Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class UserForLoginService :ILogin
    {
        private readonly IRepository<User> _repository;
        private readonly IMapper mapper;
        public UserForLoginService(IRepository<User> _repository, IMapper mapper)
        {
            this._repository = _repository;
            this.mapper = mapper;
        }
        public async Task<UserDto> Authenticate(UserForLogin user)
        {
            return mapper.Map <User,UserDto > ((await _repository.GetAll()).FirstOrDefault(x => x.Email == user.Email && x.Password == user.Password));
        }
        public async Task<UserDto> GetByUserId(int id)
        {
            return mapper.Map<User, UserDto>((await _repository.GetAll()).FirstOrDefault(x => x.Id == id));
        }
        public async Task<UserDto> AddUser(User item)
        {
            return mapper.Map<User, UserDto>(await _repository.AddItem(item));
        }
        //public async Task<UserDto> Authenticate(UserForLogin user)
        //{
        //    // קריאה לפונקציה ב-Repository שבודקת רק משתמש אחד ב-DB
        //    var authUser = await repository.GetByEmailAndPassword(user.Email, user.Password);

        //    if (authUser == null) return null; // טיפול במקרה של פרטים שגויים

        //    return mapper.Map<User, UserDto>(authUser);
        //}
    }
}
