using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq.Expressions;
using RSSModel.Model;

namespace RSSRepository.Repositories.Interfaces
{
    public interface IBaseRepo<T> where T : Base
    {
        IList<T> FetchAll();
    }
}
