using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IRepositoryBase<T>
    {
        List<T> GetAll();
        T AddItem(T item);
    }
    public interface IRepository<T>:IRepositoryBase<T>
    {
        T GetById(int id);
        T UpdateItem(int id, T item);
        void DeleteItem(int id);
    }
    public interface IRepositoryDouble<T> : IRepositoryBase<T>
    {
        T GetById(int id1, int id2);
        T UpdateItem(int id1, int id2, T item);
        void DeleteItem(int id1, int id2);
    }
}
