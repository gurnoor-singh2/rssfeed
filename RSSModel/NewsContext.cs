using RSSModel.Model;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace RSSModel
{
    public class NewsContext : DbContext
    {
        public NewsContext() : base("NewsContext")
        {
            if (Database.Connection.State == System.Data.ConnectionState.Closed)
            {
                Database.Connection.Open();
            }
        }

        public DbSet<NewsItem> NewsItems { get; set; }
        public DbSet<Feed> FeedNames { get; set; }
       
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
