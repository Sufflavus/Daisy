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
        private IArticleService _articleService;
        private ObservableCollection<Article> _articles;
        private ICommentService _commentService;
        private ArticleAddViewModel _newArticleViewModel;
        private Article _selectedArticle;
        private ArticleViewModel _selectedArticleViewModel;


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
            _articleService = kernel.Get<IArticleService>();
            _commentService = kernel.Get<ICommentService>();
        }


        private void DoAddArticle()
        {
            var newArticle = new NewArticle
            {
                Title = "*",
                CreateDate = DateTime.Now.Date
            };

            Articles.Add(newArticle);
            SelectedArticle = newArticle;
        }


        private void DoGetNonExistingArticle()
        {
            _articleService.GetArticleById(Guid.NewGuid());
        }


        private void DoRemoveArticle()
        {
            int articleIndex = Articles.IndexOf(_selectedArticle);

            if (articleIndex < 0)
            {
                return;
            }

            Guid articleId = Articles[articleIndex].Id.Value;
            _articleService.RemoveArticle(articleId);

            SelectNextArticle(articleIndex);
            Articles.RemoveAt(articleIndex);
        }


        private Article GetArticle(Article article)
        {
            if (article is NewArticle)
            {
                return article;
            }

            ArticleModel articleModel = _articleService.GetArticleById(article.Id.Value);
            return TinyMapper.Map<Article>(articleModel);
        }


        private List<Article> GetArticles()
        {
            return _articleService.GetAllArticles()
                .ConvertAll(x => TinyMapper.Map<Article>(x));
        }


        private void InitArticles()
        {
            List<Article> articles = GetArticles();

            _articles = new ObservableCollection<Article>();
            articles.ForEach(_articles.Add);

            SelectedArticle = articles.Count > 0 ? Articles[0] : null;
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
            Guid id = _articleService.SaveArticle(model);
            article.Id = id;
        }


        private void SelectNextArticle(int selectedArticleIndex)
        {
            if (selectedArticleIndex == 0 && Articles.Count == 1)
            {
                SelectedArticle = null;
            }
            else if (selectedArticleIndex == Articles.Count - 1)
            {
                SelectedArticle = _articles[selectedArticleIndex - 1];
            }
            else if (selectedArticleIndex < Articles.Count - 1)
            {
                SelectedArticle = Articles[selectedArticleIndex + 1];
            }
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
                SelectedArticleViewModel = new ArticleShowViewModel(_commentService)
                {
                    Article = _selectedArticle
                };
            }
        }
    }
}
