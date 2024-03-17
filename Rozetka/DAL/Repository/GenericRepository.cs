using DAL.Data.Entities;
using DAL.Data;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity, int>
        where TEntity : class, IEntity<int>
    {
        protected readonly EFAppContext _dbContext;
        public GenericRepository(EFAppContext context)
        {
            _dbContext = context;
        }

        public async Task Create(TEntity entity)
        {
            await _dbContext.Set<TEntity>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var entity = await GetById(id);
            entity.IsDelete = true;
            _dbContext.Set<TEntity>().Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        public IQueryable<TEntity> GetAll()
        {
            return _dbContext.Set<TEntity>().AsNoTracking();
        }

        public async Task<TEntity> GetById(int id)
        {
            return await _dbContext.Set<TEntity>()
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task Update(int id, TEntity entity)
        {
            var local = _dbContext.Set<TEntity>()
                                 .Local
                                 .FirstOrDefault(entry => entry.Id == entity.Id);

            if (local != null)
            {
                _dbContext.Entry(local).State = EntityState.Detached;
            }

            _dbContext.Set<TEntity>().Update(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
