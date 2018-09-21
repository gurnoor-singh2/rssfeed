using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core.Objects;
using System.Data.Linq;
using RSSModel.Model;
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
       
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
