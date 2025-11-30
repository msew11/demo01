using System;

namespace eventbus
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class EventListenAttribute : Attribute
    {
        public Type EventType { get; }

        public EventListenAttribute(Type eventType)
        {
            EventType = eventType;
        }
    }
}