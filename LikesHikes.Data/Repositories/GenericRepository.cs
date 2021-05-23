using LikesHikes.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LikesHikes.Data
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class, IEntity
    {
        protected DataContext context;
        internal DbSet<TEntity> dbSet;

        public GenericRepository(DataContext context)
        {
            this.context = context;
            dbSet = context.Set<TEntity>();
        }

        public Task<IQueryable<TEntity>> GetAll()
        {
            IQueryable<TEntity> data = dbSet;
            return Task.FromResult(data);
        }

        public async Task<TEntity> GetById(Guid id)
        {
            var data = await dbSet.FindAsync(id);
            return data;
        }

        public async Task Create(TEntity entity)
        {
            await dbSet.AddAsync(entity);
        }

        public Task Remove(Guid id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            dbSet.Remove(entityToDelete);
            return Task.CompletedTask;
        }

        public Task Update(TEntity entityToUpdate)
        {
            dbSet.Update(entityToUpdate);
            return Task.CompletedTask;
        }
    }
}
