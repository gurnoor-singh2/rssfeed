using System.Collections.Generic;
using RSSManager.ViewModel;
using RSSModel.Model;

namespace RSSManager
{
    public interface INewsItemMgr
    {
        IList<NewsItemModel> GetAll();
        void Save(IEnumerable<NewsItemModel> model);
        NewsItemModel GetById(int Id);
    }
}
