using RSSManager.Mapping;
using RSSRepository.Repositories.Interfaces;
using RSSModel.Model;
using RSSManager.Interfaces;
using RSSManager.ViewModel;
using NewsRepository.Repositories.Interfaces;
using System.Collections.Generic;

namespace RSSManager.Impl
{
    public class FeedNameMgr : IFeedNameMgr
    {
        private readonly IFeedNameRepo _feedNameRepo;
        private readonly INewsItemRepo _newsItemRepo;
        private readonly IDefaultMapping _modelMapper;
        public FeedNameMgr(IDefaultMapping modelMapper, IFeedNameRepo feedNameRepo, INewsItemRepo newsItemRepo)
        {
            _modelMapper = modelMapper;
            _feedNameRepo = feedNameRepo;
            _newsItemRepo = newsItemRepo;
        }

        

        public IList<FeedNameModel> GetAll()
        {
            IList<FeedNameModel> feedItems = new List<FeedNameModel>();
            var items = _feedNameRepo.FetchAll();
            foreach (var item in items)
            {
                feedItems.Add(_modelMapper.ModelMapper.Map<FeedNameModel>(item));
            }
            return feedItems;
        }

        public IList<NewsItem> GetAllNewsItemsByFeedId(int id)
        {
            var NewsItems = _newsItemRepo.GetAllNewsItemsByFeedId(id);
            return NewsItems;
        }

        public FeedNameModel GetById(int Id)
        {
            return this._modelMapper.ModelMapper.Map<FeedNameModel>(_feedNameRepo.Get(Id));
        }

        public void Save(FeedNameModel model)
        {
            _feedNameRepo.IsExist(x => x.Id == model.Id);
            var saveRssUrl = this._modelMapper.ModelMapper.Map<FeedName>(model);
            _feedNameRepo.Save(saveRssUrl);
        }

        public void Update(FeedNameModel feeds)
        {
            _feedNameRepo.Update(this._modelMapper.ModelMapper.Map<FeedName>(feeds));
        }

        public void Delete(int Id)
        {
            _feedNameRepo.Delete(Id);
        }
    }
}
