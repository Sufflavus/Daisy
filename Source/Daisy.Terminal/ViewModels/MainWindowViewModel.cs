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
        private ObservableCollection<Article> _articles;
        private Article _newArticle;
        private Article _selectedArticle;
        private ArticleViewModel _selectedArticleViewModel;
        private ArticleAddViewModel _newArticleViewModel;


        public MainWindowViewModel()
        {
            _articles = new ObservableCollection<Article>();
            InitArticles();
            _selectedArticle = _articles[0];
            _newArticle = new NewArticle();

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

        public Article NewArticle
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
                
                if (_selectedArticle is NewArticle)
                {
                    SelectedArticleViewModel = new ArticleAddViewModel();
                }
                else
                {
                    SelectedArticleViewModel = new ArticleShowViewModel
                    {
                        Article = _selectedArticle
                    };
                }
            }
        }

        public ArticleAddViewModel NewArticleViewModel
        {
            get { return _newArticleViewModel; }
            set
            {
                _newArticleViewModel = value;
                RaisePropertyChangedEvent("NewArticleViewModel");
            }
        }

        public ArticleViewModel SelectedArticleViewModel
        {
            get { return _selectedArticleViewModel; }
            set
            {
                _selectedArticleViewModel = value;
                RaisePropertyChangedEvent("SelectedArticleViewModel");
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
            
            Articles.Add(newArticle);

            NewArticle = newArticle;
            SelectedArticle = newArticle;

            //RaisePropertyChangedEvent("IsExistingArticle");
            //RaisePropertyChangedEvent("IsNewArticle");
        }


        private void InitArticles()
        {
            _articles = new ObservableCollection<Article>
            {
                new Article
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

            SelectedArticle = _articles[0];
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

            Articles.Add(article);

            SelectedArticle = article;
        }
    }
}
