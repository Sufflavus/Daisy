using System;


namespace Daisy.BusinessLogic.Models
{
    public class CommentModel
    {
        public Guid ArticleId { get; set; }

        public DateTime? CreateDate { get; set; }

        public Guid? Id { get; set; }

        public string Text { get; set; }
    }
}
