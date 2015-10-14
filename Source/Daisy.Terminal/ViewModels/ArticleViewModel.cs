using System;

using Daisy.Terminal.Models;


namespace Daisy.Terminal.ViewModels
{
    public class ArticleViewModel : WindowViewModelBase
    {
        private Article _article = new Article();

        public Article Article
        {
            get { return _article; }
            set
            {
                _article = value;
                RaisePropertyChangedEvent("Article");
            }
        }

        public DateTime CreateDate
        {
            get { return _article.CreateDate; }
        }

        public override string DisplayName
        {
            get { return "Article"; }
            protected set { base.DisplayName = value; }
        }

        public string Text
        {
            get { return _article.Text; }
        }

        public string Title
        {
            get { return _article.Title; }
        }
    }
}
