using UnityEngine;
using System;
using System.Collections.Generic;

public class FWEventHandlerManager : FWSingleton<FWEventHandlerManager>
{
    private Dictionary<string, List<EventHandler>> m_listEvents = new Dictionary<string, List<EventHandler>>();

    void Destroy()
    {
        m_listEvents.Clear();
    }

    public void Add(string strKey, EventHandler eventHandler)
    {
        if (!m_listEvents.ContainsKey(strKey))
        {
            List<EventHandler> evtList = new List<EventHandler>();
            m_listEvents.Add(strKey, evtList);
        }

        m_listEvents[strKey].Add(eventHandler);
    }

    public void Remove(string strKey, EventHandler eventHandler)
    {
        if (!m_listEvents.ContainsKey(strKey)) return;

        m_listEvents[strKey].Remove(eventHandler);
    }

    public void Remove(string strKey)
    {
        if (!m_listEvents.ContainsKey(strKey)) return;

        m_listEvents[strKey].Clear();
        m_listEvents.Remove(strKey);
    }

    public void Excute(string strKey)
    {
        if (!m_listEvents.ContainsKey(strKey)) return;

        EventHandler handler = null;
        for (int i = 0; i < m_listEvents[strKey].Count; ++i)
        {
            handler = m_listEvents[strKey][i];
            handler(this, EventArgs.Empty);
        }
    }

    public void Clean()
    {
        m_listEvents.Clear();
    }
}