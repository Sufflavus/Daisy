using System;

using Daisy.Terminal.Mediator.CallBackArgs;


namespace Daisy.Terminal.Mediator
{
    public class SubscribersDictionary : MultiDictionary<ViewModelMessageType, Action<NotificationCallBackArgs>>
    {
    }
}
