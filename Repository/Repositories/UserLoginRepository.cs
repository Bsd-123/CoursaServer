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
    public class UserLoginRepository : IRepository<UserLogin>
    {
        private readonly IContext _context;
        public UserLoginRepository(IContext context)
        {
            this._context = context;
        }
        public async Task<UserLogin> AddItem(UserLogin item)
        {
            _context.UserLogins.Add(item);
            await _context.SaveAsync();
            return item;
        }

        public async Task DeleteItem(int id)
        {
            _context.UserLogins.Remove(await GetById(id));
            await _context.SaveAsync();
        }

        public async Task<List<UserLogin>> GetAll()
        {
            return await _context.UserLogins.ToListAsync();
        }

        public async Task<UserLogin> GetById(int id)
        {
            return await _context.UserLogins.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task UpdateItem(int id, UserLogin item)
        {
            var UserLogin = await GetById(id);
            UserLogin.Ipaddress = item.Ipaddress;
            UserLogin.LoginDate = item.LoginDate;
            UserLogin.DeviceType = item.DeviceType;
            await _context.SaveAsync();
        }
    }
}
