using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Models;
using DataLayer.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task<List<T>> GetAsync();
        Task<T> GetByIdAsync(params object[] id);
        Task<List<T>> GetAsync(Expression<Func<T, bool>> expression);
        void Add(T entity);
        void Update(T entity);
        Task AddRangeAsync(IEnumerable<T> entities);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
        void Delete(T entity);
        void UndoDelete(T entity);
    }
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected readonly Context _db;
        protected DbSet<T> _dbSet;

        protected BaseRepository(Context db)
        {
            _db = db;
            _dbSet = db.Set<T>();
        }
        public async Task<List<T>> GetAsync()
        {
            return await GetAllFiltered().ToListAsync();
        }

        protected IQueryable<T> GetAllFiltered(bool includeDeleted = false)
        {
            return _dbSet.Where(x => x.IsDeleted == includeDeleted);
        }

        public async Task<T> GetByIdAsync(params object[] id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<List<T>> GetAsync(Expression<Func<T, bool>> expression)
        {
            return await _dbSet.Where(expression).ToListAsync();
        }

        public void  Add(T entity)
        {
            entity.CreatedAt = DateTime.UtcNow;
            _dbSet.Add(entity);
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            foreach (T entity in entities)
            {
                entity.CreatedAt = DateTime.UtcNow;
            }
            await _dbSet.AddRangeAsync(entities);
        }

        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public void Delete(T entity)
        {
            entity.IsDeleted = true;
            entity.DeletedAt = DateTime.UtcNow;
        }


        public void UndoDelete(T entity)
        {
            entity.IsDeleted = false;
            entity.LastModifiedAt = DateTime.UtcNow;
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            if (entities.Any())
                _dbSet.RemoveRange(entities);
        }
    }
}
