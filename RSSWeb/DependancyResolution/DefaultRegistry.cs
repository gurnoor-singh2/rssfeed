namespace RSSWeb.DependancyResolution
{
    using StructureMap.Configuration.DSL;
    using StructureMap.Graph;
    using System.Data.Entity;
    using RSSManager.Mapping;
    using RSSManager.Impl;
    using RSSManager;
 
    using RSSModel;
    using RSSRepository.Repositories.Interfaces;
    using NewsRepository.Repositories.Impl;
    using NewsRepository.Repositories.Interfaces;
    using RSSRepository.Repositories.Impl;

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
        }
        #endregion
    }
}