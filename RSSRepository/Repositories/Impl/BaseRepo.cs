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
       
        public IList<T> FetchAll()
        {
            return _dbContext.Set<T>().OrderByDescending(x => x.Id).ToList();
        }
    }
}
