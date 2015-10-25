using System;
using System.Collections.Generic;
using System.Reflection;
using System.ServiceModel;

using Daisy.Contracts;
using Daisy.Dal.Domain;
using Daisy.Dal.Repository.Interfaces;
using Daisy.Service.Log;

using Nelibur.ObjectMapper;

using Ninject;


namespace Daisy.Service
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class DaisyService : IDaisyService
    {
        private IArticleRepository _articleRepository;
        private ICommentRepository _commentRepository;
        private ILogger _logger;


        public DaisyService()
        {
            BuildDependencies();
            InitMapper();
        }


        public List<ArticleInfo> GetAllArticles()
        {
            try
            {
                return _articleRepository.GetAll()
                    .ConvertAll(x => TinyMapper.Map<ArticleInfo>(x));
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                throw new FaultException(ex.Message);
            }
        }


        public ArticleInfo GetArticle(Guid articleId)
        {
            ArticleEntity entity;

            try
            {
                entity = _articleRepository.GetById(articleId);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                throw new FaultException(ex.Message);
            }

            if (entity == null)
            {
                return null;
            }

            return TinyMapper.Map<ArticleInfo>(entity);
        }


        public void RemoveArticle(Guid articleId)
        {
            try
            {
                _articleRepository.Remove(articleId);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                throw new FaultException(ex.Message);
            }
        }


        public void RemoveComment(Guid commentId)
        {
            try
            {
                _commentRepository.Remove(commentId);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                throw new FaultException(ex.Message);
            }
        }


        public ArticleInfo SaveArticle(ArticleInfo article)
        {
            var entity = new ArticleEntity
            {
                Title = article.Title,
                Text = article.Text,
                CreateDate = article.CreateDate.Value
            };

            try
            {
                _articleRepository.AddOrUpdate(entity);
            }
            catch (ArgumentException ex)
            {
                _logger.Error(ex);

                throw new FaultException<ArgumentFaultInfo>(new ArgumentFaultInfo
                {
                    ErrorMessage = ex.Message,
                    ParamName = ex.ParamName
                });
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                throw new FaultException(ex.Message);
            }

            article.Id = entity.Id;
            return article;
        }


        public CommentInfo SaveComment(CommentInfo comment)
        {
            var entity = TinyMapper.Map<CommentEntity>(comment);

            try
            {
                _commentRepository.AddOrUpdate(entity);
            }
            catch (ArgumentException ex)
            {
                _logger.Error(ex);

                throw new FaultException<ArgumentFaultInfo>(new ArgumentFaultInfo
                {
                    ErrorMessage = ex.Message,
                    ParamName = ex.ParamName
                });
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                throw new FaultException(ex.Message);
            }

            comment.Id = entity.Id;
            return comment;
        }


        private void BuildDependencies()
        {
            IKernel kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());

            _logger = kernel.Get<ILogger>();
            _articleRepository = kernel.Get<IArticleRepository>();
            _commentRepository = kernel.Get<ICommentRepository>();
        }


        private void InitMapper()
        {
            TinyMapper.Bind<ArticleEntity, ArticleInfo>();
            TinyMapper.Bind<CommentEntity, CommentInfo>();
        }
    }
}
