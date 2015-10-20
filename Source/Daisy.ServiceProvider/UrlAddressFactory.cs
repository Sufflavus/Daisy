using System;
using System.IO;

using Daisy.ServiceProvider.Interfaces;


namespace Daisy.ServiceProvider
{
    public class UrlAddressFactory : IUrlAddressFactory
    {
        private readonly ISettingsProvider _settingsProvider;


        public UrlAddressFactory(ISettingsProvider settingsProvider)
        {
            _settingsProvider = settingsProvider;
        }


        public string GetAllArticles()
        {
            return CreateUri(Terms.GetAllArticles);
        }


        public string GetArticle(Guid articleId)
        {
            return CreateUri(string.Format(Terms.GetArticle, articleId));
        }


        public string RemoveArticle(Guid articleId)
        {
            return CreateUri(string.Format(Terms.RemoveArticle, articleId));
        }


        public string RemoveComment(Guid commentId)
        {
            return CreateUri(string.Format(Terms.RemoveComment, commentId));
        }


        public string SaveArticle()
        {
            return CreateUri(Terms.SaveArticle);
        }


        public string SaveComment()
        {
            return CreateUri(Terms.SaveComment);
        }


        private string CreateUri(string relativeUrl)
        {
            string baseUri = _settingsProvider.GetServerAddress();
            return UrlPathCombine(baseUri, relativeUrl);
        }


        private string UrlPathCombine(string path1, string path2)
        {
            path1 = path1.TrimEnd('/') + "/";
            path2 = path2.TrimStart('/');

            return Path.Combine(path1, path2)
                .Replace(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
        }
    }
}
