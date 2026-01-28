using Repository.Entities;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    internal class OwnerRepository: IRepository<Owner>
    {
        private readonly IContext _context;
        public OwnerRepository(IContext context)
        {
            this._context = context;
        }
        public Owner AddItem(Owner item)
        {
            _context.Owners.ToList().Add(item);

            _context.save();
            return item;
        }

        public void DeleteItem(int id)
        {
            _context.Owners.ToList().Remove(GetById(id));
            _context.save();
        }

        public List<Owner> GetAll()
        {
            return _context.Owners.ToList();
        }

        public Owner GetById(int id)
        {
            return _context.Owners.ToList().FirstOrDefault(x => x.Id == id);
        }

        public void UpdateItem(int id, Owner item)
        {
            var Owner = GetById(id);
            Owner.OwnerName = item.OwnerName;

            _context.save();
        }
    }
}
