using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IRepositoryBase<T>
    {
        Task<List<T>> GetAll();
        Task<T> AddItem(T item);
    }
    public interface IRepository<T>:IRepositoryBase<T>
    {
        Task<T> GetById(int id);
        Task UpdateItem(int id, T item);
        Task DeleteItem(int id);
    }
    public interface IRepositoryDouble<T> : IRepositoryBase<T>
    {
        Task<T> GetById(int id1, int id2);
        Task UpdateItem(int id1, int id2, T item);
        Task DeleteItem(int id1, int id2);
    }
}
