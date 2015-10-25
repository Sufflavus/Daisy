using System;
using System.Collections.Generic;


namespace Daisy.Terminal.Models
{
    public class Article : IEquatable<Article>
    {
        public Article()
        {
            Comments = new List<Comment>();
        }


        public List<Comment> Comments { get; set; }

        public DateTime CreateDate { get; set; }
        public Guid? Id { get; set; }
        public string Text { get; set; }
        public string Title { get; set; }


        public bool Equals(Article other)
        {
            return Id.Equals(other.Id);
        }
    }
}
