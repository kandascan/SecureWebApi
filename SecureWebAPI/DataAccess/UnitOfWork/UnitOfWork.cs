using Microsoft.EntityFrameworkCore;
using SecureWebAPI.DataAccess.Entities;
using SecureWebAPI.DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecureWebAPI.DataAccess.UnitOfWork
{
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        private ApplicationDbContext db = null;

        public UnitOfWork()
        {
            db = new ApplicationDbContext(new DbContextOptions<ApplicationDbContext>());
        }

        public Dictionary<Type, object> repositories = new Dictionary<Type, object>();

        public IRepository<T> Repository<T>() where T : class
        {
            if (repositories.Keys.Contains(typeof(T)) == true)
                return repositories[typeof(T)] as IRepository<T>;
            IRepository<T> repo = new Repository<T>(db);
            repositories.Add(typeof(T), repo);
            return repo;
        }

        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                    db.Dispose();
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
