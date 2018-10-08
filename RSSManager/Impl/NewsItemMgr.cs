﻿using System.Collections.Generic;
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
        private readonly IFeedRepo _feedNameRepo;
        private readonly IDefaultMapping _modelMapper;
        public NewsItemMgr(IDefaultMapping modelMapper, INewsItemRepo newsItemRepo, IFeedRepo feedNameRepo)
        {
            _modelMapper = modelMapper;
            _newsItemRepo = newsItemRepo;
            _feedNameRepo = feedNameRepo;
        }

        public IList<FeedItemModel> GetAll()
        {
            IList<FeedItemModel> newsItems = new List<FeedItemModel>();
            var items = _newsItemRepo.FetchAll();
            foreach (var item in items)
            {
                newsItems.Add(_modelMapper.ModelMapper.Map<FeedItemModel>(item));
            }
            return newsItems;
        }

        public void Save(FeedItemModel model)
        {
            var feed = _newsItemRepo.GetSingleResult(x => x.Url_Id == model.Url_Id);
            if (feed == null)
                _newsItemRepo.Save(this._modelMapper.ModelMapper.Map<FeedItem>(model));
            else
            {
                feed.FeedXML = model.FeedXML;
                _newsItemRepo.Update(feed);
            }
        }

        public FeedItemModel GetById(int Id)
        {
            return this._modelMapper.ModelMapper.Map<FeedItemModel>(_newsItemRepo.Get(Id));
        }
    }
}
