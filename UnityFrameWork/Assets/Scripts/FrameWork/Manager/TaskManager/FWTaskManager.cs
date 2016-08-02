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

    public void AllStop()
    {
        for (int i = 0; i < m_Tasks.Count; i++)
        {
            Stop(m_Tasks[i]);
        }
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

            GameObject a_kGameObject = FWFinder.Find(Instance.name);
            Transform transform = FWFinder.FindChild(a_kGameObject, kTask.TaskName);

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


