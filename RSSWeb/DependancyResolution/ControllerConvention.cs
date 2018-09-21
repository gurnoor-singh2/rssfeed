
namespace RSSWeb.DependancyResolution
{
    using System;
    using StructureMap;
    using StructureMap.Graph;
    using StructureMap.Pipeline;
    using StructureMap.TypeRules;
    using System.Web.Mvc;
    using StructureMap.Configuration.DSL;
    

    public class ControllerConvention : IRegistrationConvention
    {
        
        #region Public Methods and Operators

        public void Process(Type type, Registry registry)
        {
            if (type.CanBeCastTo<Controller>() && !type.IsAbstract)
            {
                registry.For(type).LifecycleIs(new UniquePerRequestLifecycle());
            }
        }

        #endregion
    }
}
