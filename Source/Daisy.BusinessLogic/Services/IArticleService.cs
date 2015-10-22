using System;
using System.Collections.Generic;

using Daisy.BusinessLogic.Models;


namespace Daisy.BusinessLogic.Services
{
    public interface IArticleService
    {
        List<ArticleModel> GetAllArticles();
        ArticleModel GetArticleById(Guid id);
        void RemoveArticle(Guid id);
        Guid SaveArticle(ArticleModel article);
    }
}
