using RSSManager.ViewModel;
using System.Collections.Generic;

namespace RSSManager
{
    public interface INewsItemMgr
    {
        IList<NewsItemModel> GetAllNewsItems();        
    }
}
