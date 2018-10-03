using NewsRepository.Repositories.Interfaces;
using RSSModel.Model;
using RSSRepository.Repositories.Impl;
using System.Data.Entity;

namespace NewsRepository.Repositories.Impl
{
    public class FeedRepo : BaseRepo<Feed>, IFeedRepo
    {
        public FeedRepo(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
