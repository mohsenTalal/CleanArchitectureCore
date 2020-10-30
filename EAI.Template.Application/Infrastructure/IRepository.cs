using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace $safeprojectname$.Infrastructure
{
    public interface IRepository<T> where T : class
    {
        Task<int> CountAsync();

        Task<int> CountAsync(Expression<Func<T, bool>> predicate);

        T FirstOrDefault(Expression<Func<T, bool>> predicate);

        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);

        T FirstOrDefault(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] propertySelectors);

        IQueryable<T> GetAll();

        IQueryable<T> GetAll(Expression<Func<T, bool>> predicate);

        IQueryable<T> GetAllIncluding(params Expression<Func<T, object>>[] propertySelectors);

        IQueryable<T> GetAll(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] propertySelectors);

        Task<List<T>> GetAllListAsync();

        Task<List<T>> GetAllListAsync(Expression<Func<T, bool>> predicate);

        IQueryable<T> GetAll(int page, int pageCount);

        T Insert(T entity);

        Task<T> InsertAsync(T entity);

        T Update(T entity);

        Task<T> UpdateAsync(T entity);

        void Delete(T entity);
    }
}