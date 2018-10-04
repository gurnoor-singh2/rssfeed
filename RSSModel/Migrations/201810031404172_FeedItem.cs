namespace NewsModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FeedItem : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Feed",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Url = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FeedItem",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        Link = c.String(),
                        PubDate = c.String(),
                        Url_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.FeedItem");
            DropTable("dbo.Feed");
        }
    }
}
