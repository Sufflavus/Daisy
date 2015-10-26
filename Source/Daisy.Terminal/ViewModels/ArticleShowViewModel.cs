using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

using Daisy.BusinessLogic;
using Daisy.BusinessLogic.Models;
using Daisy.BusinessLogic.Services;
using Daisy.Terminal.Log;
using Daisy.Terminal.Mediator;
using Daisy.Terminal.Mediator.CallBackArgs;
using Daisy.Terminal.Models;

using Nelibur.ObjectMapper;


namespace Daisy.Terminal.ViewModels
{
    public sealed class ArticleShowViewModel : ArticleViewModel
    {
        private readonly ICommentService _service;
        private ObservableCollection<CommentShowViewModel> _comments;
        private bool _isAddPanelVisible;
        private ILogger _logger;
        private CommentAddViewModel _newCommentViewModel;


        public ArticleShowViewModel(ICommentService service, ILogger logger) : this()
        {
            _service = service;
            _logger = logger;
        }


        public ArticleShowViewModel()
        {
            _comments = new ObservableCollection<CommentShowViewModel>();
            _isAddPanelVisible = false;

            ViewModelsMediator.Instance.Register(ViewModelMessageType.CommentSaved, OnCommentAdded);
        }


        public ICommand AddCommentCommand
        {
            get { return new Command(DoAddComment); }
        }

        public Article Article
        {
            get { return _article; }
            set
            {
                _article = value;
                InitComments();
                RaisePropertyChangedEvent("Article");
            }
        }

        public ObservableCollection<CommentShowViewModel> Comments
        {
            get { return _comments; }
            set
            {
                _comments = value;
                RaisePropertyChangedEvent("Comments");
            }
        }

        public DateTime CreateDate
        {
            get { return _article.CreateDate; }
        }

        public override string DisplayName
        {
            get { return "Article"; }
            protected set { base.DisplayName = value; }
        }

        public bool IsAddPanelVisible
        {
            get { return _isAddPanelVisible; }
            set
            {
                _isAddPanelVisible = value;
                RaisePropertyChangedEvent("IsAddPanelVisible");
            }
        }

        public CommentAddViewModel NewCommentViewModel
        {
            get { return _newCommentViewModel; }
            set
            {
                _newCommentViewModel = value;
                RaisePropertyChangedEvent("NewCommentViewModel");
            }
        }

        public ICommand RemoveLastCommentCommand
        {
            get { return new Command(DoRemoveLastComment); }
        }

        public string Text
        {
            get { return _article.Text; }
        }

        public string Title
        {
            get { return _article.Title; }
        }


        private void DoAddComment()
        {
            NewCommentViewModel = new CommentAddViewModel
            {
                Comment = new Comment
                {
                    ArticleId = Article.Id.Value,
                    CreateDate = DateTime.Now,
                    Text = "*"
                }
            };
            IsAddPanelVisible = true;
        }


        private void DoRemoveLastComment()
        {
            if (_article.Comments.Count == 0)
            {
                return;
            }

            int lastCommentIndex = 0;
            Guid lastCommentId = Article.Comments[lastCommentIndex].Id.Value;

            try
            {
                _service.RemoveComment(lastCommentId);

                _article.Comments.RemoveAt(lastCommentIndex);
                Comments.RemoveAt(lastCommentIndex);
            }
            catch (ServiceException ex)
            {
                _logger.Error(string.Format("Error occured while removing Comment with Id={0}", lastCommentId), ex);
            }
        }


        private void InitComments()
        {
            _article.Comments.Sort((x, y) => y.CreateDate.CompareTo(x.CreateDate));
            _article.Comments.ForEach(x =>
                _comments.Add(new CommentShowViewModel
                {
                    Comment = x
                }));
        }


        private void OnCommentAdded(NotificationCallBackArgs args)
        {
            Comment newComment = ((CommentSavedCallBackArgs)args).Comment;

            if (newComment.ArticleId != Article.Id)
            {
                return;
            }

            if (!newComment.Id.HasValue)
            {
                if (!TrySaveComment(newComment))
                {
                    return;
                }
            }

            var commentViewModel = new CommentShowViewModel
            {
                Comment = newComment
            };

            _article.Comments.Insert(0, newComment);
            Comments.Insert(0, commentViewModel);
            IsAddPanelVisible = false;
        }


        private bool TrySaveComment(Comment comment)
        {
            try
            {
                var model = TinyMapper.Map<CommentModel>(comment);
                Guid id = _service.SaveComment(model);
                comment.Id = id;
                return true;
            }
            catch (ArgumentException ex)
            {
                _logger.Error(string.Format("Error occured while adding Article"), ex);
                NewCommentViewModel.ErrorMessage = string.Format("{0} is too long", ex.ParamName);
                return false;
            }
            catch (Exception ex)
            {
                _logger.Error(string.Format("Error occured while adding Article"), ex);
                NewCommentViewModel.ErrorMessage = "Error occured";
                return false;
            }
        }
    }
}
