namespace RSSModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newsItem : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.NewsItem",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        PublishDate = c.DateTime(),
                        Deleted = c.String(),
                        CreatedDate = c.DateTime(),
                        LastUpdatedDate = c.DateTime(),
                        Url = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.NewsItem");
        }
    }
}
