namespace RSSWeb.DependancyResolution
{
    using NewsRepository.Repositories.Impl;
    using NewsRepository.Repositories.Interfaces;
    using RSSManager;
    using RSSManager.Impl;
    using RSSManager.Interfaces;
    using RSSManager.Mapping;
    using RSSModel;
    using RSSRepository.Repositories.Impl;
    using RSSRepository.Repositories.Interfaces;
    using StructureMap.Configuration.DSL;
    using StructureMap.Graph;
    using System.Data.Entity;

    public class DefaultRegistry : Registry
    {
        #region Constructors and Destructors
        
        public DefaultRegistry()
        {
            Scan(
                scan => {
                    scan.TheCallingAssembly();
                    scan.AssembliesFromApplicationBaseDirectory(
                    filter => filter.FullName.StartsWith("CleanArchitecture"));
                    scan.WithDefaultConventions();
                    scan.With(new ControllerConvention());
                });
            
            For<IDefaultMapping>().Use<DefaultMapping>();
            For<DbContext>().Use<NewsContext>();
            For(typeof(IBaseRepo<>)).Use(typeof(BaseRepo<>));
            For<INewsItemRepo>().Use<NewsItemRepo>();
            For<INewsItemMgr>().Use<NewsItemMgr>();
            For<IFeedNameRepo>().Use<FeedNameRepo>();
            For<IFeedNameMgr>().Use<FeedNameMgr>();
        }
        #endregion
    }
}