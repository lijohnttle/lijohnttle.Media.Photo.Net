using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace lijohnttle.Media.Photo.App.Events
{
    public class Messenger : IMessenger
    {
        private Dictionary<Type, ArrayList> subscriptions = new Dictionary<Type, ArrayList>();

        public void Publish<TMessage>(TMessage message)
        {
            if (subscriptions.TryGetValue(typeof(TMessage), out var handlers))
            {
                foreach (var handler in handlers.Cast<Action<TMessage>>())
                {
                    handler(message);
                }
            }
        }

        public void Subscribe<TMessage>(Action<TMessage> handler)
        {
            if (!subscriptions.TryGetValue(typeof(TMessage), out var handlers))
            {
                handlers = new ArrayList();
                subscriptions[typeof(TMessage)] = handlers;
            }

            handlers.Add(handler);
        }

        public void Unsubscribe<TMessage>(Action<TMessage> handler)
        {
            if (subscriptions.TryGetValue(typeof(TMessage), out var handlers))
            {
                handlers.Remove(handler);
            }
        }
    }
}
