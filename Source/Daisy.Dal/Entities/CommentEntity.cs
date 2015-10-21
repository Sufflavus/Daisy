using System;


namespace Daisy.Dal.Entities
{
    public sealed class CommentEntity
    {
        public Guid ArticleId { get; set; }
        public DateTime CreateDate { get; set; }
        public Guid? Id { get; set; }
        public string Text { get; set; }
    }
}
