using System;

using Daisy.Dal.Context;
using Daisy.Dal.Context.Interfaces;
using Daisy.Dal.Domain;
using Daisy.Dal.Repository;

using Moq;

using Xunit;


namespace Daisy.Tests.Daisy.Dal
{
    public class ArticleRepositoryTests
    {
        private const string HundredLetters = "1234567890qwertyuiop1234567890qwertyuiop1234567890" +
            "qwertyuiop1234567890qwertyuiop1234567890qwertyuiop";


        [Fact]
        public void AddOrUpdate_ArticleWithTooLongText_Thows()
        {
            var context = new Mock<IContext>();
            var connectionFactory = new Mock<IConnectionFactory>();
            var repository = new ArticleRepository(context.Object, connectionFactory.Object);
            var article = new ArticleEntity
            {
                Text = HundredLetters + HundredLetters + "a"
            };

            var ex = Assert.Throws<ArgumentException>(() => repository.AddOrUpdate(article));

            Assert.Equal("Text", ex.ParamName);
        }


        [Fact]
        public void AddOrUpdate_ArticleWithTooLongTitle_Thows()
        {
            var context = new Mock<IContext>();
            var connectionFactory = new Mock<IConnectionFactory>();
            var repository = new ArticleRepository(context.Object, connectionFactory.Object);
            var article = new ArticleEntity
            {
                Title = HundredLetters + "a"
            };

            var ex = Assert.Throws<ArgumentException>(() => repository.AddOrUpdate(article));

            Assert.Equal("Title", ex.ParamName);
        }


        [Fact]
        public void AddOrUpdate_CorrectArticle_AddedInContext()
        {
            var context = new MockContext();
            var connectionFactory = new Mock<IConnectionFactory>();
            var repository = new ArticleRepository(context, connectionFactory.Object);
            var article = new ArticleEntity
            {
                Title = "article 1",
                Text = "text text",
                CreateDate = DateTime.Now
            };

            repository.AddOrUpdate(article);

            Assert.Equal(article, context.Storage[0]);
        }


        [Fact]
        public void AddOrUpdate_CorrectArticle_ContextCalled()
        {
            var context = new Mock<IContext>();
            var connectionFactory = new Mock<IConnectionFactory>();
            var repository = new ArticleRepository(context.Object, connectionFactory.Object);
            var article = new ArticleEntity
            {
                Title = "article 1",
                Text = "text text",
                CreateDate = DateTime.Now
            };

            repository.AddOrUpdate(article);

            context.Verify(x => x.AddOrUpdate(article));
        }


        [Fact]
        public void GetAll_QueryExecuted()
        {
            var context = new Mock<IContext>();
            var connectionFactory = new Mock<IConnectionFactory>();
            var connection = new Mock<IConnection>();
            connectionFactory.Setup(x => x.CreateDapperConnection()).Returns(connection.Object);
            var repository = new ArticleRepository(context.Object, connectionFactory.Object);

            repository.GetAll();

            connectionFactory.Verify(x => x.CreateDapperConnection());
            connection.Verify(x => x.Open());
            connection.Verify(x => x.Query<ArticleEntity>(DbQueries.GetAllArticles));
            connection.Verify(x => x.Close());
        }


        [Fact]
        public void Remove_ContextCalled()
        {
            var context = new Mock<IContext>();
            var connectionFactory = new Mock<IConnectionFactory>();
            var repository = new ArticleRepository(context.Object, connectionFactory.Object);
            Guid id = Guid.NewGuid();
            repository.Remove(id);

            context.Verify(x => x.Remove<ArticleEntity>(id));
        }
    }
}
