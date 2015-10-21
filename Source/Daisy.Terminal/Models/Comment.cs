using System;


namespace Daisy.Terminal.Models
{
    public sealed class Comment
    {
        public Guid ArticleId { get; set; }
        public DateTime CreateDate { get; set; }
        public Guid? Id { get; set; }
        public string Text { get; set; }
    }
}
