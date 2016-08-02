using UnityEngine;
using System;
using System.Collections.Generic;

public class FWEventHandlerManager : FWSingleton<FWEventHandlerManager>
{
    Dictionary<string, List<EventHandler>> m_listEventDictionary = new Dictionary<string, List<EventHandler>>();

    void Destroy()
    {
        m_listEventDictionary.Clear();
    }

    public void Register(string key, EventHandler eventHandler)
    {
        if (!m_listEventDictionary.ContainsKey(key))
        {
            List<EventHandler> evtList = new List<EventHandler>();
            m_listEventDictionary.Add(key, evtList);
        }

        m_listEventDictionary[key].Add(eventHandler);
    }

    public void UnRegister(string key, EventHandler eventHandler)
    {
        if (!m_listEventDictionary.ContainsKey(key)) return;

        m_listEventDictionary[key].Remove(eventHandler);
    }

    public void Clean(string key)
    {
        if (!m_listEventDictionary.ContainsKey(key)) return;

        m_listEventDictionary[key].Clear();
        m_listEventDictionary.Remove(key);
    }

    public void Excute(string key)
    {
        if (!m_listEventDictionary.ContainsKey(key)) return;

        EventHandler handler = null;
        for (int i = 0; i < m_listEventDictionary[key].Count; ++i)
        {
            handler = m_listEventDictionary[key][i];
            handler(this, EventArgs.Empty);
        }
    }

    public void Clean()
    {
        m_listEventDictionary.Clear();
    }
}