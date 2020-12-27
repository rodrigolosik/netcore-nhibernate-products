using System.Collections.Generic;
using System.Threading.Tasks;

namespace netcore_nhibernate.Repository
{
    public interface IRepository<T>
    {
        Task Add(T item);
        Task Remove(int id);
        Task Update(T item);
        Task<T> FindById(int id);
        IEnumerable<T> FindAll();
    }
}