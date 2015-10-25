using System;
using System.Collections.Generic;
using System.ServiceModel;

using Daisy.BusinessLogic.Models;
using Daisy.Contracts;
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
            try
            {
                return _serviceClient.GetAllArticles()
                    .ConvertAll(x => TinyMapper.Map<ArticleModel>(x));
            }
            catch (CommunicationException ex)
            {
                throw new ServiceException(ex);
            }
        }


        public ArticleModel GetArticleById(Guid id)
        {
            ArticleInfo articleInfo;
            try
            {
                articleInfo = _serviceClient.GetArticleById(id);
            }
            catch (CommunicationException ex)
            {
                throw new ServiceException(ex);
            }

            if (articleInfo == null)
            {
                return null;
            }

            return TinyMapper.Map<ArticleModel>(articleInfo);
        }


        public void RemoveArticle(Guid id)
        {
            try
            {
                _serviceClient.RemoveArticle(id);
            }
            catch (CommunicationException ex)
            {
                throw new ServiceException(ex);
            }
        }


        public Guid SaveArticle(ArticleModel article)
        {
            try
            {
                var articleInfo = TinyMapper.Map<ArticleInfo>(article);
                return _serviceClient.SaveArticle(articleInfo);
            }
            catch (ArgumentException)
            {
                throw;
            }
            catch (CommunicationException ex)
            {
                throw new ServiceException(ex);
            }
        }


        private void InitMapper()
        {
            TinyMapper.Bind<ArticleModel, ArticleInfo>();
        }
    }
}
