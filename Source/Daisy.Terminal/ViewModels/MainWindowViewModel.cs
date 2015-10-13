using System;
using System.Collections.Generic;

using Daisy.Terminal.Models;


namespace Daisy.Terminal.ViewModels
{
    public class MainWindowViewModel : WindowViewModelBase
    {
        private List<Article> _articles;


        public MainWindowViewModel()
        {
            _articles = new List<Article>();
            InitArticles();
        }


        public List<Article> Articles
        {
            get { return _articles; }
            set
            {
                _articles = value;
                RaisePropertyChangedEvent("Articles");
            }
        }

        public override string DisplayName
        {
            get { return "Articles"; }
            protected set { base.DisplayName = value; }
        }


        private void InitArticles()
        {
            _articles = new List<Article>
            {
                new Article
                {
                    Title = "Article 1",
                    CreateDate = DateTime.Now.Date,
                    Text = "Text text text"
                }
            };
        }
    }
}
