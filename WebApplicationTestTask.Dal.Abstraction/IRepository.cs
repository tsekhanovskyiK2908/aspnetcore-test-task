using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WebApplicationTestTask.Dal.Abstraction
{
    public interface IRepository<TEntity>
    {
        Task<TEntity> AddAsync(TEntity entity);
        void Update(TEntity updatedEntity);
        Task<TEntity> DeleteAsync(TEntity entityToDelete);
        Task<TEntity> GetByIdAsync(int id);
        Task<List<TEntity>> GetAllAsync();
        Task SaveAsync();
        Task<int> GetCount();
    }
}
