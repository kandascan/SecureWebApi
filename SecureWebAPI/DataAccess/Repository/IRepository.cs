using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SecureWebAPI.DataAccess.Repository
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll(Func<T, bool> predicate = null);
        IEnumerable<T> GetOverview(Func<T, bool> predicate = null);
        T GetDetails(Func<T, bool> predicate);
        T Add(T entity);
        T Update(T entity);
        void AddMany(IEnumerable<T> entities);
        void Delete(T entity);
    }
}