using System.Collections.Generic;
using System.Web.Mvc;
using RSSWeb.RSSHelper;
using RSSManager;
using RSSManager.ViewModel;

namespace RSSWeb.Controllers
{

    public class HomeController : Controller
    {
        private readonly INewsItemMgr _newsFeedMgr;
        public HomeController(INewsItemMgr newsFeedMgr)
        {
            _newsFeedMgr = newsFeedMgr;
        }

        public ActionResult Index()
        {
            IList<NewsItemModel> news = _newsFeedMgr.GetAllNewsItems();
            return new RssResult(news, "Custom News Feed By Ameotech", "The latest news on development structure.");
        }
    }
}