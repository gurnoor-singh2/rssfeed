using RSSManager.ViewModel;
using RSSModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RSSManager.Interfaces
{
    public interface IFeedNameMgr
    {
        IList<FeedNameModel> GetAll();
        void Save(FeedNameModel model);
        void Update(FeedNameModel feeds);
        void Delete(int Id);
        FeedNameModel GetById(int Id);
        IList<NewsItem> GetAllNewsItemsByFeedId(int id);
    }
}
