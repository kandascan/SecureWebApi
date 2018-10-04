using SecureWebAPI.DataAccess.Repository;

namespace SecureWebAPI.DataAccess.UnitOfWork
{
    public interface IUnitOfWork
    {
        IRepository<T> Repository<T>() where T : class;
        void Save();
        void Dispose();
    }
}