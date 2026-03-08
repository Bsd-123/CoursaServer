using Microsoft.EntityFrameworkCore;
using Repository.Entities;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class UserRepository : IRepository<User>
    {
        private readonly IContext _context;
        public UserRepository(IContext context)
        {
            this._context = context;
        }
        public async Task<User> AddItem(User item)
        {
            _context.Users.Add(item);
            await _context.SaveAsync();
            return item;
        }

        public async Task DeleteItem(int id)
        {
            _context.Users.Remove(await GetById(id));
            await _context.SaveAsync();
        }

        public async Task<List<User>> GetAll()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetById(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task UpdateItem(int id, User item)
        {
            var User = await GetById(id);
            User.Name = item.Name;
            User.Email = item.Email;
            //User.Password = item.Password;
            User.Role = item.Role;
            User.RegDate = item.RegDate;
            User.ResetPasswordToken = item.ResetPasswordToken;
            User.ResetTokenExpires = item.ResetTokenExpires;
            await  _context.SaveAsync();
        }
    }
}
