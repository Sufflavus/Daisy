using System;
using System.Collections.Generic;

using Daisy.Terminal.Models;


namespace Daisy.Terminal.ViewModels
{
    public class ArticleShowViewModel : WindowViewModelBase
    {
        private Article _article;


        public ArticleShowViewModel()
        {
        }


        public Article Article
        {
            get { return _article; }
            set
            {
                _article = value;
                RaisePropertyChangedEvent("Article");
            }
        }

        public override string DisplayName
        {
            get { return "Article"; }
            protected set { base.DisplayName = value; }
        }
    }
}
