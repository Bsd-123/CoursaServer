using Repository.Entities;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    internal class EnrollmentRepository:IRepository<Enrollment>
    {
        private readonly IContext _context;
        public EnrollmentRepository(IContext context)
        {
            this._context = context;
        }
        public Enrollment AddItem(Enrollment item)
        {
            _context.Enrollments.ToList().Add(item);

            _context.save();
            return item;
        }

        public void DeleteItem(int id)
        {
            _context.Enrollments.ToList().Remove(GetById(id));
            _context.save();
        }

        public List<Enrollment> GetAll()
        {
            return _context.Enrollments.ToList();
        }
        // to ask the teacher
        public Enrollment GetById(int id)
        {
            return _context.Enrollments.ToList().FirstOrDefault(x => x.CourseId == id);
        }

        public void UpdateItem(int id, Enrollment item)
        {
            var Enrollment = GetById(id);
            Enrollment.StartDate = item.StartDate;

            _context.save();
        }
    }
}
