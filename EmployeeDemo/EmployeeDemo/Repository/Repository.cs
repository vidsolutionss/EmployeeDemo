using EmployeeDemo.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ServiceProviderAPI.Models.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly EmployeeDBContext _context;
        private readonly DbSet<TEntity> table;

        #region constructors

        public Repository(EmployeeDBContext _context)
        {
            this._context = _context;
            table = _context.Set<TEntity>();
        }

        #endregion
        public async Task Add(TEntity entity)
        {
            await table.AddAsync(entity);
        }

        public void Delete(TEntity entity)
        {
             table.Remove(entity);
        }

        public async Task<TEntity> GetById(object id)
        {
            return await table.FindAsync(id);
        }

        public async Task<IList<TEntity>> GetAll()
        {
            return await table.ToListAsync();
        }

        public async Task<IQueryable<TEntity>> Get(Expression<Func<TEntity, bool>> filter = null)
        //Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null)
        {
            IQueryable<TEntity> query = table;

            if (filter != null)
                query = query.Where(filter);

            //if (orderBy != null)
            //    return orderBy(query).ToList();
            //      else

            return query;
        }

        public async Task<int> Save()
        {
                return await _context.SaveChangesAsync();
        }

        public void Update(TEntity entityToUpdate)
        {
            table.Attach(entityToUpdate);
            _context.Entry(entityToUpdate).State = EntityState.Modified;
        }


        #region Disposable pattern
        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
    
}
