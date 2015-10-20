using System;

using Daisy.BusinessLogic.Models;
using Daisy.Contracts;
using Daisy.ServiceProvider;
using Daisy.ServiceProvider.Interfaces;

using Nelibur.ObjectMapper;


namespace Daisy.BusinessLogic.Services
{
    public class CommentService : ICommentService
    {
        private readonly IServiceClient _serviceClient;


        public CommentService(IServiceClient serviceClient)
        {
            _serviceClient = serviceClient;
            InitMapper();
        }


        public void RemoveComment(Guid id)
        {
            _serviceClient.RemoveArticle(id);
        }


        public Guid SaveComment(CommentModel comment)
        {
            var commentInfo = TinyMapper.Map<CommentInfo>(comment);
            return _serviceClient.SaveComment(commentInfo);
        }


        private void InitMapper()
        {
            TinyMapper.Bind<CommentModel, CommentInfo>();
        }
    }
}
