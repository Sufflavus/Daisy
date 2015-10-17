﻿using System;
using System.Collections.Generic;


namespace Daisy.Terminal.Models
{
    public sealed class Article
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
    }
}
