using System;

using Daisy.Terminal.Models;


namespace Daisy.Terminal.ViewModels
{
    public sealed class CommentShowViewModel : WindowViewModelBase
    {
        private Comment _comment;


        public CommentShowViewModel()
        {
            _comment = new Comment();
        }


        public Comment Comment
        {
            get { return _comment; }
            set
            {
                _comment = value;
                RaisePropertyChangedEvent("Comment");
            }
        }

        public DateTime CreateDate
        {
            get { return _comment.CreateDate; }
        }

        public string Text
        {
            get { return _comment.Text; }
        }
    }
}
