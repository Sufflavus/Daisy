using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

using Daisy.Contracts;
using Daisy.Contracts.Error;
using Daisy.ServiceProvider.Interfaces;

using Newtonsoft.Json;


namespace Daisy.ServiceProvider
{
    public class ServiceClient : IServiceClient
    {
        private const string JsonMediaType = "application/json";
        private readonly IUrlAddressFactory _urlAddressFactory;


        public ServiceClient(IUrlAddressFactory urlAddressFactory)
        {
            _urlAddressFactory = urlAddressFactory;
        }


        public List<ArticleInfo> GetAllArticles()
        {
            string uri = _urlAddressFactory.GetAllArticles();
            var data = GetData<List<ArticleInfo>>(uri);
            return data;
        }


        public ArticleInfo GetArticleById(Guid id)
        {
            string uri = _urlAddressFactory.GetArticle(id);
            var data = GetData<ArticleInfo>(uri);
            return data;
        }


        public void RemoveArticle(Guid id)
        {
            string uri = _urlAddressFactory.RemoveArticle(id);
            DeleteData(uri);
        }


        public void RemoveComment(Guid id)
        {
            string uri = _urlAddressFactory.RemoveComment(id);
            DeleteData(uri);
        }


        public Guid SaveArticle(ArticleInfo article)
        {
            string uri = _urlAddressFactory.SaveArticle();
            string jsonPostData = SerializeObjectForPost(article);
            var newArticle = PostWithGetData<ArticleInfo>(uri, jsonPostData);
            return newArticle.Id.Value;
        }


        public Guid SaveComment(CommentInfo comment)
        {
            string uri = _urlAddressFactory.SaveComment();
            string jsonPostData = SerializeObjectForPost(comment);
            var newComment = PostWithGetData<CommentInfo>(uri, jsonPostData);
            return newComment.Id.Value;
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
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(JsonMediaType));

                    var requestContent = new StringContent(jsonPostData, Encoding.UTF8, JsonMediaType);
                    requestContent.Headers.ContentType = new MediaTypeHeaderValue(JsonMediaType);

                    using (Task<HttpResponseMessage> task = client.PostAsync(uri, requestContent))
                    {
                        HttpResponseMessage response = task.Result;
                        Task<string> jsonResult = response.Content.ReadAsStringAsync();
                        if (response.IsSuccessStatusCode)
                        {
                            var result = JsonConvert.DeserializeObject<T>(jsonResult.Result);
                            return result;
                        }
                        else
                        {
                            var faultInfo = JsonConvert.DeserializeObject<ArgumentFaultInfo>(jsonResult.Result);
                            if (string.IsNullOrEmpty(faultInfo.ParamName))
                            {
                                throw new Exception(faultInfo.ErrorMessage);
                            }
                            else
                            {
                                throw new ArgumentException(faultInfo.ErrorMessage, faultInfo.ParamName);
                            }
                        }
                    }
                }
            }
            catch (ArgumentException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new CommunicationException(string.Format("Service error occurred: {0}", ex.Message), ex);
            }
        }


        private string SerializeObjectForPost<T>(T data)
        {
            var jsonSerializer = new DataContractJsonSerializer(typeof(T));
            using (var stream = new MemoryStream())
            {
                jsonSerializer.WriteObject(stream, data);
                return Encoding.UTF8.GetString(stream.ToArray());
            }
        }
    }
}
