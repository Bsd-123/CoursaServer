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
        public ContentType AddItem(ContentType item)
        {
            _context.ContentTypes.Add(item);
            _context.save();
            return item;
        }

        public void DeleteItem(int id)
        {
            _context.ContentTypes.Remove(GetById(id));
            _context.save();
        }

        public List<ContentType> GetAll()
        {
            return _context.ContentTypes.ToList();
        }

        public ContentType GetById(int id)
        {
            return _context.ContentTypes.FirstOrDefault(x => x.Id == id);
        }

        public void UpdateItem(int id, ContentType item)
        {
            var ContentType = GetById(id);
            ContentType.Name = item.Name;

            _context.save();
        }
    }
}
