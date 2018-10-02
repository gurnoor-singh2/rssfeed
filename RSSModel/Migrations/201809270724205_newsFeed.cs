namespace NewsModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newsFeed : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.NewsItem", "FeedName_Id", c => c.Int());
            CreateIndex("dbo.NewsItem", "FeedName_Id");
            AddForeignKey("dbo.NewsItem", "FeedName_Id", "dbo.FeedName", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.NewsItem", "FeedName_Id", "dbo.FeedName");
            DropIndex("dbo.NewsItem", new[] { "FeedName_Id" });
            DropColumn("dbo.NewsItem", "FeedName_Id");
        }
    }
}
