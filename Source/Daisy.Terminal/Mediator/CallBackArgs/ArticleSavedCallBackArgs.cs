using System;

using Daisy.Terminal.Models;


namespace Daisy.Terminal.Mediator.CallBackArgs
{
    public class ArticleSavedCallBackArgs : NotificationCallBackArgs
    {
        public Article Article { get; set; }
    }
}
