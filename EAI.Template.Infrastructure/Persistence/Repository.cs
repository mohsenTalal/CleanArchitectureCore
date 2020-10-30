using $ext_safeprojectname$.Application.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace $safeprojectname$.Persistence
{
    public class Repository<T> : IRepository<T>
        where T : class
    {
        private readonly DbContext context;

        private readonly DbSet<T> dbSet;

        public Repository(DbContext context)
        {
            this.context = context;

            this.dbSet = context.Set<T>();
        }

        public IQueryable<T> GetAll()
        {
            return GetAllIncluding();
        }

        public IQueryable<T> GetAllIncluding(params Expression<Func<T, object>>[] propertySelectors)
        {
            var query = dbSet.AsQueryable();

            if (propertySelectors != null)
            {
                foreach (var propertySelector in propertySelectors)
                {
                    query = query.Include(propertySelector);
                }
            }

            return query;
        }

        public async Task<List<T>> GetAllListAsync()
        {
            return await GetAll().ToListAsync();
        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>> predicate)
        {
            return GetAll().Where(predicate);
        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] propertySelectors)
        {
            return GetAllIncluding(propertySelectors).Where(predicate);
        }

        public async Task<List<T>> GetAllListAsync(Expression<Func<T, bool>> predicate)
        {
            return await GetAll().Where(predicate).ToListAsync();
        }

        public IQueryable<T> GetAll(int page, int pageCount)
        {
            var pageSize = (page - 1) * pageCount;

            return GetAll().Skip(pageSize).Take(pageCount);
        }

        public T FirstOrDefault(Expression<Func<T, bool>> predicate)
        {
            return GetAll().FirstOrDefault(predicate);
        }

        public T FirstOrDefault(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] propertySelectors)
        {
            return GetAllIncluding(propertySelectors).FirstOrDefault(predicate);
        }

        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            return await GetAll().FirstOrDefaultAsync(predicate);
        }

        public T Insert(T entity)
        {
            return dbSet.Add(entity).Entity;
        }

        public Task<T> InsertAsync(T entity)
        {
            return Task.FromResult(Insert(entity));
        }

        public T Update(T entity)
        {
            AttachIfNot(entity);
            return dbSet.Update(entity).Entity;
        }

        public Task<T> UpdateAsync(T entity)
        {
            entity = Update(entity);
            return Task.FromResult(entity);
        }

        public void Delete(T entity)
        {
            AttachIfNot(entity);
            dbSet.Remove(entity);
        }

        public async Task<int> CountAsync()
        {
            return await GetAll().CountAsync();
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> predicate)
        {
            return await GetAll().Where(predicate).CountAsync();
        }

        protected virtual void AttachIfNot(T entity)
        {
            var entry = context.ChangeTracker.Entries().FirstOrDefault(ent => ent.Entity == entity);
            if (entry != null)
            {
                return;
            }

            dbSet.Attach(entity);
        }
    }
}