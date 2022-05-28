using System;

namespace lijohnttle.Media.Photo.App.Events
{
    public interface IMessenger
    {
        void Subscribe<TMessage>(Action<TMessage> handler);

        void Unsubscribe<TMessage>(Action<TMessage> handler);
        
        void Publish<TMessage>(TMessage message);
    }
}
