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
        public async Task<Course> AddItem(Course item)
        {
            _context.Courses.Add(item);
            await  _context.SaveAsync();
            return item;
        }

        public async Task DeleteItem(int id)
        {
            _context.Courses.Remove( await  GetById(id));
            await  _context.SaveAsync();
        }

        public async Task<List<Course>> GetAll()
        {
            return await  _context.Courses.Include(c => c.Skill).Include(c => c.Owner).ToListAsync();
        }

        public async Task<Course> GetById(int id)
        {
            return await  _context.Courses.Include(c => c.Skill).Include(c => c.Owner).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task UpdateItem(int id, Course item)
        {
            var Course = await  GetById(id);
            Course.Name = item.Name;
            Course.Status = item.Status;
            Course.Description = item.Description;
            Course.Price = item.Price;
            Course.Image = item.Image;
            Course.OwnerId = item.OwnerId;
            Course.SkillId = item.SkillId;
            Course.Status = item.Status;
            Course.ValidityDays = item.ValidityDays;
            Course.Percentage = item.Percentage;
            await  _context.SaveAsync();
        }
    }
}
