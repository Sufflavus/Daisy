using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

using Daisy.Terminal.Models;


namespace Daisy.Terminal.ViewModels
{
    public sealed class ArticleShowViewModel : ArticleViewModel
    {
        private ObservableCollection<CommentShowViewModel> _comments;
        private bool _isAddPanelVisible;
        private CommentAddViewModel _newCommentViewModel;


        public ArticleShowViewModel()
        {
            _comments = new ObservableCollection<CommentShowViewModel>();
            _isAddPanelVisible = false;
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
                    CreateDate = DateTime.Now,
                    Text = "*"
                }
            };
            IsAddPanelVisible = true;
        }


        private void InitComments()
        {
            _article.Comments.ForEach(x =>
                _comments.Add(new CommentShowViewModel
                {
                    Comment = x
                }));
        }
    }
}
