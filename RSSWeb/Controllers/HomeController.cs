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
        private readonly IFeedNameMgr _feedNameMgr;
        public HomeController(INewsItemMgr newsFeedMgr, IFeedNameMgr feedNameMgr)

        {
            _newsFeedMgr = newsFeedMgr;
            _feedNameMgr = feedNameMgr;
        }

        public ActionResult Index(int? page)
        {
            ViewBag.CurrentPage = page;
            var feedName = _feedNameMgr.GetAll();
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(feedName.ToPagedList(pageNumber, pageSize));
        }

        [HttpPost]
        public ActionResult Index(string RSSURL, int? page)
        {
            try
            {
                WebClient wclient = new WebClient();
                string RSSData = wclient.DownloadString(RSSURL);
                XDocument xml = XDocument.Parse(RSSData);

                FeedNameModel Feeds = new FeedNameModel
                {
                    Name = RSSURL,
                    Url = RSSURL
                };
                _feedNameMgr.Save(Feeds);

                var FeedNameData = _feedNameMgr.GetAll();
                var UrlId = (from feedId in FeedNameData orderby feedId.Id descending select feedId).First();
                var itemId = UrlId.Id;
                
                var RSSFeedData = (from xmlData in xml.Descendants("item")
                                   select new NewsItemModel
                                   {
                                       Url_Id = itemId,
                                       Title = ((string)xmlData.Element("title")),
                                       Link = ((string)xmlData.Element("link")),
                                       Description = ((string)xmlData.Element("description")),
                                       PubDate = ((string)xmlData.Element("pubDate"))
                                   });
                _newsFeedMgr.Save(RSSFeedData);
                ViewBag.CurrentPage = page;
                var feedName = _feedNameMgr.GetAll();
                int pageSize = 5;
                int pageNumber = (page ?? 1);
                return View(feedName.ToPagedList(pageNumber, pageSize));
            }
            catch
            {
                ViewBag.Message = "No Rss Feed Data Found";
                ViewBag.CurrentPage = page;
                var feedName = _feedNameMgr.GetAll();
                int pageSize = 5;
                int pageNumber = (page ?? 1);
                return View(feedName.ToPagedList(pageNumber, pageSize));
            }
            
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
            var items = _feedNameMgr.GetAllNewsItemsByFeedId(Id);
            int pageSize = 9;
            int pageNumber = (page ?? 1);
            return View("GetAllNewsItemsByFeedId", items.ToPagedList(pageNumber, pageSize));
        }


        [HttpGet]
        public ActionResult EditFeedName(int Id)
        {
            FeedNameModel model = new FeedNameModel();
            model = _feedNameMgr.GetById(Id);
            return View("EditFeedName", model);
        }

        [HttpPost]
        public ActionResult EditFeedName(FeedNameModel model)
        {
            _feedNameMgr.Update(model);
            return RedirectToAction("Index");
        }
    }
}