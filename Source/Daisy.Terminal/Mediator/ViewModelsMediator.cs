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


        public void NotifySubscribers(ViewModelMessages message, NotificationCallBackArgs args)
        {
            if (!subscribers.ContainsKey(message))
            {
                return;
            }

            foreach (Action<NotificationCallBackArgs> callback in subscribers[message])
            {
                callback(args);
            }
        }


        public void Register(ViewModelMessages message, Action<NotificationCallBackArgs> callback)
        {
            subscribers.AddValue(message, callback);
        }
    }
}
