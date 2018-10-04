using RSSModel.Model;
using RSSRepository.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace RSSRepository.Repositories.Impl
{
    public class BaseRepo<T> : IBaseRepo<T> where T : Base
    {
        private readonly DbContext _dbContext;
        public BaseRepo(DbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Delete(T entity)
        {
            _dbContext.Entry<T>(entity).State = System.Data.Entity.EntityState.Deleted;
            _dbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            this.Delete(this.Get(id));
        }

        public IList<T> FetchAll()
        {
            return _dbContext.Set<T>().ToList();
        }

        public IEnumerable<T> GetAll()
        {
            return _dbContext.Set<T>().ToList();
        }

        public DbSet<T> GetDbSet()
        {
            return _dbContext.Set<T>();
        }

        public IList<T> FetchAll(Expression<Func<T, bool>> query)
        {
            return _dbContext.Set<T>().Where(query).ToList();
        }

        public T Get(int id)
        {
            return _dbContext.Set<T>().FirstOrDefault(x => x.Id == id);
        }

        public T GetSingleResult(Expression<Func<T, bool>> query)
        {
            return _dbContext.Set<T>().FirstOrDefault(query);
        }

        public void Save(T entity)
        {
            _dbContext.Entry<T>(entity).State = EntityState.Added;
            _dbContext.SaveChanges();
        }

        public void Update(T entity)
        {
            _dbContext.Entry<T>(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public bool IsExist(Expression<Func<T, bool>> query)
        {
            return _dbContext.Set<T>().Count(query) > 0;
        }

        public T GetLastItem()
        {
            return _dbContext.Set<T>().OrderBy(x => x.Id).LastOrDefault();
        }

    }
}
