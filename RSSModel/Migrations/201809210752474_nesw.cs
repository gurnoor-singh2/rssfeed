namespace RSSModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nesw : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.NewsItem", "PublishDate", c => c.String());
            AlterColumn("dbo.NewsItem", "CreatedDate", c => c.String());
            AlterColumn("dbo.NewsItem", "LastUpdatedDate", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.NewsItem", "LastUpdatedDate", c => c.DateTime());
            AlterColumn("dbo.NewsItem", "CreatedDate", c => c.DateTime());
            AlterColumn("dbo.NewsItem", "PublishDate", c => c.DateTime());
        }
    }
}
