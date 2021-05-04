using LibraryIS.Domain.Common;
using System;
using System.Threading.Tasks;

namespace LibraryIS.Application.Repositories
{
    public interface IRepository<T> where T : AggregateRoot
    {
        public Task<T> GetByIdAsync(Guid id);
        public Task SaveAsync(T aggregateRoot);
    }
}
