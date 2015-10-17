using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

using Daisy.Terminal.Mediator;
using Daisy.Terminal.Mediator.CallBackArgs;
using Daisy.Terminal.Models;


namespace Daisy.Terminal.ViewModels
{
    public sealed class MainWindowViewModel : WindowViewModelBase
    {
        //http://stackoverflow.com/questions/4488463/how-i-can-refresh-listview-in-wpf
        private ObservableCollection<ArticleViewModel> _articles;
        private ArticleAddViewModel _newArticle;
        private ArticleViewModel _selectedArticle;


        public MainWindowViewModel()
        {
            _articles = new ObservableCollection<ArticleViewModel>();
            InitArticles();
            _selectedArticle = _articles[0];
            _newArticle = new ArticleAddViewModel();

            ViewModelsMediator.Instance.Register(ViewModelMessages.ArticleSaved, OnArticleAdded);
        }


        public ICommand AddArticleCommand
        {
            get { return new Command(DoAddArticle); }
        }

        public ObservableCollection<ArticleViewModel> Articles
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
            get { return SelectedArticle is ArticleAddViewModel; }
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

        public ArticleViewModel SelectedArticle
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
            var newArticle = new Article
            {
                Title = "*",
                CreateDate = DateTime.Now.Date,
                Text = "new asd asfdsdf sfdsdf"
            };

            var viewModel = new ArticleAddViewModel { Article = newArticle };
            Articles.Add(viewModel);
            NewArticle = viewModel;

            RaisePropertyChangedEvent("IsExistingArticle");
            RaisePropertyChangedEvent("IsNewArticle");
            SelectedArticle = viewModel;
        }


        private void InitArticles()
        {
            _articles.Add(new ArticleShowViewModel
            {
                Article = new Article
                {
                    Title = "Article 1",
                    CreateDate = DateTime.Now.Date,
                    Text = "Text text text",
                    Comments = new List<Comment>
                    {
                        new Comment
                        {
                            Text = "comment 1",
                            CreateDate = DateTime.Now.Date
                        },
                        new Comment
                        {
                            Text = "comment 2",
                            CreateDate = DateTime.Now.Date
                        },
                        new Comment
                        {
                            Text = "comment 3",
                            CreateDate = DateTime.Now.Date
                        }
                    }
                }
            });

            _articles.Add(new ArticleShowViewModel
            {
                Article = new Article
                {
                    Title = "Article 2",
                    CreateDate = DateTime.Now.Date,
                    Text = "Text text text dfg dfgdfg dfgdfgh"
                }
            });

            _articles.Add(new ArticleShowViewModel
            {
                Article = new Article
                {
                    Title = "Article 3",
                    CreateDate = DateTime.Now.Date,
                    Text = "Text text text sdfdg dfgdfgdfgdfg dfg"
                }
            });
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

            Articles.Remove(NewArticle);
            _newArticle = new ArticleAddViewModel();

            var showViewModel = new ArticleShowViewModel { Article = article };
            Articles.Add(showViewModel);

            SelectedArticle = showViewModel;
        }
    }
}
