using System;
using System.Collections.Generic;


namespace Daisy.BusinessLogic.Models
{
    public class ArticleModel
    {
        public List<CommentModel> Comments { get; set; }

        public DateTime? CreateDate { get; set; }

        public Guid? Id { get; set; }

        public string Text { get; set; }

        public string Title { get; set; }
    }
}
