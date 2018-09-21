
namespace RSSWeb.DependancyResolution
{
    using System.Web;

    using RSSWeb.App_Start;

    using StructureMap.Web.Pipeline;
    using System;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    public class StructureMapScopeModule : IHttpModule
    {
        #region Public Methods and Operators

        public void Dispose()
        {
        }

        public void Init(HttpApplication context)
        {

            context.BeginRequest += (sender, e) => StructuremapMvc.StructureMapDependencyScope.CreateNestedContainer();
            context.EndRequest += (sender, e) =>
            {
                HttpContextLifecycle.DisposeAndClearAll();
                StructuremapMvc.StructureMapDependencyScope.DisposeNestedContainer();
            };

            #endregion
         }
    }
}