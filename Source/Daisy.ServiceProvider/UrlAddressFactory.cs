using System;
using System.IO;


namespace Daisy.ServiceProvider
{
    public static class UrlAddressFactory
    {
        private static readonly string _baseUri = "";


        public static string GetAllArticles()
        {
            return CreateUri(Terms.GetAllArticles);
        }


        public static string GetArticle(Guid articleId)
        {
            return CreateUri(string.Format(Terms.GetArticle, articleId));
        }


        public static string RemoveArticle(Guid articleId)
        {
            return CreateUri(string.Format(Terms.RemoveArticle, articleId));
        }


        public static string RemoveComment(Guid commentId)
        {
            return CreateUri(string.Format(Terms.RemoveComment, commentId));
        }


        public static string SaveArticle()
        {
            return CreateUri(Terms.SaveArticle);
        }


        public static string SaveComment()
        {
            return CreateUri(Terms.SaveComment);
        }


        private static string CreateUri(string relativeUrl)
        {
            return UrlPathCombine(_baseUri, relativeUrl);
        }


        private static string UrlPathCombine(string path1, string path2)
        {
            path1 = path1.TrimEnd('/') + "/";
            path2 = path2.TrimStart('/');

            return Path.Combine(path1, path2)
                .Replace(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
        }
    }
}
