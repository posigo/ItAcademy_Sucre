using Microsoft.EntityFrameworkCore;
using Sucre_DataAccess.Data;
using Sucre_DataAccess.Entities;
using Sucre_DataAccess.Entities.TDO;
using Sucre_DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Sucre_DataAccess.Repository
{
    public class DbSucre<T> : IDbSucre<T> where T : class, IBaseEntity
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbSet;

        public DbSucre(ApplicationDbContext db)
        {
            _db = db;
            this.dbSet = _db.Set<T>();
        }
        public void Add(T entity)
        {
            dbSet.Add(entity);
        }
        public async Task AddAsync(T entity)
        {
            await dbSet.AddAsync(entity);
        }

        public async Task<int> Count()
        { 
            return await dbSet.CountAsync();
        }


        public virtual T Find(int id)
        {
            return dbSet.Find(id);
        }
        public virtual async Task<T> FindAsync(int id)
        {
            return await dbSet.FindAsync(id);
        }

        public T FirstOrDefault(Expression<Func<T, bool>> filter = null, string includeProperties = null, bool isTracking = true)
        {
            IQueryable<T> query = dbSet;
            if (filter != null)
                query = query.Where(filter);

            if (includeProperties != null)
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    query = query.Include(includeProp);

            if (!isTracking)
                query = query.AsNoTracking();

            return query.FirstOrDefault();
        }
        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> filter = null, string includeProperties = null, bool isTracking = true)
        {
            IQueryable<T> query = dbSet;
            if (filter != null)
                query = query.Where(filter);

            if (includeProperties != null)
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    query = query.Include(includeProp);

            if (!isTracking)
                query = query.AsNoTracking();

            return await query.FirstOrDefaultAsync();
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = null, bool isTracking = true)
        {
            IQueryable<T> query = dbSet;
            if (filter != null)
                query = query.Where(filter);

            if (includeProperties != null)
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    query = query.Include(includeProp);

            if (orderBy != null)
                query = orderBy(query);

            if (!isTracking)
                query = query.AsNoTracking();

            return query.ToList();

        }
        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter = null, 
                                                Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, 
                                                string includeProperties = null, 
                                                bool isTracking = true)
        {
            IQueryable<T> query = dbSet;
            if (filter != null)
                query = query.Where(filter);

            if (includeProperties != null)
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    query = query.Include(includeProp);

            if (orderBy != null)
                query = orderBy(query);

            if (!isTracking)
                query = query.AsNoTracking();

            return await query.ToListAsync();
        }

        public virtual IQueryable<T> GetAsQueryable()
        {
            return dbSet.AsQueryable();
        }

        public async Task<T?> GetById(int id, params Expression<Func<T, object>>[] includes)
        {
            var resultQuery = dbSet.AsQueryable();
            if (includes.Any())
            {
                resultQuery = includes.Aggregate(resultQuery, (current, include) => current.Include(include));
            }

            return await resultQuery.FirstOrDefaultAsync(entity => entity.Id.Equals(id));
        }

        public async Task<T?> GetByIdAsNoTracking(int id, params Expression<Func<T, object>>[] includes)
        {
            var resultQuery = dbSet.AsNoTracking();
            if (includes.Any())
            {
                //resultQuery = includes.Aggregate(resultQuery, (current, include) => current.Include<T>(include.ToString()));
                resultQuery = includes.Aggregate(resultQuery, (current, include) => current.Include(include));
            }

            return await resultQuery.FirstOrDefaultAsync(entity => entity.Id.Equals(id));
        }

        public async Task Patch(int id, List<PatchTdo> patchTdos)
        {
            var entity = await GetById(id);
            if (entity != null) 
            { 
                var nameValuePairProperties = patchTdos.ToDictionary(key => key.PropertyName,
                                                                    value => value.PropertyValue);               
                var dbEntityEntry = _db.Entry(entity);
                dbEntityEntry.CurrentValues.SetValues(nameValuePairProperties);
                dbEntityEntry.State = EntityState.Modified;
            }
            else
            {
                throw new ArgumentException("Incorrect Id for update", nameof(id));
            }
        }

        public virtual void Remove(T entity)
        {
            dbSet.Remove(entity);
        }        
        public virtual async Task RemoveAsync(T entity)
        {
            await Task.Run(() => Remove(entity));
        }
        public virtual async Task RemoveByIdAsync(int id)
        { 
            var entityRemove = await GetById(id);
            if (entityRemove != null)
            {
                dbSet.Remove(entityRemove);
            }
            else
            {
                throw new ArgumentException("Incorrect Id for delete", nameof(id));
            }
        }

        public virtual void RemoveRange(IEnumerable<T> entities)
        {
            if (entities.Any())
            {
                var entitiesToRemove = entities.Where(entity => dbSet.Any(dbe => dbe.Id.Equals(entity.Id))).ToList();
                dbSet.RemoveRange(entitiesToRemove);
            }
            
        }
        public virtual async Task RemoveRangeAsync(IEnumerable<T> entities)
        {
            await Task.Run(() => RemoveRange(entities));
        }

        //public void Save()
        //{
        //    _db.SaveChanges();
        //}
        //public async Task SaveAsync()
        //{
        //    await _db.SaveChangesAsync();
        //}

    }
}
