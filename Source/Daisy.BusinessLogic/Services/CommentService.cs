using System;
using System.ServiceModel;

using Daisy.BusinessLogic.Models;
using Daisy.Contracts;
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
            try
            {
                _serviceClient.RemoveComment(id);
            }
            catch (CommunicationException ex)
            {
                throw new ServiceException(ex);
            }
        }


        public Guid SaveComment(CommentModel comment)
        {
            try
            {
                var commentInfo = TinyMapper.Map<CommentInfo>(comment);
                return _serviceClient.SaveComment(commentInfo);
            }
            catch (ArgumentException)
            {
                throw;
            }
            catch (CommunicationException ex)
            {
                throw new ServiceException(ex);
            }
        }


        private void InitMapper()
        {
            TinyMapper.Bind<CommentModel, CommentInfo>();
        }
    }
}
