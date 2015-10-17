using System;

using Daisy.Terminal.Mediator.CallBackArgs;


namespace Daisy.Terminal.Mediator
{
    public sealed class ViewModelsMediator
    {
        private static readonly ViewModelsMediator instance = new ViewModelsMediator();

        private readonly SubscribersDictionary subscribers = new SubscribersDictionary();


        static ViewModelsMediator()
        {
        }


        private ViewModelsMediator()
        {
        }


        public static ViewModelsMediator Instance
        {
            get { return instance; }
        }


        public void NotifySubscribers(ViewModelMessageType messageType, NotificationCallBackArgs args)
        {
            if (!subscribers.ContainsKey(messageType))
            {
                return;
            }

            foreach (Action<NotificationCallBackArgs> callback in subscribers[messageType])
            {
                callback(args);
            }
        }


        public void Register(ViewModelMessageType messageType, Action<NotificationCallBackArgs> callback)
        {
            subscribers.AddValue(messageType, callback);
        }
    }
}
