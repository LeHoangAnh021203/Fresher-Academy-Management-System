using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        ICollection<T> GetAll();
        ICollection<T> GetList(Expression<Func<T, bool>> expression);
        T Get(Expression<Func<T, bool>> expression);
        T Add(T entity);
        void AddRange(ICollection<T> entities);
        void Update(T entity);
        void Delete(Guid id);
        void ClearTrackers();
        int SaveChanges();
        Task SaveChangesAsync();
        void Dispose();
    }
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly FAMSDBContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(FAMSDBContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public virtual ICollection<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public ICollection<T> GetList(Expression<Func<T, bool>> expression)
        {
            return _dbSet.Where(expression).ToList();
        }

        public virtual T Get(Expression<Func<T, bool>> expression)
        {
            return _dbSet.FirstOrDefault(expression);
        }

        public virtual T Add(T entity)
        {
            _dbSet.Add(entity);
            return entity;
        }

        public void AddRange(ICollection<T> entities)
        {
            _dbSet.AddRange(entities);
        }

        public virtual void Update(T entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }


        public virtual void Delete(Guid id)
        {
            var entity = _dbSet.Find(id);
            _dbSet.Remove(entity);
        }

        public virtual void Delete(string id)
        {
            var entity = _dbSet.Find(id);
            _dbSet.Remove(entity);
        }

        public virtual void Remove(T enity)
        {

            _dbSet.Remove(enity);
        }

        public void ClearTrackers()
        {
            _context.ChangeTracker.Clear();
        }
        public virtual int SaveChanges()
        {
            try
            {
                return _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                // Handle or log the exception
                throw new Exception(ex.Message);
            }
        }   
        public virtual async Task SaveChangesAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                // Handle or log the exception
                throw new Exception(ex.Message);
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }

}

