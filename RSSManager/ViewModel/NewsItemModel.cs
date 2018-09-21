namespace RSSManager.ViewModel
{
    public class NewsItemModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string PublishDate { get; set; }
        public string Deleted { get; set; }
        public string CreatedDate { get; set; }
        public string LastUpdatedDate { get; set; }
        public string Url { get; set; }
    }
}
