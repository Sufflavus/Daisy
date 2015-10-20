using System;
using System.Collections.Generic;

using Daisy.BusinessLogic.Models;
using Daisy.Contracts;
using Daisy.ServiceProvider;
using Daisy.ServiceProvider.Interfaces;

using Nelibur.ObjectMapper;


namespace Daisy.BusinessLogic.Services
{
    public class ArticleService : IArticleService
    {
        private readonly IServiceClient _serviceClient;


        public ArticleService(IServiceClient serviceClient)
        {
            _serviceClient = serviceClient;
            InitMapper();
        }


        public List<ArticleModel> GetAllArticles()
        {
            return _serviceClient.GetAllArticles()
                .ConvertAll(x => TinyMapper.Map<ArticleModel>(x));
        }


        public ArticleModel GetArticleById(Guid id)
        {
            ArticleInfo articleInfo = _serviceClient.GetArticleById(id);
            return TinyMapper.Map<ArticleModel>(articleInfo);
        }


        public void RemoveArticle(Guid id)
        {
            _serviceClient.RemoveArticle(id);
        }


        private void InitMapper()
        {
            TinyMapper.Bind<ArticleModel, ArticleInfo>();
        }
    }
}
