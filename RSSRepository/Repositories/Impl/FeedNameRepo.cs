using NewsRepository.Repositories.Interfaces;
using RSSModel.Model;
using RSSRepository.Repositories.Impl;
using System.Data.Entity;

namespace NewsRepository.Repositories.Impl
{
    public class FeedNameRepo : BaseRepo<FeedName>, IFeedNameRepo
    {
        public FeedNameRepo(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
