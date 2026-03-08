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
    public class EnrollmentRepository:IRepository<Enrollment>
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

        public async Task DeleteItem(int id)
        {
            _context.Enrollments.Remove(await GetById(id));
            await _context.SaveAsync();
        }

        public async Task<List<Enrollment>> GetAll()
        {
            return await _context.Enrollments.Include(e => e.User).Include(e => e.Course).Include(e => e.CouponOwner).ToListAsync();
        }

        public async Task<Enrollment> GetById(int id)
        {
            return await  _context.Enrollments.Include(e => e.User).Include(e => e.Course).Include(e => e.CouponOwner).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task UpdateItem(int id, Enrollment item)
        {
            var Enrollment = await  GetById(id);
            Enrollment.StartDate = item.StartDate;
            Enrollment.EndDate = item.EndDate;
            Enrollment.FullPrice = item.FullPrice;
            Enrollment.Status = item.Status;
            Enrollment.ReceptionNumber = item.ReceptionNumber;
            Enrollment.PaymentNumber = item.PaymentNumber;
            Enrollment.CouponOwnerId = item.CouponOwnerId;

            await _context.SaveAsync();
        }
    }
}
