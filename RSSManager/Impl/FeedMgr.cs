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
        private readonly IFeedRepo _feedRepo;
        private readonly INewsItemRepo _newsItemRepo;
        private readonly IDefaultMapping _modelMapper;
        public FeedMgr(IDefaultMapping modelMapper, IFeedRepo feedRepo, INewsItemRepo newsItemRepo)
        {
            _modelMapper = modelMapper;
            _feedRepo = feedRepo;
            _newsItemRepo = newsItemRepo;
        }

        public IList<FeedModel> GetAll()
        {
            IList<FeedModel> feedItems = new List<FeedModel>();
            var items = _feedRepo.FetchAll();
            foreach (var item in items)
            {
                feedItems.Add(_modelMapper.ModelMapper.Map<FeedModel>(item));
            }
            return feedItems;
        }

        public IList<FeedItem> GetAllNewsItemsByFeedId(int id)
        {
            var NewsItems = _newsItemRepo.GetAllNewsItemsByFeedId(id);
            return NewsItems;
        }

        public FeedModel GetById(int Id)
        {
            return this._modelMapper.ModelMapper.Map<FeedModel>(_feedRepo.Get(Id));
        }

        public void Save(FeedModel model)
        {
            //_feedNameRepo.IsExist(x => x.Id == model..Id);
            var saveRssUrl = this._modelMapper.ModelMapper.Map<Feed>(model);
            _feedRepo.Save(saveRssUrl);
        }

        public void Update(FeedModel feeds)
        {
            _feedRepo.Update(this._modelMapper.ModelMapper.Map<Feed>(feeds));
        }

        public void Delete(int Id)
        {
            _feedRepo.Delete(Id);
        }

        public FeedItemModel ParseFeedUrl(string rssUrl, bool isValid)
        {
            WebClient wclient = new WebClient();
            string rssData = wclient.DownloadString(rssUrl);
            XDocument xml = XDocument.Parse(rssData);
            FeedItemModel feedItemModel = new FeedItemModel();

            var feed = _feedRepo.GetSingleResult(x => x.Url == rssUrl);
            if (feed == null)
            {
                var feedModel = new FeedModel
                {
                    Name = rssUrl,
                    Url = rssUrl
                };
                this.Save(feedModel);
                feedItemModel.Url_Id = feed.Id;
            }
            else
            {
                feedItemModel.Url_Id = feed.Id;
            }

            feedItemModel.FeedXML = xml.ToString();           
            return feedItemModel;
        }
    }
}
