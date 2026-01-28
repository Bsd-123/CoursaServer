using Repository.Entities;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    internal class LessonRepository:IRepository<Lesson>
    {
        private readonly IContext _context;
        public LessonRepository(IContext context)
        {
            this._context = context;
        }
        public Lesson AddItem(Lesson item)
        {
            _context.Lessons.ToList().Add(item);
            _context.save();
            return item;
        }

        public void DeleteItem(int id)
        {
            _context.Lessons.ToList().Remove(GetById(id));
            _context.save();
        }

        public List<Lesson> GetAll()
        {
            return _context.Lessons.ToList();
        }

        public Lesson GetById(int id)
        {
            return _context.Lessons.ToList().FirstOrDefault(x => x.Id == id);
        }

        public void UpdateItem(int id, Lesson item)
        {
            var Lesson = GetById(id);
            Lesson.Name = item.Name;

            _context.save();
        }
    }
}
