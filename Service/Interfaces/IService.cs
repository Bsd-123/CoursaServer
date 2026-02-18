using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IServiceBase<T>
    {
        List<T> GetAll();
        T AddItem(T item);
    }
    public interface IService<T> : IServiceBase<T>
    {
        T GetById(int id);
        void UpdateItem(int id, T item);
        void DeleteItem(int id);
    }
    public interface IServiceDouble<T> : IServiceBase<T>
    {
        T GetById(int id1, int id2);
        void UpdateItem(int id1, int id2, T item);
        void DeleteItem(int id1, int id2);
    }
}
