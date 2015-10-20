using System;
using System.Collections.Generic;

using Daisy.Contracts;


namespace Daisy.ServiceProvider.Interfaces
{
    public interface IServiceClient
    {
        List<ArticleInfo> GetAllArticles();
        ArticleInfo GetArticleById(Guid id);
        void RemoveArticle(Guid id);
        void RemoveComment(Guid id);
        Guid SaveArticle(ArticleInfo article);
        Guid SaveComment(CommentInfo comment);
    }
}
