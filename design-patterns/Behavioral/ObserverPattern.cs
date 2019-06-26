using System;
using System.Collections.Generic;

namespace design_patterns
{
    /// <summary>
    /// Observer is a behavioral design pattern that lets you define a subscription mechanism to notify multiple objects 
    /// about any events that happen to the object they’re observing.
    /// </summary>
    class ObserverPattern : DesignPattern
    {
        public override string PatternName => "Observer";
        public override void ExecuteSample()
        {
            base.ExecuteSample();

            var eventManager = new EventManager();
            var textEditor = new TextEditor(eventManager);

            var alertListener = new AlertsListener();
            var loggingListener = new LoggingListener();

            eventManager.Subscribe(alertListener);
            eventManager.Subscribe(loggingListener);
            eventManager.Subscribe(loggingListener);

            textEditor.OpenFile();
            textEditor.SaveFile();

            eventManager.Unsubscribe(alertListener);
            textEditor.OpenFile();
        }
    }

    class TextEditor
    {
        private EventManager _eventManager;
        public TextEditor(EventManager manager)
        {
            _eventManager = manager;
        }

        public void OpenFile()
        {
            //open file
            _eventManager.Notify("opened file");
        }

        public void SaveFile()
        {
            //save file
            _eventManager.Notify("saved file");
        }
    }

    class EventManager
    {
        private List<ISubscriber> _subscribers;
        public EventManager()
        {
            _subscribers = new List<ISubscriber>();
        }

        public void Subscribe(ISubscriber subscriber)
        {
            _subscribers.Add(subscriber);
        }

        public void Unsubscribe(ISubscriber subscriber)
        {
            _subscribers.Remove(subscriber);
        }

        public void Notify(string message)
        {
            foreach (var subscriber in _subscribers)
            {
                subscriber.Update(new EventModel()
                {
                    Message = message
                });
            }
        }
    }

    interface ISubscriber
    {
        void Update(EventModel eventData);
    }

    class EventModel
    {
        public string Message { get; set; }
    }

    class LoggingListener : ISubscriber
    {
        public void Update(EventModel eventData)
        {
            Console.WriteLine($"Logging: Event received: {eventData.Message}");
        }
    }

    class AlertsListener : ISubscriber
    {
        public void Update(EventModel eventData)
        {
            Console.WriteLine($"Alerts: Event received: {eventData.Message}");
        }
    }
}
