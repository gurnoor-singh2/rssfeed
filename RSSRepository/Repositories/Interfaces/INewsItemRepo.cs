using RSSModel.Model;
using System.Collections.Generic;

namespace RSSRepository.Repositories.Interfaces
{
    public interface INewsItemRepo : IBaseRepo<FeedItem>
    {
        IList<FeedItem> GetAllNewsItemsByFeedId(int Id);
    }
}

    
