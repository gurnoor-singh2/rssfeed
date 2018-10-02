using System.Collections.Generic;
using RSSManager.Mapping;
using RSSManager.ViewModel;
using RSSRepository.Repositories.Interfaces;
using RSSModel.Model;
using NewsRepository.Repositories.Interfaces;

namespace RSSManager.Impl
{

    public class NewsItemMgr : INewsItemMgr
    {
        private readonly INewsItemRepo _newsItemRepo;
        private readonly IFeedNameRepo _feedNameRepo;
        private readonly IDefaultMapping _modelMapper;
        public NewsItemMgr(IDefaultMapping modelMapper, INewsItemRepo newsItemRepo, IFeedNameRepo feedNameRepo)
        {
            _modelMapper = modelMapper;
            _newsItemRepo = newsItemRepo;
            _feedNameRepo = feedNameRepo;
        }

        public IList<NewsItemModel> GetAll()
        {
             IList<NewsItemModel> newsItems = new List<NewsItemModel>();
            var items = _newsItemRepo.FetchAll();
            foreach (var item in items)
            {
                newsItems.Add(_modelMapper.ModelMapper.Map<NewsItemModel>(item));
            }
            return newsItems;
        }

        public void Save(IEnumerable<NewsItemModel> model)
        {
            foreach(var item in model) 

            _newsItemRepo.Save(this._modelMapper.ModelMapper.Map<NewsItem>(item));
        }

        public NewsItemModel GetById(int Id)
        {
            return this._modelMapper.ModelMapper.Map<NewsItemModel>(_newsItemRepo.Get(Id));
        }
    }
}
