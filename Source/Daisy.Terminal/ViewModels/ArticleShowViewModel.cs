using System;
using System.Collections.ObjectModel;

using Daisy.Terminal.Models;


namespace Daisy.Terminal.ViewModels
{
    public sealed class ArticleShowViewModel : ArticleViewModel
    {
        private ObservableCollection<CommentShowViewModel> _comments;


        public ArticleShowViewModel()
        {
            _comments = new ObservableCollection<CommentShowViewModel>();
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

        public string Text
        {
            get { return _article.Text; }
        }

        public string Title
        {
            get { return _article.Title; }
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
