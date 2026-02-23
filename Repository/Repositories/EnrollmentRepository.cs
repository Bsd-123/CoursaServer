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
    public class EnrollmentRepository:IRepositoryDouble<Enrollment>
    {
        private readonly IContext _context;
        public EnrollmentRepository(IContext context)
        {
            this._context = context;
        }
        public async Task<Enrollment> AddItem(Enrollment item)
        {
            _context.Enrollments.Add(item);
            await _context.SaveAsync();
            return item;
        }

        public async Task DeleteItem(int id1, int id2)
        {
            _context.Enrollments.Remove(await  GetById(id1,id2));
            await _context.SaveAsync();
        }

        public async Task<List<Enrollment>> GetAll()
        {
            return await _context.Enrollments.Include(e => e.User).Include(e => e.Course).Include(e => e.Coupon).ToListAsync();
        }

        public async Task<Enrollment> GetById(int id1, int id2)
        {
            return await  _context.Enrollments.Include(e => e.User).Include(e => e.Course).Include(e => e.Coupon).FirstOrDefaultAsync(x => x.UserId == id1 && x.CourseId ==id2);
        }

        public async Task UpdateItem(int id1, int id2, Enrollment item)
        {
            var Enrollment = await  GetById(id1,id2);
            Enrollment.StartDate = item.StartDate;
            Enrollment.EndDate = item.EndDate;
            Enrollment.FullPrice = item.FullPrice;
            Enrollment.Status = item.Status;
            Enrollment.ReceptionNumber = item.ReceptionNumber;
            Enrollment.PaymentNumber = item.PaymentNumber;
            Enrollment.CouponId = item.CouponId;
            await _context.SaveAsync();
        }
    }
}
