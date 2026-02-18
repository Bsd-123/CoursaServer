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
        public Enrollment AddItem(Enrollment item)
        {
            _context.Enrollments.Add(item);

            _context.save();
            return item;
        }

        public void DeleteItem(int id1, int id2)
        {
            _context.Enrollments.Remove(GetById(id1,id2));
            _context.save();
        }

        public List<Enrollment> GetAll()
        {
            return _context.Enrollments.Include(e => e.User).Include(e => e.Course).Include(e => e.Coupon).ToList();
        }

        public Enrollment GetById(int id1, int id2)
        {
            return _context.Enrollments.Include(e => e.User).Include(e => e.Course).Include(e => e.Coupon).FirstOrDefault(x => x.UserId == id1 && x.CourseId ==id2);
        }

        public Enrollment UpdateItem(int id1, int id2, Enrollment item)
        {
            var Enrollment = GetById(id1,id2);
            Enrollment.StartDate = item.StartDate;
            Enrollment.EndDate = item.EndDate;
            Enrollment.FullPrice = item.FullPrice;
            Enrollment.Status = item.Status;
            Enrollment.ReceptionNumber = item.ReceptionNumber;
            Enrollment.PaymentNumber = item.PaymentNumber;
            Enrollment.CouponId = item.CouponId;
            _context.save();
            return GetById(id1, id2);
        }
    }
}
