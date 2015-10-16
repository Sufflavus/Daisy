using System;

using Daisy.Terminal.Models;


namespace Daisy.Terminal.ViewModels
{
    public abstract class ArticleViewModel : WindowViewModelBase
    {
        protected Article _article = new Article();

        public Article Article
        {
            get { return _article; }
            set
            {
                _article = value;
                RaisePropertyChangedEvent("Article");
            }
        }
    }
}
