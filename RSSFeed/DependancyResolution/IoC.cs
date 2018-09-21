namespace RSSWeb.DependancyResolution
{
    using StructureMap;
    
    public class IoC
    {
        public static IContainer Initialize()
        {
            return new Container(c => c.AddRegistry<DefaultRegistry>());
        }
     
    }
}