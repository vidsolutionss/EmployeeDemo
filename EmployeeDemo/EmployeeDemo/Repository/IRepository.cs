using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ServiceProviderAPI.Models.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<IList<TEntity>> GetAll();
        Task<TEntity> GetById(object id);
        Task<IQueryable<TEntity>> Get(Expression<Func<TEntity, bool>> filter = null);
        Task Add(TEntity entity);
        void Update(TEntity entityToUpdate);
        void Delete(TEntity entity);
        Task<int> Save();
    }
}
