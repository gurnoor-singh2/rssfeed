using System.Collections.Generic;
using RSSManager.ViewModel;
using RSSModel.Model;

namespace RSSManager
{
    public interface INewsItemMgr
    {
        IList<FeedItemModel> GetAll();
        void Save(FeedItemModel model);
        FeedItemModel GetById(int Id);
    }
}
