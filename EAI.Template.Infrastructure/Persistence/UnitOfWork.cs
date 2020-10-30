using $ext_safeprojectname$.Application.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace $safeprojectname$.Persistence
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private DbContext dbContext;

        private Dictionary<Type, object> repositories;

        public UnitOfWork(DbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IRepository<TEntity> GetRepository<TEntity>()
            where TEntity : class
        {
            if (this.repositories == null)
            {
                this.repositories = new Dictionary<Type, object>();
            }

            var type = typeof(TEntity);
            if (!this.repositories.ContainsKey(type))
            {
                this.repositories[type] = new Repository<TEntity>(this.dbContext);
            }

            return (IRepository<TEntity>)this.repositories[type];
        }

        public int Commit()
        {
            return this.dbContext.SaveChanges();
        }

        public void Dispose()
        {
            this.Dispose(true);

            GC.SuppressFinalize(obj: this);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.dbContext != null)
                {
                    this.dbContext.Dispose();
                    this.dbContext = null;
                }
            }
        }
    }
}