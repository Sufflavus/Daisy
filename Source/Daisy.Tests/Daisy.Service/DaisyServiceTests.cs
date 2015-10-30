using System;
using System.Collections.Generic;

using Daisy.Dal.Domain;
using Daisy.Dal.Repository.Interfaces;
using Daisy.Service;
using Daisy.Service.Log;

using Moq;

using Xunit;


namespace Daisy.Tests.Daisy.Service
{
    public class DaisyServiceTests
    {
        [Fact]
        public void GetAllArticles_RepositoryCalled()
        {
            var articleRepository = new Mock<IArticleRepository>();
            var commentRepository = new Mock<ICommentRepository>();
            var logger = new Mock<ILogger>();
            articleRepository.Setup(x => x.GetAll()).Returns(new List<ArticleEntity>());
            var service = new DaisyService(articleRepository.Object, commentRepository.Object, logger.Object);

            service.GetAllArticles();

            articleRepository.Verify(x => x.GetAll());
        }


        [Fact]
        public void GetArticle_RepositoryCalled()
        {
            var articleRepository = new Mock<IArticleRepository>();
            var commentRepository = new Mock<ICommentRepository>();
            var logger = new Mock<ILogger>();
            Guid articleId = Guid.NewGuid();
            articleRepository.Setup(x => x.GetById(articleId)).Returns(new ArticleEntity());
            var service = new DaisyService(articleRepository.Object, commentRepository.Object, logger.Object);

            service.GetArticle(articleId);

            articleRepository.Verify(x => x.GetById(articleId));
        }


        [Fact]
        public void RemoveArticle_RepositoryCalled()
        {
            var articleRepository = new Mock<IArticleRepository>();
            var commentRepository = new Mock<ICommentRepository>();
            var logger = new Mock<ILogger>();
            Guid articleId = Guid.NewGuid();
            var service = new DaisyService(articleRepository.Object, commentRepository.Object, logger.Object);

            service.RemoveArticle(articleId);

            articleRepository.Verify(x => x.Remove(articleId));
        }


        [Fact]
        public void RemoveComment_RepositoryCalled()
        {
            var articleRepository = new Mock<IArticleRepository>();
            var commentRepository = new Mock<ICommentRepository>();
            var logger = new Mock<ILogger>();
            Guid commentId = Guid.NewGuid();
            var service = new DaisyService(articleRepository.Object, commentRepository.Object, logger.Object);

            service.RemoveComment(commentId);

            commentRepository.Verify(x => x.Remove(commentId));
        }
    }
}
