using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Windows.Input;

using Daisy.BusinessLogic.Models;
using Daisy.BusinessLogic.Services;
using Daisy.Terminal.Mediator;
using Daisy.Terminal.Mediator.CallBackArgs;
using Daisy.Terminal.Models;

using Nelibur.ObjectMapper;

using Ninject;


namespace Daisy.Terminal.ViewModels
{
    public sealed class MainWindowViewModel : WindowViewModelBase
    {
        //http://stackoverflow.com/questions/4488463/how-i-can-refresh-listview-in-wpf
        private ObservableCollection<Article> _articles;
        private ArticleAddViewModel _newArticleViewModel;
        private Article _selectedArticle;
        private ArticleViewModel _selectedArticleViewModel;
        private IArticleService _service;


        public MainWindowViewModel()
        {
            BuildDependencies();
            InitMapper();
            InitArticles();

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
                    _selectedArticle = GetArticle(value);
                    SetSelectedArticleViewModel();
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


        private void BuildDependencies()
        {
            IKernel kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());
            _service = kernel.Get<IArticleService>();
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
            _service.GetArticleById(Guid.NewGuid());
        }


        private void DoRemoveArticle()
        {
            int articleIndex = _articles.IndexOf(_selectedArticle);

            if (articleIndex < 0)
            {
                return;
            }

            Guid articleId = _articles[articleIndex].Id.Value;
            _service.RemoveArticle(articleId);

            SelectNextArticle(articleIndex);
            Articles.RemoveAt(articleIndex);
        }


        private List<Article> GetArticles()
        {
            return _service.GetAllArticles()
                .ConvertAll(x => TinyMapper.Map<Article>(x));

            /*Guid articleId1 = Guid.NewGuid();
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
            };*/
        }


        private void InitArticles()
        {
            List<Article> articles = GetArticles();

            _articles = new ObservableCollection<Article>();
            articles.ForEach(_articles.Add);

            SelectedArticle = articles.Count > 0 ? _articles[0] : null;
        }


        private void InitMapper()
        {
            TinyMapper.Bind<ArticleModel, Article>();
            TinyMapper.Bind<CommentModel, Comment>();
        }


        private void OnArticleAdded(NotificationCallBackArgs args)
        {
            Article newArticle = ((ArticleSavedCallBackArgs)args).Article;

            var article = new Article
            {
                //Id = Guid.NewGuid(),
                Title = newArticle.Title,
                CreateDate = newArticle.CreateDate,
                Text = newArticle.Text
            };

            SaveArticle(article);

            Articles.Add(article);
            Articles.Remove(newArticle);

            SelectedArticle = article;
        }


        private void SaveArticle(Article article)
        {
            var model = TinyMapper.Map<ArticleModel>(article);
            Guid id = _service.SaveArticle(model);
            article.Id = id;
        }


        private void SelectNextArticle(int selectedArticleIndex)
        {
            if (selectedArticleIndex == 0 && _articles.Count == 1)
            {
                SelectedArticle = null;
            }
            else if (selectedArticleIndex == _articles.Count - 1)
            {
                SelectedArticle = _articles[selectedArticleIndex - 1];
            }
            else if (selectedArticleIndex < _articles.Count - 1)
            {
                SelectedArticle = _articles[selectedArticleIndex + 1];
            }
        }

        private Article GetArticle(Article article)
        {
            if (article is NewArticle)
            {
                return article;
            }

            var articleModel = _service.GetArticleById(article.Id.Value);
            return TinyMapper.Map<Article>(articleModel);
        }

        private void SetSelectedArticleViewModel()
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
    }
}
