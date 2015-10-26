using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Windows.Input;

using Daisy.BusinessLogic;
using Daisy.BusinessLogic.Models;
using Daisy.BusinessLogic.Services;
using Daisy.Terminal.Log;
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
        private string _errorMessage;
        private ILogger _logger;
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

        public string ErrorMessage
        {
            get { return _errorMessage; }
            set
            {
                _errorMessage = value;
                RaisePropertyChangedEvent("ErrorMessage");
            }
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
                    _selectedArticle = GetArticleDetails(value);
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
            _logger = kernel.Get<ILogger>();
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
            Guid articleId = Guid.NewGuid();
            Article article = GetArticleDetails(articleId);
            if (article == null)
            {
                _logger.Error(string.Format("Article with Id={0} is not found", articleId));
                ErrorMessage = "Error added in log";
            }
        }


        private void DoRemoveArticle()
        {
            int articleIndex = Articles.IndexOf(SelectedArticle);

            if (articleIndex < 0)
            {
                return;
            }

            Guid articleId = SelectedArticle.Id.Value;

            try
            {
                _articleService.RemoveArticle(articleId);
                SelectNextArticle(articleIndex);
                Articles.RemoveAt(articleIndex);
            }
            catch (ServiceException ex)
            {
                _logger.Error(string.Format("Error occured while removing Article with Id={0}", articleId), ex);
                ErrorMessage = "Error occured";
            }
        }


        private Article GetArticleDetails(Article article)
        {
            if (article is NewArticle)
            {
                return article;
            }

            return GetArticleDetails(article.Id.Value);
        }


        private Article GetArticleDetails(Guid articleId)
        {
            ArticleModel articleModel = _articleService.GetArticleById(articleId);

            if (articleModel == null)
            {
                return null;
            }
            return TinyMapper.Map<Article>(articleModel);
        }


        private List<Article> GetArticles()
        {
            return _articleService.GetAllArticles()
                .ConvertAll(x => TinyMapper.Map<Article>(x))
                .OrderBy(x => x.CreateDate)
                .ToList();
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

            if (!TrySaveArticle(article))
            {
                return;
            }

            Articles.Add(article);
            Articles.Remove(newArticle);

            SelectedArticle = article;
        }


        private void SelectNextArticle(int selectedArticleIndex)
        {
            if (selectedArticleIndex == 0 && Articles.Count == 1)
            {
                SelectedArticle = null;
            }
            else if (selectedArticleIndex == Articles.Count - 1)
            {
                SelectedArticle = Articles[selectedArticleIndex - 1];
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
                SelectedArticleViewModel = new ArticleShowViewModel(_commentService, _logger)
                {
                    Article = _selectedArticle
                };
            }
        }


        private bool TrySaveArticle(Article article)
        {
            try
            {
                var model = TinyMapper.Map<ArticleModel>(article);
                Guid id = _articleService.SaveArticle(model);
                article.Id = id;
                return true;
            }
            catch (ArgumentException ex)
            {
                _logger.Error(string.Format("Error occured while adding Article"), ex);
                ErrorMessage = string.Format("{0} is too long", ex.ParamName);
                return false;
            }
            catch (Exception ex)
            {
                _logger.Error(string.Format("Error occured while adding Article"), ex);
                ErrorMessage = "Error occured";
                return false;
            }
        }
    }
}
