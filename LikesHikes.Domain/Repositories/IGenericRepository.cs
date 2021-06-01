using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LikesHikes.Domain
{
    public interface IGenericRepository<TEntity>
    {
        Task<IQueryable<TEntity>> GetAll();
        Task<TEntity> GetById(Guid id);
        Task Create(TEntity entity);
        Task Update(TEntity entityToUpdate);
        Task Remove(Guid id);
        Task DeleteRange(IQueryable<TEntity> entities);
    }
}
