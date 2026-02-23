using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IServiceBase<T>
    {
        Task<List<T>> GetAll();
        Task<T> AddItem(T item);
    }
    public interface IService<T> : IServiceBase<T>
    {
        Task<T> GetById(int id);
        Task UpdateItem(int id, T item);
        Task DeleteItem(int id);
    }
    public interface IServiceDouble<T> : IServiceBase<T>
    {
        Task<T> GetById(int id1, int id2);
        Task UpdateItem(int id1, int id2, T item);
        Task DeleteItem(int id1, int id2);
    }
}
