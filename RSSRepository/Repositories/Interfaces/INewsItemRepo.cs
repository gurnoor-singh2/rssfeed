using RSSModel.Model;
using System.Collections.Generic;

namespace RSSRepository.Repositories.Interfaces
{
    public interface INewsItemRepo : IBaseRepo<NewsItem>
    {
        IList<NewsItem> GetAllNewsItemsByFeedId(int Id);
    }
}

    
