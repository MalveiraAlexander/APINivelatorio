
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace TestNivelatorio.Interfaces
{
     public interface IGenericService<TEntity> where TEntity : class
     {
        public Task<List<TEntity>> GetAll();

        public Task<TEntity> GetById(int id);

        public Task<TEntity> Insert(TEntity entity);

        public Task<TEntity> Update(TEntity entity, object key, List<string>? ignoreFields );

        public Task<TEntity> Delete(int id);

        Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, params Expression<Func<TEntity, object>>[] includes);
    }
}
