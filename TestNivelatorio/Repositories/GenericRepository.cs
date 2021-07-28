using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TestNivelatorio.Data;
using TestNivelatorio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace TestNivelatorio.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly DBNivelatorioContext dbContext;
        protected readonly DbSet<TEntity> dbSet;
        private readonly ILogger<GenericRepository<TEntity>> _logger;

        public GenericRepository(DBNivelatorioContext dbContext, ILogger<GenericRepository<TEntity>> logger)
        {
            this.dbContext = dbContext;
            this.dbSet = dbContext?.Set<TEntity>();
            _logger = logger;
        }

        public async Task<int> Count()
        {

            return dbContext.Set<TEntity>().ToList().Count();
        }

        public IEnumerable<TEntity> FindAll(Func<TEntity, bool> exp)
        {
            return dbContext.Set<TEntity>().Where<TEntity>(exp);
        }


        public IEnumerable<TEntity> GetByParam(Func<TEntity, bool> exp)
        {
            return dbContext.Set<TEntity>().Where<TEntity>(exp);
        }


        public async Task<TEntity> Delete(int id)
        {

            var entity = dbContext.Set<TEntity>().Find(id);


            if (entity == null)
                throw new Exception("The entity is null");


            dbContext.Set<TEntity>().Remove(entity);
            dbContext.SaveChanges();
            return entity;
        }

        public async Task<List<TEntity>> GetAll()
        {
            return await dbContext.Set<TEntity>()
                .ToListAsync();
        }

        public async Task<TEntity> GetById(int id)
        {
            return await dbContext.Set<TEntity>().FindAsync(id);
        }


        public async Task<TEntity> Insert(TEntity entity)
        {
            try
            {
                await dbContext.Set<TEntity>().AddAsync(entity);
                await dbContext.SaveChangesAsync();
                return entity;
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString(), entity);
                throw new Exception("Mensaje.", e);
            }

        }

        public virtual async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = this.dbSet;
            if (includes != null)
                foreach (Expression<Func<TEntity, object>> include in includes)
                {
                    query = query.Include(include);
                }

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return query;
        }

        public virtual async Task<TEntity> Update(TEntity entity, object key, List<string>? ignoreFields)
        {
            try
            {
                if (entity == null)
                    return null;
                TEntity exist = await dbContext.Set<TEntity>().FindAsync(key);
                if (exist != null)
                {


                    dbContext.Entry(exist).CurrentValues.SetValues(entity);
                    dbContext.Entry(exist).Property("Id").IsModified = false;



                    if (ignoreFields != null)
                    {
                        foreach (var ignoreField in ignoreFields)
                        {
                            dbContext.Entry(exist).Property(ignoreField).IsModified = false;
                        }

                    }


                    await dbContext.SaveChangesAsync();
                }
                return exist;

            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString(), entity);
                throw new Exception("Message", e);

            }

        }

        
    }
}
