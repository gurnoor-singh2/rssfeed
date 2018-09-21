using AutoMapper;
using RSSManager.ViewModel;
using RSSModel.Model;
using System.Collections.Generic;

namespace RSSManager.Mapping
{

    public class DefaultMapping :IDefaultMapping
    {
        private readonly IMapper _modelMapper;
        public DefaultMapping()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<IList<NewsItem>, List<NewsItemModel>>();
            });

            _modelMapper = config.CreateMapper();
        }

        public IMapper ModelMapper
        {
            get
            {
                return _modelMapper;
            }
        }
    }
}
