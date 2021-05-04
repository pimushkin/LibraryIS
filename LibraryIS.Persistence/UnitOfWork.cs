using LibraryIS.Domain.Common;
using LibraryIS.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryIS.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly Dictionary<Type, object> _repositories = new Dictionary<Type, object>();
        private bool _disposed;

        public UnitOfWork(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : Entity
        {
            if (_repositories.Keys.Contains(typeof(TEntity)))
            {
                {
                    return (IGenericRepository<TEntity>)_repositories[typeof(TEntity)];
                }
            }

            IGenericRepository<TEntity> repository = new GenericRepository<TEntity>(_applicationDbContext);
            _repositories.Add(typeof(TEntity), repository);
            return repository;
        }

        public async Task<int> Commit()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(GetType().FullName);
            }
            return await _applicationDbContext.SaveChangesAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _applicationDbContext.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
