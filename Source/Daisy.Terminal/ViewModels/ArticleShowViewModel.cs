using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

using Daisy.BusinessLogic.Models;
using Daisy.BusinessLogic.Services;
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
        private CommentAddViewModel _newCommentViewModel;


        public ArticleShowViewModel(ICommentService service) : this()
        {
            _service = service;
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
            _article.Comments.RemoveAt(lastCommentIndex);
            Comments.RemoveAt(lastCommentIndex);
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
                SaveComment(newComment);
            }

            var commentViewModel = new CommentShowViewModel
            {
                Comment = newComment
            };

            _article.Comments.Insert(0, newComment);
            Comments.Insert(0, commentViewModel);
            IsAddPanelVisible = false;
        }


        private void SaveComment(Comment comment)
        {
            var model = TinyMapper.Map<CommentModel>(comment);
            Guid id = _service.SaveComment(model);
            comment.Id = id;
        }
    }
}
