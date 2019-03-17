using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SecureWebAPI.DataAccess.Entities;

namespace SecureWebAPI.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private ApplicationDbContext _db = null;
        private DbSet<T> _objectSet;
        public Repository(ApplicationDbContext db)
        {
            _db = db;
            _objectSet = db.Set<T>();
        }

        public IEnumerable<T> GetOverview(Func<T, bool> predicate = null)
        {
            if (predicate != null)
                return _objectSet.Where(predicate);
            return _objectSet;
        }

        public T GetDetails(Func<T, bool> predicate)
        {
            return _objectSet.FirstOrDefault(predicate);
        }

        public T Add(T entity)
        {
            _objectSet.Add(entity);
            return entity;
        }

        public void Delete(T entity)
        {
            _objectSet.Remove(entity);
        }



        public void AddMany(IEnumerable<T> entities)
        {
            _objectSet.AddRange(entities);
        }

        public T Update(T entity)
        {
            _objectSet.Update(entity);
            return entity;
        }

        public IEnumerable<T> GetAll()
        {
            return _objectSet;
        }

        public void DeleteMany(IEnumerable<T> entities)
        {
            _objectSet.RemoveRange(entities);
        }
    }
}