using System;


namespace Daisy.Dal.Domain
{
    public class CommentEntity : BaseEntity
    {
        public virtual Guid ArticleId { get; set; }
        public virtual ArticleEntity Article { get; set; }
        public virtual DateTime CreateDate { get; set; }
        public virtual string Text { get; set; }
    }
}
