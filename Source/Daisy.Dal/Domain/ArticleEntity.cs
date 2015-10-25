using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Daisy.Dal.Domain
{
    public class ArticleEntity : BaseEntity
    {
        public ArticleEntity()
        {
            Comments = new List<CommentEntity>();
        }


        public virtual IList<CommentEntity> Comments { get; set; }

        public virtual DateTime CreateDate { get; set; }

        [StringLength(200, ErrorMessage = "Text should be less or equal than 200")]
        public virtual string Text { get; set; }

        [StringLength(100, ErrorMessage = "Title should be less or equal than 100")]
        public virtual string Title { get; set; }
    }
}
