using Repository.Entities;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    internal class ProgressRepository:IRepository<Progress>
    {
        private readonly IContext _context;
        public ProgressRepository(IContext context)
        {
            this._context = context;
        }
        public Progress AddItem(Progress item)
        {
            _context.Progresses.ToList().Add(item);
            _context.save();
            return item;
        }

        public void DeleteItem(int id)
        {
            _context.Progresses.ToList().Remove(GetById(id));
            _context.save();
        }

        public List<Progress> GetAll()
        {
            return _context.Progresses.ToList();
        }
        //to ask the teacher
        public Progress GetById(int id)
        {
            return _context.Progresses.ToList().FirstOrDefault(x => x.UserId == id && x.LessonId == id);
        }

        public void UpdateItem(int id, Progress item)
        {
            var Progress = GetById(id);
            Progress.Seconds = item.Seconds;

            _context.save();
        }
    }
}
