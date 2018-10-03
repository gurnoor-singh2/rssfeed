using RSSManager;
using RSSManager.Interfaces;
using RSSManager.ViewModel;
using RSSModel;
using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Xml.Linq;
using PagedList;

namespace RSSWeb.Controllers
{

    public class HomeController : Controller
    {
        private readonly INewsItemMgr _newsFeedMgr;
        private readonly IFeedMgr _feedMgr;
        public HomeController(INewsItemMgr newsFeedMgr, IFeedMgr feedMgr)
        {
            _newsFeedMgr = newsFeedMgr;
            _feedMgr = feedMgr;
        }

        public ActionResult Index(int? page)
        {
            ViewBag.CurrentPage = page;
            var feedName = _feedMgr.GetAll();
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(feedName.ToPagedList(pageNumber, pageSize));
        }

        [HttpPost]
        public ActionResult Index(string rssUrl, int? page)
        {
            try
            {
                var rssFeedData = _feedMgr.ParseFeedUrl(rssUrl);
                _newsFeedMgr.Save(rssFeedData);                
            }
            catch
            {
                ViewBag.Message = "No Rss Feed Data Found";                
            }
            return RedirectToAction("Index");
        }


        public ActionResult GetAllFeeds(int? page)
        {
            ViewBag.CurrentPage = page;
            var newsfeed = _newsFeedMgr.GetAll();
            int pageSize = 9;
            int pageNumber = (page ?? 1);
            return View(newsfeed.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult GetAllNewsItemsByFeedId(int Id, int? page)
        {
            ViewBag.CurrentPage = page;
            var items = _feedMgr.GetAllNewsItemsByFeedId(Id);
            int pageSize = 9;
            int pageNumber = (page ?? 1);
            return View("GetAllNewsItemsByFeedId", items.ToPagedList(pageNumber, pageSize));
        }


        [HttpGet]
        public ActionResult EditFeedName(int Id)
        {
            FeedModel model = new FeedModel();
            model = _feedMgr.GetById(Id);
            return View("EditFeedName", model);
        }

        [HttpPost]
        public ActionResult EditFeedName(FeedModel model)
        {
            _feedMgr.Update(model);
            return RedirectToAction("Index");
        }
    }
}