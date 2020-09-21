using AutoMapper;
using Nepal.Abstraction.Model;
using Nepal.Abstraction.Service.Business;
using Nepal.Data.Service;
using Nepal.EF.DB.DataObject;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Nepal.Business.Service
{
    public class ArticleService : IArticleService
    {
        private ArticleRepository _articleRepository;
        private IBlobStoreService _blobStoreService;
        private readonly IMapper _mapper;
        public ArticleService(ArticleRepository articleRepository, IBlobStoreService blobStoreService, IMapper mapper)
        {
            _articleRepository = articleRepository;
            _blobStoreService = blobStoreService;
            _mapper = mapper;
        }
        public async Task Create(ArticleModel model)
        {
            var article = _mapper.Map<Article>(model);
            if (model.ImageFile != null) {
                var blob = new BlobStore
                {
                    FileName = model.ImageFile.FileName,
                    ContentType = model.ImageFile.ContentType,
                    FileLength = model.ImageFile.Length,
                    FileStream = model.ImageFile.OpenReadStream()
                };
                article.ImageUrl = await _blobStoreService.UploadFileToStore(blob);
             }
            article.ArticleDate = DateTime.Now;
            article.Status = 1;
            article.CreatedBy = "System";
            article.CreatedOn = DateTime.Now;
            await _articleRepository.Add(article);
        }

        public async Task<ArticleViewModel> GetArticle(int Id)
        {
            var article = await _articleRepository.Get(Id);
            return _mapper.Map<ArticleViewModel>(article);
        }

        public async Task<List<ArticleViewModel>> GetArticles()
        {
            var article = await _articleRepository.GetAll();
            return _mapper.Map<List<ArticleViewModel>>(article);
        }

        public async Task<List<ArticleViewModel>> GetCurrentArticles(int Limit)
        {
            var article = await _articleRepository.GetCurrentArticles(Limit);
            return _mapper.Map<List<ArticleViewModel>>(article);
        }

        public async Task DeleteArticle(int Id)
        {
            await _articleRepository.Delete(Id);
        }

        public async Task UpdateArticle(ArticleModel model, int Id)
        {
            var article = await _articleRepository.Get(Id);
            article.ModifiedBy = "SYSTEM";
            article.ModifiedOn = DateTime.Now;
            article.Title = model.Title;
            article.Body = model.Body;
            if (model.ImageFile != null)
            {
                var blob = new BlobStore
                {
                    FileName = model.ImageFile.FileName,
                    ContentType = model.ImageFile.ContentType,
                    FileLength = model.ImageFile.Length,
                    FileStream = model.ImageFile.OpenReadStream()
                };
                article.ImageUrl = await _blobStoreService.UploadFileToStore(blob);
            }
            await _articleRepository.Update(article);
        }
    }
}
