﻿using RSSModel.Model;
using RSSRepository.Repositories.Interfaces;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace RSSRepository.Repositories.Impl
{
    public class NewsItemRepo : BaseRepo<FeedItem>, INewsItemRepo
    {
        public NewsItemRepo(DbContext dbContext) : base(dbContext)
        {
        }

        public IList<FeedItem> GetAllNewsItemsByFeedId(int Id)
        {
            return this.GetDbSet().Where(x => x.Url_Id == Id).ToList();
        }
    }
}
