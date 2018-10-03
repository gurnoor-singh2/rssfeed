using RSSManager.Mapping;
using RSSRepository.Repositories.Interfaces;
using RSSModel.Model;
using RSSManager.Interfaces;
using RSSManager.ViewModel;
using NewsRepository.Repositories.Interfaces;
using System.Collections.Generic;
using System.Net;
using System.Xml.Linq;
using System.Linq;

namespace RSSManager.Impl
{
    public class FeedMgr : IFeedMgr
    {
        private readonly IFeedRepo _feedNameRepo;
        private readonly INewsItemRepo _newsItemRepo;
        private readonly IDefaultMapping _modelMapper;
        public FeedMgr(IDefaultMapping modelMapper, IFeedRepo feedNameRepo, INewsItemRepo newsItemRepo)
        {
            _modelMapper = modelMapper;
            _feedNameRepo = feedNameRepo;
            _newsItemRepo = newsItemRepo;
        }        

        public IList<FeedModel> GetAll()
        {
            IList<FeedModel> feedItems = new List<FeedModel>();
            var items = _feedNameRepo.FetchAll();
            foreach (var item in items)
            {
                feedItems.Add(_modelMapper.ModelMapper.Map<FeedModel>(item));
            }
            return feedItems;
        }

        public IList<NewsItem> GetAllNewsItemsByFeedId(int id)
        {
            var NewsItems = _newsItemRepo.GetAllNewsItemsByFeedId(id);
            return NewsItems;
        }

        public FeedModel GetById(int Id)
        {
            return this._modelMapper.ModelMapper.Map<FeedModel>(_feedNameRepo.Get(Id));
        }

        public void Save(FeedModel model)
        {
            _feedNameRepo.IsExist(x => x.Id == model.Id);
            var saveRssUrl = this._modelMapper.ModelMapper.Map<Feed>(model);
            _feedNameRepo.Save(saveRssUrl);
        }

        public void Update(FeedModel feeds)
        {
            _feedNameRepo.Update(this._modelMapper.ModelMapper.Map<Feed>(feeds));
        }

        public void Delete(int Id)
        {
            _feedNameRepo.Delete(Id);
        }

        public IEnumerable<NewsItemModel> ParseFeedUrl(string rssUrl)
        {
            WebClient wclient = new WebClient();
            string rssData = wclient.DownloadString(rssUrl);
            XDocument xml = XDocument.Parse(rssData);

            FeedModel feed = new FeedModel
            {
                Name = rssUrl,
                Url = rssUrl
            };
            this.Save(feed);            

            var rssFeedData = (from xmlData in xml.Descendants("item")
                               select new NewsItemModel
                               {
                                   Url_Id = feed.Id,
                                   Title = ((string)xmlData.Element("title")),
                                   Link = ((string)xmlData.Element("link")),
                                   Description = ((string)xmlData.Element("description")),
                                   PubDate = ((string)xmlData.Element("pubDate"))
                               });

            return rssFeedData;
        }
    }
}
