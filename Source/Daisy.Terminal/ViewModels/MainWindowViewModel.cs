using System;
using System.Collections.Generic;
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
        private ArticleAddViewModel _newArticleViewModel;
        private Article _selectedArticle;
        private ArticleViewModel _selectedArticleViewModel;


        public MainWindowViewModel()
        {
            _articles = new ObservableCollection<Article>();
            InitArticles();
            _selectedArticle = _articles[0];

            ViewModelsMediator.Instance.Register(ViewModelMessageType.ArticleSaved, OnArticleAdded);
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

        public ICommand GetNonExistingArticleCommand
        {
            get { return new Command(DoGetNonExistingArticle); }
        }

        public bool IsExistingArticleSelected
        {
            get { return SelectedArticle != null && !IsNewArticleSelected; }
        }

        public bool IsNewArticleSelected
        {
            get { return SelectedArticle is NewArticle; }
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

        public ICommand RemoveArticleCommand
        {
            get { return new Command(DoRemoveArticle); }
        }

        public Article SelectedArticle
        {
            get { return _selectedArticle; }
            set
            {
                _selectedArticle = value;

                if (value != null)
                {

                    if (_selectedArticle is NewArticle)
                    {
                        NewArticleViewModel = new ArticleAddViewModel
                        {
                            Article = _selectedArticle
                        };
                    }
                    else
                    {
                        SelectedArticleViewModel = new ArticleShowViewModel
                        {
                            Article = _selectedArticle
                        };
                    }
                }

                RaisePropertyChangedEvent("SelectedArticle");
                RaisePropertyChangedEvent("IsExistingArticleSelected");
                RaisePropertyChangedEvent("IsNewArticleSelected");
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
            var newArticle = new NewArticle
            {
                Title = "*",
                CreateDate = DateTime.Now.Date,
                Text = "new asd asfdsdf sfdsdf"
            };

            Articles.Add(newArticle);
            SelectedArticle = newArticle;
        }


        private void DoGetNonExistingArticle()
        {
        }


        private void DoRemoveArticle()
        {
            int articleIndex = _articles.IndexOf(_selectedArticle);

            if (articleIndex < 0)
            {
                return;
            }

            if (articleIndex == 0 && _articles.Count == 1)
            {
                SelectedArticle = null;
            }
            else if (articleIndex == _articles.Count - 1)
            {
                SelectedArticle = _articles[articleIndex - 1];
            }
            else if (articleIndex < _articles.Count - 1)
            {
                SelectedArticle = _articles[articleIndex + 1];
            }

            Articles.RemoveAt(articleIndex);
        }


        private void InitArticles()
        {
            Guid articleId1 = Guid.NewGuid();
            Guid articleId2 = Guid.NewGuid();
            Guid articleId3 = Guid.NewGuid();

            _articles = new ObservableCollection<Article>
            {
                new Article
                {
                    Id = articleId1,
                    Title = "Article 1",
                    CreateDate = DateTime.Now.AddDays(-3),
                    Text = "Text text text",
                    Comments = new List<Comment>
                    {
                        new Comment
                        {
                            Text = "comment 1",
                            CreateDate = DateTime.Now.AddDays(-3),
                            ArticleId = articleId1
                        },
                        new Comment
                        {
                            Text = "comment 2",
                            CreateDate = DateTime.Now.AddDays(-2),
                            ArticleId = articleId1
                        },
                        new Comment
                        {
                            Text = "comment 3",
                            CreateDate = DateTime.Now.AddDays(-1),
                            ArticleId = articleId1
                        }
                    }
                },
                new Article
                {
                    Id = articleId2,
                    Title = "Article 2",
                    CreateDate = DateTime.Now,
                    Text = "Text text text dfg dfgdfg dfgdfgh"
                },
                new Article
                {
                    Id = articleId3,
                    Title = "Article 3",
                    CreateDate = DateTime.Now,
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
                Id = Guid.NewGuid(),
                Title = newArticle.Title,
                CreateDate = newArticle.CreateDate,
                Text = newArticle.Text
            };

            Articles.Add(article);
            Articles.Remove(newArticle);

            SelectedArticle = article;
        }
    }
}
