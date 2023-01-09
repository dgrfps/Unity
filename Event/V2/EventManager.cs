using UnityEngine;
using System.Collections.Generic;

public class EventManager
{
    private static EventManager instance;

    public static EventManager GetInstance()
    {
        if (instance == null)
        {
            instance = new EventManager();
        }
        return instance;
    }

    private Dictionary<string, List<EventListener>> eventListeners;

    private EventManager()
    {
        eventListeners = new Dictionary<string, List<EventListener>>();
    }

    public void AddListener(string eventName, EventListener listener)
    {
        if (eventListeners.ContainsKey(eventName))
        {
            eventListeners[eventName].Add(listener);
        }
        else
        {
            eventListeners[eventName] = new List<EventListener> { listener };
        }
    }

    public void RemoveListener(string eventName, EventListener listener)
    {
        if (eventListeners.ContainsKey(eventName))
        {
            eventListeners[eventName].Remove(listener);
        }
    }

    public void TriggerEvent(string eventName, EventArgs args)
    {
        if (eventListeners.ContainsKey(eventName))
        {
            foreach (EventListener listener in eventListeners[eventName])
            {
                listener.OnEvent(eventName, args);
            }
        }
    }
}

public abstract class EventListener : MonoBehaviour
{
    public abstract void OnEvent(string eventName, EventArgs args);
}

public class EventArgs
{
    // Você pode adicionar campos e propriedades adicionais aqui
}
