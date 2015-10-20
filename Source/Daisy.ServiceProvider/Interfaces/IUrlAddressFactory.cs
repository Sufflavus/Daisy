using System;


namespace Daisy.ServiceProvider.Interfaces
{
    public interface IUrlAddressFactory
    {
        string GetAllArticles();
        string GetArticle(Guid articleId);
        string RemoveArticle(Guid articleId);
        string RemoveComment(Guid commentId);
        string SaveArticle();
        string SaveComment();
    }
}
