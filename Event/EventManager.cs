using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GEvent
{
    Action<object[]> action;
    string ID;
    int called_times;

    public GEvent(String ID, Action<object[]> evt)
    {
        this.ID = ID;
        this.action = evt;
        this.called_times = 0;
    }
    

    public void Call(object[] data)
    {        
        this.called_times ++;
        action.Invoke(data);
    }

    public String GetID()
    {
        return this.ID;
    }

    public int CalledTimes()
    {
        return this.called_times;
    }
}

public class EventManager : MonoBehaviour
{
    static List<KeyValuePair<string, GEvent>> events = new List<KeyValuePair<string, GEvent>>();
    
    public static void RegisterEvent(GEvent e)
    {
        events.Add(new KeyValuePair<string, GEvent>(e.GetID(), e));
    }

    //Now it's possible to assing multiple callbacks to the same id!
    public static void CallEvents(string EventID, params object[] data)
    {
        events.ForEach(e => { 
            if(e.Key == EventID) e.Value.Call(data);
        });
    }

    public static void CallEventsOnce(string EventID, params object[] data)
    {
        events.ForEach(e => { if(e.Key == EventID) if(e.Value.CalledTimes() == 0) e.Value.Call(data); });
    }

    public static List<GEvent> GetEvents(String EventID)
    {
        List<GEvent> _ev = new List<GEvent>();      

        events.ForEach(e => {
            if(e.Key == EventID) _ev.Add(e.Value);
        });

        return _ev;
    }

    static EventManager _inst;
    void Awake()
    {
        if(_inst != null)
        {
            DestroyImmediate(gameObject);
            return;
        }else
        {
            _inst = this;
            DontDestroyOnLoad(gameObject);
        }
    }

}
