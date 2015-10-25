using System;
using System.ComponentModel.DataAnnotations;


namespace Daisy.Dal.Domain
{
    public class CommentEntity : BaseEntity
    {
        public virtual ArticleEntity Article { get; set; }
        public virtual Guid ArticleId { get; set; }
        public virtual DateTime CreateDate { get; set; }

        [StringLength(50, ErrorMessage = "Text should be less or equal than 50")]
        public virtual string Text { get; set; }
    }
}
