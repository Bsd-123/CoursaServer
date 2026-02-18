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
        public Owner AddItem(Owner item)
        {
            _context.Owners.Add(item);

            _context.save();
            return item;
        }

        public void DeleteItem(int id)
        {
            _context.Owners.Remove(GetById(id));
            _context.save();
        }

        public List<Owner> GetAll()
        {
            return _context.Owners.Include(o => o.User).ToList();
        }

        public Owner GetById(int id)
        {
            return _context.Owners.Include(o => o.User).FirstOrDefault(x => x.Id == id);
        }

        public Owner UpdateItem(int id, Owner item)
        {
            var Owner = GetById(id);
            Owner.OwnerName = item.OwnerName;
            Owner.PaymentNumber = item.PaymentNumber;
            Owner.Percentage = item.Percentage;
            Owner.Image = item.Image;
            Owner.UserId = item.UserId;
            _context.save();
            return GetById(id);
        }
    }
}
