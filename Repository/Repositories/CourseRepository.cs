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
    public class CourseRepository:IRepository<Course>
    {
        private readonly IContext _context;
        public CourseRepository(IContext context)
        {
            this._context = context;
        }
        public Course AddItem(Course item)
        {
            _context.Courses.Add(item);

            _context.save();
            return item;
        }

        public void DeleteItem(int id)
        {
            _context.Courses.Remove(GetById(id));
            _context.save();
        }

        public List<Course> GetAll()
        {
            return _context.Courses.Include(c => c.Skill).Include(c => c.Owner).ToList();
        }

        public Course GetById(int id)
        {
            return _context.Courses.Include(c => c.Skill).Include(c => c.Owner).FirstOrDefault(x => x.Id == id);
        }

        public Course UpdateItem(int id, Course item)
        {
            var Course = GetById(id);
            Course.Name = item.Name;
            Course.Status = item.Status;
            Course.Description = item.Description;
            Course.Price = item.Price;
            Course.Image = item.Image;
            Course.OwnerId = item.OwnerId;
            Course.SkillId = item.SkillId;
            _context.save();
            return GetById(id);
        }
    }
}
