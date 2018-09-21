using System.Collections.Generic;
using NewsRepository.Repositories.Interfaces;
using RSSManager.Mapping;
using RSSManager.ViewModel;

namespace RSSManager.Impl
{

    public class NewsItemMgr : INewsItemMgr
    {
        private readonly INewsItemRepo _newsItemRepo;
        private readonly IDefaultMapping _modelMapper;
        public NewsItemMgr(IDefaultMapping modelMapper, INewsItemRepo newsItemRepo)
        {
            _modelMapper = modelMapper;
            _newsItemRepo = newsItemRepo;
        }

        public IList<NewsItemModel> GetAllNewsItems()
        {
            IList<NewsItemModel> newsItems = new List<NewsItemModel>();
            var items = _newsItemRepo.FetchAll();
            return _modelMapper.ModelMapper.Map<IList<NewsItemModel>>(items);            
        }        
    }
}
