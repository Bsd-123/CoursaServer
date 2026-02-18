using Repository.Entities;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class ProgressRepository:IRepositoryDouble<Progress>
    {
        private readonly IContext _context;
        public ProgressRepository(IContext context)
        {
            this._context = context;
        }
        public Progress AddItem(Progress item)
        {
            _context.Progresses.Add(item);
            _context.save();
            return item;
        }

        public void DeleteItem(int id1, int id2)
        {
            _context.Progresses.Remove(GetById(id1,id2));
            _context.save();
        }

        public List<Progress> GetAll()
        {
            return _context.Progresses.ToList();
        }
        public Progress GetById(int id1, int id2)
        {
            return _context.Progresses.FirstOrDefault(x => x.UserId == id1 && x.LessonId == id2);
        }

        public void UpdateItem(int id1, int id2, Progress item)
        {
            var Progress = GetById(id1, id2);
            Progress.Seconds = item.Seconds;
            Progress.LastView = item.LastView;
            _context.save();
        }
    }
}
