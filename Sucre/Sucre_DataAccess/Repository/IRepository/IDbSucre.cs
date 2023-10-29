using Sucre_DataAccess.Entities;
using Sucre_DataAccess.Entities.TDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Sucre_DataAccess.Repository.IRepository
{
    public interface IDbSucre<T> where T : class, IBaseEntity
    {
        void Add(T entity);
        Task AddAsync(T entity);
        Task AddManyAsync(T entities);

        Task<int> Count();

        T Find(int id);
        Task<T> FindAsync(int id);

        T FirstOrDefault(
            Expression<Func<T, bool>> filter = null,
            string includeProperties = null,
            bool isTracking = true);
        Task<T> FirstOrDefaultAsync(
            Expression<Func<T, bool>> filter = null,
            string includeProperties = null,
            bool isTracking = true);

        IEnumerable<T> GetAll(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = null,
            bool isTracking = true);
        Task<IEnumerable<T>> GetAllAsync(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = null,
            bool isTracking = true);

        IQueryable<T> GetAsQueryable();

        Task<T?> GetById(int id, params Expression<Func<T, object>>[] includes);
        Task<T?> GetByIdAsNoTracking(int id, params Expression<Func<T, object>>[] includes);

        Task Patch(int id, List<PatchTdo> patchTdos);

        void Remove(T entity);
        Task RemoveAsync(T entity);
        Task RemoveByIdAsync(int id);
        void RemoveRange(IEnumerable<T> entities);
        Task RemoveRangeAsync(IEnumerable<T> entities);

        //void Save();
        //Task SaveAsync();
    }
}
