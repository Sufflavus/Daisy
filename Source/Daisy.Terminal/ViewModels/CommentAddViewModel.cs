using System;
using System.Windows.Input;

using Daisy.Terminal.Mediator;
using Daisy.Terminal.Mediator.CallBackArgs;
using Daisy.Terminal.Models;


namespace Daisy.Terminal.ViewModels
{
    public class CommentAddViewModel : WindowViewModelBase
    {
        private Comment _comment;
        private string _errorMessage;


        public CommentAddViewModel()
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

        public string ErrorMessage
        {
            get { return _errorMessage; }
            set
            {
                _errorMessage = value;
                RaisePropertyChangedEvent("ErrorMessage");
            }
        }

        public ICommand SaveCommentCommand
        {
            get { return new Command(DoSaveComment); }
        }

        public string Text
        {
            get { return _comment.Text; }
            set
            {
                _comment.Text = value;
                RaisePropertyChangedEvent("Text");
            }
        }


        private void DoSaveComment()
        {
            ViewModelsMediator.Instance.NotifySubscribers(ViewModelMessageType.CommentSaved,
                new CommentSavedCallBackArgs { Comment = Comment });
        }
    }
}
