using Nepal.EF.DB.Context;
using Nepal.EF.DB.DataObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nepal.Data.Service
{
    public class ArticleRepository : CoreRepository<Article, CoreContext>
    {
        CoreContext _context;
        public ArticleRepository(CoreContext context) : base(context)
        {
            _context = context;
        }

        public Task<List<Article>> GetCurrentArticles(int limit)
        {
            var articles = _context.Articles.OrderByDescending(a => a.ArticleDate).Take(limit).ToList();
            return Task.FromResult(articles);
        }
    }
}
