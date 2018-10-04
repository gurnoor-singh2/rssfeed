using RSSModel.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq.Expressions;

namespace RSSRepository.Repositories.Interfaces
{
    public interface IBaseRepo<T> where T : Base
    {
        void Save(T entity);

        void Update(T entity);

        IList<T> FetchAll();

        IEnumerable<T> GetAll();

        IList<T> FetchAll(Expression<Func<T, bool>> query);

        T Get(int id);

        void Delete(int id);

        void Delete(T entity);

        bool IsExist(Expression<Func<T, bool>> query);

        T GetSingleResult(Expression<Func<T, bool>> query);

        DbSet<T> GetDbSet();

        T GetLastItem();

    }
}
