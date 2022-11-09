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

public class EventManager
{
    static Dictionary<string, GEvent> events = new Dictionary<string, GEvent>();
    
    public static void RegisterEvent(GEvent e)
    {
        if(!events.ContainsKey(e.GetID()))
            events.Add(e.GetID(), e);
    }

    public static void CallEvent(string EventID, params object[] data)
    {
        GEvent e = null;
        
        if(events.TryGetValue(EventID, out e))
            e.Call(data);
    }

     public static void CallEventOnce(string EventID, params object[] data)
    {
        GEvent e = null;
        
        if(events.TryGetValue(EventID, out e))
            if(e.CalledTimes() == 0)
                e.Call(data);
    }

    public static GEvent GetEvent(String EventID)
    {
        GEvent e = null;        
        return events.TryGetValue(EventID, out e) ? e : null;
    }

}
