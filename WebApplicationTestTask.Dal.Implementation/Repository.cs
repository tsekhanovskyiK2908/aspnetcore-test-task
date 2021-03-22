using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebApplicationTestTask.Dal.Abstraction;

namespace WebApplicationTestTask.Dal.Implementation
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        public OrderingSystemDbContext Context { get; }

        public Repository(OrderingSystemDbContext shoppingDbContext)
        {
            Context = shoppingDbContext;
        }
        public DbSet<TEntity> DbSet => Context.Set<TEntity>();

        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            EntityEntry<TEntity> item = await DbSet.AddAsync(entity);

            return item.Entity;
        }

        public virtual async Task<TEntity> DeleteAsync(TEntity entityToDelete)
        {
            TEntity deleteResult = DbSet.Remove(entityToDelete).Entity;

            return await Task.FromResult(deleteResult);
        }

        public virtual async Task<List<TEntity>> GetAllAsync()
        {
            return await DbSet.ToListAsync();
        }

        public virtual async Task<TEntity> GetByIdAsync(int id)
        {
            return await DbSet.FindAsync(id);
        }

        public virtual async Task SaveAsync()
        {
            await Context.SaveChangesAsync();
        }

        public virtual void Update(TEntity updatedEntity)
        {
            DbSet.Update(updatedEntity);
        }

        public async Task<int> GetCount()
        {
            return await DbSet.CountAsync();
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await Context.Database.BeginTransactionAsync();
        }
    }
}
