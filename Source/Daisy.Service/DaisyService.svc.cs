using System;
using System.Collections.Generic;
using System.ServiceModel;

using Daisy.Contracts;


namespace Daisy.Service
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class DaisyService : IDaisyService
    {
        public List<ArticleInfo> GetAllArticles()
        {
            throw new NotImplementedException();
        }


        public ArticleInfo GetArticle(Guid articleId)
        {
            throw new NotImplementedException();
        }


        public ArticleInfo RemoveArticle(Guid articleId)
        {
            throw new NotImplementedException();
        }


        public ArticleInfo RemoveComment(Guid commentId)
        {
            throw new NotImplementedException();
        }


        public ArticleInfo SaveArticle(ArticleInfo article)
        {
            throw new NotImplementedException();
        }


        public ArticleInfo SaveComment(CommentInfo comment)
        {
            throw new NotImplementedException();
        }
    }
}
