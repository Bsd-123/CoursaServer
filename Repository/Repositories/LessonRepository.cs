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
        public Lesson AddItem(Lesson item)
        {
            _context.Lessons.Add(item);
            _context.save();
            return item;
        }

        public void DeleteItem(int id)
        {
            _context.Lessons.Remove(GetById(id));
            _context.save();
        }

        public List<Lesson> GetAll()
        {
            return _context.Lessons.Include(l => l.Course).Include(l => l.Type).ToList();
        }

        public Lesson GetById(int id)
        {
            return _context.Lessons.Include(l => l.Course).Include(l => l.Type).FirstOrDefault(x => x.Id == id);
        }

        public Lesson UpdateItem(int id, Lesson item)
        {
            var Lesson = GetById(id);
            Lesson.Name = item.Name;
            Lesson.Content = item.Content;
            Lesson.IsFree = item.IsFree;
            Lesson.Status = item.Status;
            Lesson.TypeId = item.TypeId;
            Lesson.MimeType = item.MimeType;
            Lesson.Idx = item.Idx;
            Lesson.CourseId = item.CourseId;
            _context.save();
            return GetById(id);
        }
    }
}
