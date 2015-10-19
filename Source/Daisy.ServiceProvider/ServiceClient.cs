using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Daisy.Contracts;

using Newtonsoft.Json;


namespace Daisy.ServiceProvider
{
    public class ServiceClient : IServiceClient
    {
        private const string JsonMediaType = "application/json";


        public List<ArticleInfo> GetAllArticles()
        {
            string uri = UrlAddressFactory.GetAllArticles();
            var data = GetData<List<ArticleInfo>>(uri);
            return data;
        }


        public ArticleInfo GetArticleById(Guid id)
        {
            string uri = UrlAddressFactory.GetArticle(id);
            var data = GetData<ArticleInfo>(uri);
            return data;
        }


        public void RemoveArticle(Guid id)
        {
            string uri = UrlAddressFactory.RemoveArticle(id);
            DeleteData(uri);
        }


        public void RemoveComment(Guid id)
        {
            string uri = UrlAddressFactory.RemoveComment(id);
            DeleteData(uri);
        }


        public Guid SaveArticle(ArticleInfo article)
        {
            string uri = UrlAddressFactory.SaveArticle();
            string jsonPostData = JsonConvert.SerializeObject(article);
            var articleId = PostWithGetData<Guid>(uri, jsonPostData);
            return articleId;
        }


        public Guid SaveComment(CommentInfo comment)
        {
            string uri = UrlAddressFactory.SaveComment();
            string jsonPostData = JsonConvert.SerializeObject(comment);
            var commentId = PostWithGetData<Guid>(uri, jsonPostData);
            return commentId;
        }


        private bool DeleteData(string uri)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(uri);

                using (Task<HttpResponseMessage> task = client.DeleteAsync(uri))
                {
                    task.Result.EnsureSuccessStatusCode();
                    return true;
                }
            }
        }


        private T GetData<T>(string uri)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(uri);
                using (Task<HttpResponseMessage> task = client.GetAsync(uri))
                {
                    HttpResponseMessage response = task.Result;
                    Task<string> jsonResult = response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<T>(jsonResult.Result);
                    return result;
                }
            }
        }


        private bool PostData(string uri, string jsonPostData)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(uri);

                var requestContent = new StringContent(jsonPostData, Encoding.UTF8, JsonMediaType);

                using (Task<HttpResponseMessage> task = client.PostAsync(uri, requestContent))
                {
                    task.Result.EnsureSuccessStatusCode();
                    return true;
                }
            }
        }


        private T PostWithGetData<T>(string uri, string jsonPostData)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(uri);

                    var requestContent = new StringContent(jsonPostData, Encoding.UTF8, JsonMediaType);

                    using (Task<HttpResponseMessage> task = client.PostAsync(uri, requestContent))
                    {
                        HttpResponseMessage response = task.Result;
                        Task<string> jsonResult = response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<T>(jsonResult.Result);
                        return result;
                    }
                }
            }
            catch (AggregateException ex)
            {
                foreach (Exception e in ex.InnerExceptions)
                {
                    //_log.Error(e);
                }
                //return Bag<T>.Empty;
                throw;
            }
            catch (Exception ex)
            {
                throw;
                //_log.Error(ex);
                //return Bag<T>.Empty;
            }
        }
    }
}
