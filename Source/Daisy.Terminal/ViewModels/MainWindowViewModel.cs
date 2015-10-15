using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

using Daisy.Terminal.Mediator;
using Daisy.Terminal.Mediator.CallBackArgs;
using Daisy.Terminal.Models;


namespace Daisy.Terminal.ViewModels
{
    public sealed class MainWindowViewModel : WindowViewModelBase
    {
        //http://stackoverflow.com/questions/4488463/how-i-can-refresh-listview-in-wpf
        private ObservableCollection<Article> _articles;
        private ArticleAddViewModel _newArticle;
        private Article _selectedArticle;


        public MainWindowViewModel()
        {
            _articles = new ObservableCollection<Article>();
            InitArticles();
            _selectedArticle = _articles[0];
            _newArticle = new ArticleAddViewModel();

            ViewModelsMediator.Instance.Register(ViewModelMessages.ArticleSaved, OnArticleAdded);
        }


        public ICommand AddArticleCommand
        {
            get { return new Command(DoAddArticle); }
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

        public bool IsExistingArticle
        {
            get { return !IsNewArticle; }
        }

        public bool IsNewArticle
        {
            get { return SelectedArticle is NewArticle; }
        }

        public ArticleAddViewModel NewArticle
        {
            get { return _newArticle; }
            set
            {
                _newArticle = value;
                RaisePropertyChangedEvent("NewArticle");
            }
        }

        public Article SelectedArticle
        {
            get { return _selectedArticle; }
            set
            {
                _selectedArticle = value;
                RaisePropertyChangedEvent("SelectedArticle");
                RaisePropertyChangedEvent("IsExistingArticle");
                RaisePropertyChangedEvent("IsNewArticle");
            }
        }


        private void DoAddArticle()
        {
            var newArticle = new NewArticle
            {
                Title = "*",
                CreateDate = DateTime.Now.Date,
                Text = "new asd asfdsdf sfdsdf"
            };
            Articles.Add(newArticle);

            var viewModel = new ArticleAddViewModel { Article = newArticle };
            NewArticle = viewModel;

            RaisePropertyChangedEvent("IsExistingArticle");
            RaisePropertyChangedEvent("IsNewArticle");
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


        private void OnArticleAdded(NotificationCallBackArgs args)
        {
            Article newArticle = ((ArticleSavedCallBackArgs)args).Article;

            var article = new Article
            {
                Title = newArticle.Title,
                CreateDate = newArticle.CreateDate,
                Text = newArticle.Text
            };

            Articles.Remove(newArticle);
            Articles.Add(article);

            _newArticle = new ArticleAddViewModel();

            SelectedArticle = article;
        }
    }
}
