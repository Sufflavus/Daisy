using System;

using Daisy.Terminal.Models;


namespace Daisy.Terminal.Mediator.CallBackArgs
{
    public class CommentSavedCallBackArgs : NotificationCallBackArgs
    {
        public Comment Comment { get; set; }
    }
}
