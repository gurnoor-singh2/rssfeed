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
using System.Threading.Tasks;
using RSSWeb.DependancyResolution;
using RSSModel.Model;
using System.Transactions;
using System.Threading;

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
            
            System.Timers.Timer timerAutoUpdateFeed = new System.Timers.Timer();
            timerAutoUpdateFeed.Elapsed += async (sender, e) => await AutoUpdateFeed(timerAutoUpdateFeed);
            timerAutoUpdateFeed.Start();
            timerAutoUpdateFeed.Interval = 59000;

            return View(feedName.ToPagedList(pageNumber, pageSize));
        }

        [HttpPost]
        public ActionResult Index(string rssUrl, int? page)
        {
            try
            {
                var rssFeedData = _feedMgr.ParseFeedUrl(rssUrl, true);
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
            var newsfeed = _newsFeedMgr.GetAll().OrderByDescending(x => x.Id);
            int pageSize = 9;
            int pageNumber = (page ?? 1);
            return View(newsfeed.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult GetAllNewsItemsByFeedId(int Id, int? page)
        {
            ViewBag.CurrentPage = page;
            var items = _feedMgr.GetAllNewsItemsByFeedId(Id).OrderByDescending(x => x.Id);
            int pageSize = 9;
            int pageNumber = (page ?? 1);
            return View("GetAllNewsItemsByFeedId", items.ToPagedList(pageNumber, pageSize));
        }


        [HttpGet]
        public ActionResult EditFeed(int Id)
        {
            FeedModel model = new FeedModel();
            model = _feedMgr.GetById(Id);
            return View("EditFeed", model);
        }

        [HttpPost]
        public ActionResult EditFeed(FeedModel model)
        {
            _feedMgr.Update(model);
            return RedirectToAction("Index");
        }


        private async Task AutoUpdateFeed(System.Timers.Timer timernew)
        {
            await Task.Factory.StartNew(() =>
            {
                var currentDatetime = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time"));
                var time = currentDatetime.ToString("hh:mm tt");
                try
                {
                    var feedMgr = IoC.Initialize().GetInstance<IFeedMgr>();
                    using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new System.TimeSpan(0, 30, 0)))
                    {
                        using (NewsContext _context = new NewsContext())
                        {
                            Transaction rootTr = Transaction.Current;
                            var itemFeeds = _context.FeedNames;
                            var rssFeeds = _context.NewsItems;
                            foreach (var itemUrl in itemFeeds)
                            {
                                DependentTransaction dt = rootTr.DependentClone(DependentCloneOption.RollbackIfNotComplete);
                                var rssData = feedMgr.ParseFeedUrl(itemUrl.Url, false);
                                _newsFeedMgr.Save(rssData);
                                dt.Complete();
                            }
                            _context.Dispose();
                        }
                        scope.Complete();
                    }
                    timernew.Start();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });
        }
    }
}