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
    public class OwnerRepository: IRepository<Owner>
    {
        private readonly IContext _context;
        public OwnerRepository(IContext context)
        {
            this._context = context;
        }
        public async Task<Owner> AddItem(Owner item)
        {
            _context.Owners.Add(item);
            await  _context.SaveAsync();
            return item;
        }

        public async Task DeleteItem(int id)
        {
            _context.Owners.Remove(await GetById(id));
            await _context.SaveAsync();
        }

        public async Task<List<Owner>> GetAll()
        {
            return _context.Owners.Include(o => o.User).ToList();
        }

        public async Task<Owner> GetById(int id)
        {
            return await _context.Owners.Include(o => o.User).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task UpdateItem(int id, Owner item)
        {
            var Owner =await GetById(id);
            Owner.OwnerName = item.OwnerName;
            Owner.PaymentNumber = item.PaymentNumber;
            Owner.Percentage = item.Percentage;
            Owner.Image = item.Image;
            Owner.UserId = item.UserId;
            await _context.SaveAsync();
        }
    }
}
