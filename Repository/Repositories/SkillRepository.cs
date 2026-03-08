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
    public class SkillRepository:IRepository<Skill>
    {
        private readonly IContext _context;
        public SkillRepository(IContext context)
        {
            this._context = context;
        }
        public async Task<Skill> AddItem(Skill item)
        {
            _context.Skills.Add(item);
            await _context.SaveAsync();
            return item;
        }

        public async Task DeleteItem(int id)
        {
            _context.Skills.Remove(await GetById(id));
            await _context.SaveAsync();
        }

        public async Task<List<Skill>> GetAll()
        {
            return await _context.Skills.ToListAsync();
        }

        public async Task<Skill> GetById(int id)
        {
            return await _context.Skills.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task UpdateItem(int id, Skill item)
        {
            var Skill = await GetById(id);
            Skill.Name = item.Name;
            Skill.Image= item.Image;
            Skill.Status= item.Status;
            await  _context.SaveAsync();
        }
    }
}
