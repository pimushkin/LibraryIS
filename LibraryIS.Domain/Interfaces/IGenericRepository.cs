using LibraryIS.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LibraryIS.Domain.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : Entity
    {
        IQueryable<TEntity> Query();
        TEntity GetByUniqueId(object id);
        Task<TEntity> GetByUniqueIdAsync(object id);
        TEntity? Find(Expression<Func<TEntity, bool>> match);
        Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> match);
        ICollection<TEntity> FindAll(Expression<Func<TEntity, bool>> match);
        Task<ICollection<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> match);

        IEnumerable<TEntity> Filter(
            Expression<Func<TEntity, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            string includeProperties = "",
            int? page = null,
            int? pageSize = null);

        Task<IEnumerable<TEntity>> FilterAsync(
            Expression<Func<TEntity, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            string includeProperties = "",
            int? page = null,
            int? pageSize = null);

        IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate);
        bool Exist(Expression<Func<TEntity, bool>> predicate);
        ICollection<TEntity> GetAll();
        Task<ICollection<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(Guid id);
        void Add(TEntity entity);
        void Delete(Guid id);
        void Delete(TEntity entityToDelete);
        void Update(TEntity entityToUpdate);
        int Count();
        Task<int> CountAsync();
    }
}