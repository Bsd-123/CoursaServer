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
    public class ProgressRepository:IRepositoryDouble<Progress>
    {
        private readonly IContext _context;
        public ProgressRepository(IContext context)
        {
            this._context = context;
        }
        public async Task<Progress> AddItem(Progress item)
        {
            _context.Progresses.Add(item);
            await _context.SaveAsync();
            return item;
        }

        public async Task DeleteItem(int id1, int id2)
        {
            _context.Progresses.Remove(await GetById(id1, id2));
            await _context.SaveAsync();
        }

        public async Task<List<Progress>> GetAll()
        {
            return _context.Progresses.Include(l => l.Lesson).Include(l => l.User).ToList();
        }
        public async Task<Progress> GetById(int id1, int id2)
        {
            return await _context.Progresses.Include(l => l.Lesson).Include(l => l.User).FirstOrDefaultAsync(x => x.UserId == id1 && x.LessonId == id2);
        }

        public async Task UpdateItem(int id1, int id2, Progress item)
        {
            var Progress =await GetById(id1, id2);
            Progress.Seconds = item.Seconds;
            Progress.LastView = item.LastView;
            await _context.SaveAsync();
        }
    }
}
