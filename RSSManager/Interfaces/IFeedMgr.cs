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
        IList<FeedItem> GetAllNewsItemsByFeedId(int id);
        FeedItemModel ParseFeedUrl(string url, bool isValid);
    }
}
