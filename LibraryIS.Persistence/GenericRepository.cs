using LibraryIS.Domain.Common;
using LibraryIS.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LibraryIS.Persistence
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : Entity
    {
        private readonly ApplicationDbContext _context;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<TEntity> Query()
        {
            return _context.Set<TEntity>().AsQueryable();
        }

        public TEntity GetByUniqueId(object id)
        {
            return _context.Set<TEntity>().Find(id);
        }

        public async Task<TEntity> GetByUniqueIdAsync(object id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public TEntity? Find(Expression<Func<TEntity, bool>> match)
        {
            return _context.Set<TEntity>().SingleOrDefault(match);
        }

        public async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> match)
        {
            return await _context.Set<TEntity>().SingleOrDefaultAsync(match);
        }

        public ICollection<TEntity> FindAll(Expression<Func<TEntity, bool>> match)
        {
            return _context.Set<TEntity>().Where(match).ToList();
        }

        public async Task<ICollection<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> match)
        {
            return await _context.Set<TEntity>().Where(match).ToListAsync();
        }

        public IEnumerable<TEntity> Filter(Expression<Func<TEntity, bool>>? filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, string includeProperties = "", int? page = null,
            int? pageSize = null)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();
            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (includeProperties != null)
            {
                foreach (
                    var includeProperty in includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            if (page != null && pageSize != null)
            {
                query = query.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value);
            }

            return query.ToList();
        }

        public async Task<IEnumerable<TEntity>> FilterAsync(Expression<Func<TEntity, bool>>? filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, string includeProperties = "", int? page = null,
            int? pageSize = null)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();
            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (includeProperties != null)
            {
                foreach (
                    var includeProperty in includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            if (page != null && pageSize != null)
            {
                query = query.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value);
            }

            return await query.ToListAsync();
        }

        public IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate)
        {
            return _context.Set<TEntity>().Where(predicate);
        }

        public bool Exist(Expression<Func<TEntity, bool>> predicate)
        {
            var exist = _context.Set<TEntity>().Where(predicate);
            return exist.Any();
        }

        public ICollection<TEntity> GetAll()
        {
            return _context.Set<TEntity>().ToList();
        }

        public async Task<ICollection<TEntity>> GetAllAsync()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(Guid id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public void Add(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
        }

        public void Delete(Guid id)
        {
            var entityToDelete = _context.Set<TEntity>().Find(id);
            Delete(entityToDelete);
        }

        public void Delete(TEntity entityToDelete)
        {
            if (_context.Entry(entityToDelete).State == EntityState.Deleted)
            {
                _context.Set<TEntity>().Attach(entityToDelete);
            }

            _context.Set<TEntity>().Remove(entityToDelete);
        }

        public void Update(TEntity entityToUpdate)
        {
            _context.Set<TEntity>().Attach(entityToUpdate);
            _context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public int Count()
        {
            return _context.Set<TEntity>().Count();
        }

        public async Task<int> CountAsync()
        {
            return await _context.Set<TEntity>().CountAsync();
        }
    }
}
