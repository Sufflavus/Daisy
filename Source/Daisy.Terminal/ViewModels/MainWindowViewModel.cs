using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

using Daisy.Terminal.Models;


namespace Daisy.Terminal.ViewModels
{
    public class MainWindowViewModel : WindowViewModelBase
    {
        //http://stackoverflow.com/questions/4488463/how-i-can-refresh-listview-in-wpf
        private ObservableCollection<Article> _articles;
        private Article _selectedArticle;
        private Article _newArticle;

        public MainWindowViewModel()
        {
            _articles = new ObservableCollection<Article>();
            InitArticles();
            _selectedArticle = _articles[0];
            _newArticle = new Article();
        }


        public ICommand AddArticleCommand
        {
            get { return new Command<string>(x => DoAddArticle()); }
        }

        public ObservableCollection<Article> Articles
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

        public Article SelectedArticle
        {
            get { return _selectedArticle; }
            set
            {
                _selectedArticle = value;
                RaisePropertyChangedEvent("SelectedArticle");
            }
        }


        private void DoAddArticle()
        {
            var newArticle = new Article
            {
                Title = string.Format("Article {0}", _articles.Count + 1),
                CreateDate = DateTime.Now.Date,
                Text = "new asd asfdsdf sfdsdf"
            };
            Articles.Add(newArticle);
            SelectedArticle = newArticle;
        }


        private void InitArticles()
        {
            _articles = new ObservableCollection<Article>
            {
                new Article
                {
                    Title = "Article 1",
                    CreateDate = DateTime.Now.Date,
                    Text = "Text text text"
                },
                new Article
                {
                    Title = "Article 2",
                    CreateDate = DateTime.Now.Date,
                    Text = "Text text text dfg dfgdfg dfgdfgh"
                },
                new Article
                {
                    Title = "Article 3",
                    CreateDate = DateTime.Now.Date,
                    Text = "Text text text sdfdg dfgdfgdfgdfg dfg"
                }
            };
        }
    }
}
