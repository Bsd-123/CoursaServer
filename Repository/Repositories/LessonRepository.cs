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
    public class LessonRepository:IRepository<Lesson>
    {
        private readonly IContext _context;
        public LessonRepository(IContext context)
        {
            this._context = context;
        }
        public async Task<Lesson> AddItem(Lesson item)
        {
            _context.Lessons.Add(item);
            await  _context.SaveAsync();
            return item;
        }

        public async Task DeleteItem(int id)
        {
            _context.Lessons.Remove(await GetById(id));
            await _context.SaveAsync();
        }

        public async Task<List<Lesson>> GetAll()
        {
            return await _context.Lessons.Include(l => l.Course).Include(l => l.Type).ToListAsync();
        }

        public async Task<Lesson> GetById(int id)
        {
            return await _context.Lessons.Include(l => l.Course).Include(l => l.Type).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task UpdateItem(int id, Lesson item)
        {
            var Lesson = await GetById(id);
            Lesson.Name = item.Name;
            Lesson.Content = item.Content;
            Lesson.IsFree = item.IsFree;
            Lesson.Status = item.Status;
            Lesson.TypeId = item.TypeId;
            Lesson.MimeType = item.MimeType;
            Lesson.Idx = item.Idx;
            Lesson.CourseId = item.CourseId;
            await _context.SaveAsync();
        }
    }
}
