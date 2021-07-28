
using TestNivelatorio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace TestNivelatorio.Services
{
       public class GenericService<TEntity> : IGenericService<TEntity> where TEntity : class
       {
            private IGenericRepository<TEntity> genericRepository;

            public GenericService(IGenericRepository<TEntity> genericRepository)
            {
                this.genericRepository = genericRepository;
            }

            public async Task<TEntity> Delete(int id)
            {
                var result = await genericRepository.Delete(id);
                return result;
            }

            public async Task<List<TEntity>> GetAll()
            {
                return await genericRepository.GetAll();
            }

            public async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, params Expression<Func<TEntity, object>>[] includes)
            {
                return await genericRepository.GetAsync(filter, orderBy, includes);
            }


            public async Task<TEntity> GetById(int id)
            {
                return await genericRepository.GetById(id);
            }


            public async Task<TEntity> Insert(TEntity entity)
            {
                return await genericRepository.Insert(entity);
            }


            public async Task<TEntity> Update(TEntity entity, object key, List<string>? ignoreFields)
            {
                return await genericRepository.Update(entity, key, ignoreFields);
            }


       }
    
}
