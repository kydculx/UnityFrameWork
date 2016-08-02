using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FWTaskManager : FWSingleton<FWTaskManager>
{
    List<FWTask> m_Tasks = new List<FWTask>();
    int m_nCount;

    void Awake()
    {
        m_nCount = 1;
    }

    void Start()
    {
    }

    void Destory()
    {
        m_Tasks.Clear();
        m_nCount = 1;
    }


    public FWTask Create(IEnumerator coroutine, bool shouldStart = true)
    {
        GameObject task = new GameObject();
        task.transform.parent = FWTaskManager.Instance.gameObject.transform;
        task.name = "FWTask "+m_nCount;
        m_nCount++;

        FWTask a_kTask = new FWTask(coroutine, task.name, shouldStart);
        m_Tasks.Add(a_kTask);

        return a_kTask;
    }

    void Update()
    {
        for (int i = 0; i < m_Tasks.Count; i++)
        {
            if (m_Tasks[i].TaskFinished)
            {
                Stop(m_Tasks[i]);
            }
        }
    }

    public void AllStop()
    {
        for (int i = 0; i < m_Tasks.Count; i++)
        {
            Stop(m_Tasks[i]);
        }

        //m_Tasks.Clear();
    }

    public void AllPause()
    {
        for (int i = 0; i < m_Tasks.Count; i++)
        {
            Pause(m_Tasks[i]);
        }
    }

    public void AllUnPause()
    {
        for (int i = 0; i < m_Tasks.Count; i++)
        {
            UnPause(m_Tasks[i]);
        }
    }

    public void Start(FWTask kTask)
    {
        if (m_Tasks.Contains(kTask))
            kTask.Start();
    }

    public void Stop(FWTask kTask)
    {
        if (m_Tasks.Contains(kTask))
        {
            m_Tasks.Remove(kTask);
            kTask.Stop();

            GameObject a_kGameObject = FWHelper.Find(Instance.name);
            Transform transform = FWHelper.FindChild(a_kGameObject, kTask.TaskName);

            if (transform)
                Destroy(transform.gameObject);
        }
    }

    private IEnumerator StopDelay(FWTask kTask, float fDelayInSeconds)
    {
        yield return new WaitForSeconds(fDelayInSeconds);
        Stop(kTask);
    }

    public void Stop(FWTask kTask, float fDelayInSeconds)
    {
        if (m_Tasks.Contains(kTask))
        {
            StartCoroutine(StopDelay(kTask, fDelayInSeconds));
        }
    }

    public void Pause(FWTask kTask)
    {
        if (m_Tasks.Contains(kTask))
            kTask.Pause();
    }

    public void UnPause(FWTask kTask)
    {
        if (m_Tasks.Contains(kTask))
            kTask.UnPause();
    }
}

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
                //m_bFinished = true;
                //m_bRunning = false;
                //m_bPaused = false;
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

