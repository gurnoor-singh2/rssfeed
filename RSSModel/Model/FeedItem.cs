namespace RSSModel.Model
{
    public class FeedItem :  Base
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public string PubDate { get; set; }
        public int Url_Id { get; set; }
        public string FeedXML { get; set; }
    }   
}
