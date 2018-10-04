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


        private async Task AutoUpdateFeed(System.Timers.Timer timernew)
        {
            await Task.Factory.StartNew(() =>
            {
                var currentDatetime = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time"));
                var time = currentDatetime.ToString("hh:mm tt");

                try
                {
                    var feedMgr = IoC.Initialize().GetInstance<IFeedMgr>();
                    // var feeds = feedMgr.GetAll();
                    using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                    {
                        using (NewsContext _context = new NewsContext())
                        {


                            var itemFeeds = _context.FeedNames;
                            var rssFeeds = _context.NewsItems;
                            foreach (var itemUrl in itemFeeds)
                            {
                                Transaction rootTr = Transaction.Current;
                                var rssData = feedMgr.ParseFeedUrl(itemUrl.Url, false);
                                foreach (var rssItem in rssData.ToList())
                                {
                                    DependentTransaction dt = rootTr.DependentClone(DependentCloneOption.RollbackIfNotComplete);

                                    try
                                    {
                                        var count = rssFeeds.Where(x => x.Title == rssItem.Title).Count();
                                        var newFeedItem = new FeedItem();
                                        if (rssItem != null)
                                        {
                                            if (count < 1)
                                            {
                                                newFeedItem.Description = rssItem.Description;
                                                newFeedItem.PubDate = rssItem.PubDate;
                                                newFeedItem.Link = rssItem.Link;
                                                newFeedItem.Title = rssItem.Title;
                                                newFeedItem.Url_Id = rssItem.Url_Id;
                                                _context.NewsItems.Add(newFeedItem);
                                            }
                                            _context.SaveChanges();
                                            Thread.Sleep(500);

                                            dt.Complete();
                                        }
                                       
                                    }
                                    catch (Exception ex)
                                    {
                                        throw ex;
                                    }
                                }
                                scope.Complete();
                            }
                            _context.Dispose();

                        }
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