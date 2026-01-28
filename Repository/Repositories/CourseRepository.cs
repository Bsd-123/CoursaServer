using Repository.Entities;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    internal class CourseRepository:IRepository<Course>
    {
        private readonly IContext _context;
        public CourseRepository(IContext context)
        {
            this._context = context;
        }
        public Course AddItem(Course item)
        {
            _context.Courses.ToList().Add(item);

            _context.save();
            return item;
        }

        public void DeleteItem(int id)
        {
            _context.Courses.ToList().Remove(GetById(id));
            _context.save();
        }

        public List<Course> GetAll()
        {
            return _context.Courses.ToList();
        }

        public Course GetById(int id)
        {
            return _context.Courses.ToList().FirstOrDefault(x => x.Id == id);
        }

        public void UpdateItem(int id, Course item)
        {
            var Course = GetById(id);
            Course.Name = item.Name;

            _context.save();
        }
    }
}
