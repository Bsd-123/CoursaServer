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
    public class ContentTypeRepository: IRepository<ContentType>
    {
        private readonly IContext _context;
        public ContentTypeRepository(IContext context)
        {
            this._context = context;
        }
        public async Task<ContentType> AddItem(ContentType item)
        {
            _context.ContentTypes.Add(item);
            await  _context.SaveAsync();
            return item;
        }

        public async Task DeleteItem(int id)
        {
            var entity =await  GetById(id);
            _context.ContentTypes.Remove(entity);
            await  _context.SaveAsync();
        }

        public async Task<List<ContentType>> GetAll()
        {
            return await  _context.ContentTypes.ToListAsync();
        }

        public async Task<ContentType> GetById(int id)
        {
            var result = await  _context.ContentTypes.FirstOrDefaultAsync(x => x.Id == id);
            return result;
        }

        public async Task UpdateItem(int id, ContentType item)
        {
            var ContentType = await  GetById(id);
            ContentType.Name = item.Name;
            ContentType.DisplayIcon = item.DisplayIcon;
            await  _context.SaveAsync();
        }
    }
}
