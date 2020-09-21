using Nepal.Abstraction.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Nepal.Abstraction.Service.Business
{
    public interface IArticleService
    {
        Task Create(ArticleModel model);
        Task<ArticleViewModel> GetArticle(int Id);
        Task<List<ArticleViewModel>> GetArticles();
        Task<List<ArticleViewModel>> GetCurrentArticles(int Limit);
        Task DeleteArticle(int Id);
        Task UpdateArticle(ArticleModel model, int Id);
    }
}
