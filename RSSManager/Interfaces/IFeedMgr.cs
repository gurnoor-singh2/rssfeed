using RSSManager.ViewModel;
using RSSModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RSSManager.Interfaces
{
    public interface IFeedMgr
    {
        IList<FeedModel> GetAll();
        void Save(FeedModel model);
        void Update(FeedModel feeds);
        void Delete(int Id);
        FeedModel GetById(int Id);
        IList<NewsItem> GetAllNewsItemsByFeedId(int id);
        IEnumerable<NewsItemModel> ParseFeedUrl(string url);
    }
}
