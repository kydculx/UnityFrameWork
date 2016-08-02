//using UnityEngine;
using System.Collections;
//using System.Collections.Generic;

public class FWTask
{
    public delegate void FinishedHandler(bool bManual);
    public event FinishedHandler Finished;

    private bool m_bRunning;
    private bool m_bPaused;

    private IEnumerator m_Coroutine;

    private bool m_bFinished;
    public bool TaskFinished { get { return m_bFinished; } }

    private string m_strTaskName;
    public string TaskName { get { return m_strTaskName; } }


    public FWTask(IEnumerator coroutine, string taskName, bool shouldStart = true)
    {
        m_Coroutine = coroutine;
        m_strTaskName = taskName;
        if (shouldStart)
        {
            Start();
        }
    }

    public void Start()
    {
        m_bRunning = true;
        FWTaskManager.Instance.StartCoroutine(DoWork());
    }

    public IEnumerator StartAsCoroutine()
    {
        m_bRunning = true;
        yield return FWTaskManager.Instance.StartCoroutine(DoWork());
    }

    public void Pause()
    {
        m_bPaused = true;
    }

    public void UnPause()
    {
        m_bPaused = false;
    }

    public void Stop(float fDelayInSeconds)
    {
        var a_Delay = (int)(fDelayInSeconds * 1000);
        new System.Threading.Timer(obj =>
        {
            lock (this)
            {
                Stop();
            }
        }, null, a_Delay, System.Threading.Timeout.Infinite);
    }

    public void Stop()
    {
        m_bFinished = true;
        m_bRunning = false;
        m_bPaused = false;
    }

    private IEnumerator DoWork()
    {
        yield return null;

        while (m_bRunning)
        {
            if (m_bPaused)
            {
                yield return null;
            }
            else
            {
                if (m_Coroutine.MoveNext())
                {
                    yield return m_Coroutine.Current;
                }
                else
                {
                    m_bRunning = false;
                }
            }
        }

        if (Finished != null)
        {
            Stop();
            Finished(m_bFinished);
        }
    }
}