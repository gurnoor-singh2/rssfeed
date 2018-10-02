using RSSModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSSModel.Model
{
    public class FeedName : Base
    {
        public string Name { get; set; }
        public string Url { get; set; }


    public virtual IList<NewsItem> newsItems { get; set; }
    }
}
