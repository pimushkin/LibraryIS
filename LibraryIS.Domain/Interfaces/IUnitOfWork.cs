using LibraryIS.Domain.Common;
using System;
using System.Threading.Tasks;

namespace LibraryIS.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : Entity;
        Task<int> Commit();
    }
}