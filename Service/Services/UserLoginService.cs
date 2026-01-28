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
    public class UserLoginService :ILogin
    {
        private readonly IRepository<User> _repository;
        public UserLoginService(IRepository<User> _repository)
        {
            this._repository = _repository;
        }
        public User Authenticate(UserLogin user)
        {
            return _repository.GetAll().FirstOrDefault(x => x.Email == user.Email && x.Password == user.Password);

        }
        public User GetByEmail(string email)
        {
            return _repository.GetAll().FirstOrDefault(x => x.Email == email);
        }
        public User AddUser(User item)
        {
            return _repository.AddItem(item);
        }
    }
}
