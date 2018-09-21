using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSSModel.Model
{
    public class NewsItem :  Base
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string PublishDate { get; set; }
        public string Deleted { get; set; }
        public string CreatedDate { get; set; }
        public string LastUpdatedDate { get; set; }
        public string Url { get; set; }
    }   
}
