using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace TestNivelatorio.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<List<TEntity>> GetAll();

        IEnumerable<TEntity> FindAll(Func<TEntity, bool> exp);

        Task<TEntity> GetById(int id);

        Task<TEntity> Insert(TEntity entity);

        Task<TEntity> Update(TEntity entity, object key, List<string>? ignoreFields);

        Task<TEntity> Delete(int id);

        Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, params Expression<Func<TEntity, object>>[] includes);
    }
}
