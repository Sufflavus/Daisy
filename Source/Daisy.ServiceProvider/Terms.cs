using System;


namespace Daisy.ServiceProvider
{
    public class Terms
    {
        public const string GetAllArticles = "/GetAllArticles";
        public const string GetArticle = "/GetArticle?articleId={0}";
        public const string RemoveArticle = "/RemoveArticle?articleId={0}";
        public const string RemoveComment = "/RemoveComment?commentId={0}";
        public const string SaveArticle = "/SaveArticle";
        public const string SaveComment = "/SaveComment";
    }
}
