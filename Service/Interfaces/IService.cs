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
        List<T> GetAll();
        Task<IActionResult> AddItem(T item);
    }
    public interface IService<T> : IServiceBase<T>
    {
        T GetById(int id);
        Task<IActionResult> UpdateItem(int id, T item);
        void DeleteItem(int id);
    }
    public interface IServiceDouble<T> : IServiceBase<T>
    {
        T GetById(int id1, int id2);
        Task<IActionResult> UpdateItem(int id1, int id2, T item);
        void DeleteItem(int id1, int id2);
    }
}
