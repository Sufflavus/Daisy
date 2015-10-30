using System;

using Daisy.Dal.Context;
using Daisy.Dal.Context.Interfaces;
using Daisy.Dal.Domain;
using Daisy.Dal.Repository;

using Moq;

using Xunit;


namespace Daisy.Tests.Daisy.Dal
{
    public class CommentRepositoryTests
    {
        private const string FiftyLetters = "1234567890qwertyuiop1234567890qwertyuiop1234567890";


        [Fact]
        public void AddOrUpdate_CommentWithTooLongText_Thows()
        {
            var context = new Mock<IContext>();
            var repository = new CommentRepository(context.Object);
            var comment = new CommentEntity
            {
                Text = FiftyLetters + "a"
            };

            var ex = Assert.Throws<ArgumentException>(() => repository.AddOrUpdate(comment));

            Assert.Equal("Text", ex.ParamName);
        }


        [Fact]
        public void AddOrUpdate_CorrectComment_AddedInContext()
        {
            var context = new MockContext();
            var repository = new CommentRepository(context);
            var comment = new CommentEntity
            {
                Text = "text text",
                CreateDate = DateTime.Now
            };

            repository.AddOrUpdate(comment);

            Assert.Equal(comment, context.Storage[0]);
        }


        [Fact]
        public void AddOrUpdate_CorrectComment_ContextCalled()
        {
            var context = new Mock<IContext>();
            var repository = new CommentRepository(context.Object);
            var comment = new CommentEntity
            {
                Text = "text text",
                CreateDate = DateTime.Now
            };

            repository.AddOrUpdate(comment);

            context.Verify(x => x.AddOrUpdate(comment));
        }


        [Fact]
        public void Remove_ContextCalled()
        {
            var context = new Mock<IContext>();
            var repository = new CommentRepository(context.Object);
            Guid id = Guid.NewGuid();
            repository.Remove(id);

            context.Verify(x => x.Remove<CommentEntity>(id));
        }
    }
}
