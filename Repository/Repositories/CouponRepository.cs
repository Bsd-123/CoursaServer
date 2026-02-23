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
        public async Task<Coupon> AddItem(Coupon item)
        {
            _context.Coupons.Add(item);
            await _context.SaveAsync();
            return item;
        }

        public async Task DeleteItem(int id)
        {
            _context.Coupons.Remove(await  GetById(id));
            await _context.SaveAsync();
        }

        public async Task<List<Coupon>> GetAll()
        {
            return await  _context.Coupons.Include(c => c.User).Include(c => c.Course).ToListAsync();
        }

        public async Task<Coupon> GetById(int id)
        {
            return await  _context.Coupons.Include(c => c.User).Include(c => c.Course).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task UpdateItem(int id, Coupon item)
        {
            var Coupon =await  GetById(id);
            Coupon.Name = item.Name;
            Coupon.Status = item.Status;
            Coupon.Value = item.Value;
            Coupon.UserId = item.UserId;
            Coupon.MinPrice = item.MinPrice;
            Coupon.EndDate = item.EndDate;
            Coupon.StartDate = item.StartDate;
            Coupon.IsPercentages = item.IsPercentages;
            Coupon.CourseId = item.CourseId;
            await  _context.SaveAsync();
        }
    }
}
