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
        public Skill AddItem(Skill item)
        {
            _context.Skills.Add(item);

            _context.save();
            return item;
        }

        public void DeleteItem(int id)
        {
            _context.Skills.Remove(GetById(id));
            _context.save();
        }

        public List<Skill> GetAll()
        {
            return _context.Skills.ToList();
        }

        public Skill GetById(int id)
        {
            return _context.Skills.FirstOrDefault(x => x.Id == id);
        }

        public void UpdateItem(int id, Skill item)
        {
            var Skill = GetById(id);
            Skill.Name = item.Name;

            _context.save();
        }
    }
}
