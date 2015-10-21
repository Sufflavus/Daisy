using System;
using System.Collections.Generic;


namespace Daisy.Dal.Domain
{
    public class ArticleEntity : BaseEntity
    {
        public ArticleEntity()
        {
            Comments = new List<CommentEntity>();
        }


        public virtual List<CommentEntity> Comments { get; set; }

        public virtual DateTime CreateDate { get; set; }
        public virtual string Text { get; set; }
        public virtual string Title { get; set; }
    }
}
