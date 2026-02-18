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
    public class CouponRepository:IRepository<Coupon>
    {
        private readonly IContext _context;
        public CouponRepository(IContext context)
        {
            this._context = context;
        }
        public Coupon AddItem(Coupon item)
        {
            _context.Coupons.Add(item);
            _context.save();
            return item;
        }

        public void DeleteItem(int id)
        {
            _context.Coupons.Remove(GetById(id));
            _context.save();
        }

        public List<Coupon> GetAll()
        {
            return _context.Coupons.Include(c => c.User).Include(c => c.Course).ToList();
        }

        public Coupon GetById(int id)
        {
            return _context.Coupons.Include(c => c.User).Include(c => c.Course).FirstOrDefault(x => x.Id == id);
        }

        public Coupon UpdateItem(int id, Coupon item)
        {
            var Coupon = GetById(id);
            Coupon.Name = item.Name;
            Coupon.Status = item.Status;
            Coupon.Value = item.Value;
            Coupon.UserId = item.UserId;
            Coupon.MinPrice = item.MinPrice;
            Coupon.EndDate = item.EndDate;
            Coupon.StartDate = item.StartDate;
            Coupon.IsPercentages = item.IsPercentages;
            Coupon.CourseId = item.CourseId;
            _context.save();
            return GetById(id);
        }
    }
}
