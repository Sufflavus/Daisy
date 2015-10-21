using System;
using System.Collections.Generic;


namespace Daisy.Dal.Entities
{
    public sealed class ArticleEntity
    {
        public ArticleEntity()
        {
            Comments = new List<CommentEntity>();
        }


        public List<CommentEntity> Comments { get; set; }

        public DateTime CreateDate { get; set; }
        public Guid? Id { get; set; }
        public string Text { get; set; }
        public string Title { get; set; }
    }
}
