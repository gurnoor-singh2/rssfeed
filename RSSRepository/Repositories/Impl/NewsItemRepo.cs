using NewsRepository.Repositories.Interfaces;
using RSSModel.Model;
using RSSRepository.Repositories.Impl;
using System.Data.Entity;

namespace NewsRepository.Repositories.Impl
{
    public class NewsItemRepo : BaseRepo<NewsItem>, INewsItemRepo
    {
        public NewsItemRepo(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
