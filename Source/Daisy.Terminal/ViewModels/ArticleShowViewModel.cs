using System;


namespace Daisy.Terminal.ViewModels
{
    public sealed class ArticleShowViewModel : ArticleViewModel
    {
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
