using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SecureWebAPI.DataAccess.Repository
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetOverview(Func<T, bool> predicate = null);
        T GetDetails(Func<T, bool> predicate);
        void Add(T entity);
        void AddMany(IEnumerable<T> entities);
        void Delete(T entity);
    }
}