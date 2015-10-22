using System;
using System.Collections.Generic;
using System.Reflection;
using System.ServiceModel;

using Daisy.Contracts;
using Daisy.Dal.Domain;
using Daisy.Dal.Repository.Interfaces;

using Nelibur.ObjectMapper;

using Ninject;


namespace Daisy.Service
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class DaisyService : IDaisyService
    {
        private IArticleRepository _articleRepository;
        private ICommentRepository _commentRepository;


        public DaisyService()
        {
            BuildDependencies();
            InitMapper();
        }


        public List<ArticleInfo> GetAllArticles()
        {
            return _articleRepository.GetAll()
                .ConvertAll(x => TinyMapper.Map<ArticleInfo>(x));
        }


        public ArticleInfo GetArticle(Guid articleId)
        {
            ArticleEntity entity = _articleRepository.GetById(articleId);
            return TinyMapper.Map<ArticleInfo>(entity);
        }


        public void RemoveArticle(Guid articleId)
        {
            _articleRepository.Remove(articleId);
        }


        public void RemoveComment(Guid commentId)
        {
            _commentRepository.Remove(commentId);
        }


        public ArticleInfo SaveArticle(ArticleInfo article)
        {
            var entity = TinyMapper.Map<ArticleEntity>(article);
            _articleRepository.AddOrUpdate(entity);
            article.Id = entity.Id;
            return article;
        }


        public CommentInfo SaveComment(CommentInfo comment)
        {
            var entity = TinyMapper.Map<CommentEntity>(comment);
            _commentRepository.AddOrUpdate(entity);
            comment.Id = entity.Id;
            return comment;
        }


        private void BuildDependencies()
        {
            IKernel kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());

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
