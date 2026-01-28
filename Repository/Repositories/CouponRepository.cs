using Repository.Entities;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    internal class CouponRepository:IRepository<Coupon>
    {
        private readonly IContext _context;
        public CouponRepository(IContext context)
        {
            this._context = context;
        }
        public Coupon AddItem(Coupon item)
        {
            _context.Coupons.ToList().Add(item);

            _context.save();
            return item;
        }

        public void DeleteItem(int id)
        {
            _context.Coupons.ToList().Remove(GetById(id));
            _context.save();
        }

        public List<Coupon> GetAll()
        {
            return _context.Coupons.ToList();
        }

        public Coupon GetById(int id)
        {
            return _context.Coupons.ToList().FirstOrDefault(x => x.Id == id);
        }

        public void UpdateItem(int id, Coupon item)
        {
            var Coupon = GetById(id);
            Coupon.Name = item.Name;
            _context.save();
        }
    }
}
