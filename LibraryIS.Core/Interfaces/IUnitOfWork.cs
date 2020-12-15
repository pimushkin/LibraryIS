using System;
using System.Threading.Tasks;
using LibraryIS.SharedKernel;

namespace LibraryIS.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity<Guid>;
        Task<int> Commit();
    }
}