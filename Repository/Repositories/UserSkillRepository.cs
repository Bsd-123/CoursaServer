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
    public class UserSkillRepository : IRepositoryDouble<UserSkill>
    {
        private readonly IContext _context;
        public UserSkillRepository(IContext context)
        {
            this._context = context;
        }
        public async Task<UserSkill> AddItem(UserSkill item)
        {
            _context.UserSkills.Add(item);
            await _context.SaveAsync();
            return item;
        }

        public async Task DeleteItem(int id1, int id2)
        {
            _context.UserSkills.Remove(await GetById(id1,id2));
            await _context.SaveAsync();
        }

        public async Task<List<UserSkill>> GetAll()
        {
            return await _context.UserSkills.ToListAsync();
        }

        public async Task<UserSkill> GetById(int id1, int id2)
        {
            return await _context.UserSkills.FirstOrDefaultAsync(x => x.SkillId == id1 && x.UserId == id2);
        }

        public async Task UpdateItem(int id1,int id2, UserSkill item)
        {
            var UserSkill = await GetById(id1, id2);
            UserSkill.ProficiencyLevel = item.ProficiencyLevel;
            //לשנות???
            UserSkill.AcquiredDate = item.AcquiredDate;
            await _context.SaveAsync();
        }
    }
}
