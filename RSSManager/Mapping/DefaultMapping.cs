using AutoMapper;
using RSSManager.ViewModel;
using RSSModel.Model;

namespace RSSManager.Mapping
{

    public class DefaultMapping :IDefaultMapping
    {
        private readonly IMapper _modelMapper;
        public DefaultMapping()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<FeedItem, FeedItemModel>();
                cfg.CreateMap<FeedItemModel, FeedItem>();

                cfg.CreateMap<Feed, FeedModel>();
                cfg.CreateMap<FeedModel, Feed>();
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
