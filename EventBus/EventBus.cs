using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class EventBus<T> where T : IEvent 
{
    private static readonly HashSet<IEventBinding<T>> bindings = new HashSet<IEventBinding<T>>();

    public static void Subscribe(IEventBinding<T> binding) => bindings.Add(binding);
    public static void Unsubscribe(IEventBinding<T> binding) => bindings.Remove(binding);

    public static void Publish(T eventToPublish)
    {
        foreach (var binding in bindings.ToList())
        {
            binding.OnEvent?.Invoke(eventToPublish);
            binding.OnEventNoArgs?.Invoke();
        }
    }
    public static void Clear() => bindings.Clear();
}
