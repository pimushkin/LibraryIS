using LibraryIS.Application.Repositories;
using LibraryIS.Domain.Common;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace LibraryIS.Persistence.Repositories.Common
{
    public abstract class BaseRepository<T> : IRepository<T> where T : AggregateRoot
    {
        protected readonly ApplicationDbContext Context;

        protected BaseRepository(ApplicationDbContext context)
        {
            Context = context;
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            var entity = await Context.Set<T>().FindAsync(id);
            return entity;
        }

        public async Task SaveAsync(T aggregateRoot)
        {
            Context.Entry(aggregateRoot).State =
                aggregateRoot.Id == Guid.Empty ? EntityState.Added : EntityState.Modified;
            await Context.SaveChangesAsync();
        }
    }
}
