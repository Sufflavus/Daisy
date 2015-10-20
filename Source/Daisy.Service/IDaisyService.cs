using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;

using Daisy.Contracts;


namespace Daisy.Service
{
    [ServiceContract]
    public interface IDaisyService
    {
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/GetAllArticles")]
        List<ArticleInfo> GetAllArticles();


        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/GetArticle?articleId={articleId}")]
        ArticleInfo GetArticle(Guid articleId);


        [OperationContract]
        [WebInvoke(Method = "DELETE", RequestFormat = WebMessageFormat.Json, UriTemplate = "/RemoveArticle?articleId={articleId}")]
        ArticleInfo RemoveArticle(Guid articleId);


        [OperationContract]
        [WebInvoke(Method = "DELETE", RequestFormat = WebMessageFormat.Json, UriTemplate = "/RemoveComment?commentId={commentId}")]
        ArticleInfo RemoveComment(Guid commentId);


        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, UriTemplate = "/SaveArticle")]
        ArticleInfo SaveArticle(ArticleInfo article);


        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, UriTemplate = "/SaveComment")]
        ArticleInfo SaveComment(CommentInfo comment);
    }
}
