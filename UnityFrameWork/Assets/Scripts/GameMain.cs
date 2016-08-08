using UnityEngine;
using System.Collections;
using System;


public class EventHandlerTest : EventArgs
{
    public int age {get; set; }
    public string name {get; set; }

    public EventHandlerTest(int age, string name)
    {
        this.age = age;
        this.name = name;
    }

    public void Click(object sender, EventArgs args, EventHandlerTest data)
    {
        Debug.Log("Click = "+data.age +"/"+data.name );
    }
}

public class GameMain : MonoBehaviour
{
    private FWTask m_kTask;

    EventHandler m_EventHandler;

    void Start ()
    {
	}
	
	void Update ()
    {
	}

    IEnumerator writeLog(int totalTimes)
    {
        var i = 0;
        var wfs = new WaitForSeconds(1);

        while (i <= totalTimes)
        {
            Debug.Log(string.Format("writing log {0} of {1}", i, totalTimes));
            i++;
            yield return wfs;
        }
    }

    IEnumerator endlessLog()
    {
        var i = 0;
        var wfs = new WaitForSeconds(1);

        while (true)
        {
            Debug.Log(string.Format("writing endless log: " + i));
            i++;
            yield return wfs;
        }
    }

    IEnumerator printAfterDelay(string text, float delay)
    {
        yield return new WaitForSeconds(delay);
        Debug.Log("printAfterDelay: " + text);
    }


    void TaskComplete(bool bFlag)
    {
        Debug.Log("task done. was it killed? " + bFlag);
    }

    void TestTask()
    {
        var buttonHeight = 60;
        if (GUI.Button(new Rect(5, 5, 200, buttonHeight), "Make and Start 5 iteration task"))
        {
            FWTask a_kTask = FWTaskManager.Instance.Create(writeLog(5));
            a_kTask.Finished += TaskComplete;
        }

        if (GUI.Button(new Rect(5, 100, 200, buttonHeight), "Make Paused"))
        {
            m_kTask = FWTaskManager.Instance.Create(endlessLog(), false);
            m_kTask.Finished += TaskComplete;
        }

        if (GUI.Button(new Rect(5, 100 + buttonHeight, 200, buttonHeight), "Start Made Task"))
        {
            FWTaskManager.Instance.Start(m_kTask);
        }

        if (GUI.Button(new Rect(5, 100 + buttonHeight * 2, 200, buttonHeight), "Pause"))
        {
            FWTaskManager.Instance.Pause(m_kTask);
        }
        if (GUI.Button(new Rect(5, 100 + buttonHeight * 3, 200, buttonHeight), "UnPause"))
        {
            FWTaskManager.Instance.UnPause(m_kTask);
        }
        if (GUI.Button(new Rect(5, 100 + buttonHeight * 4, 200, buttonHeight), "Stop Immediately"))
        {
            FWTaskManager.Instance.Stop(m_kTask);
        }

        if (GUI.Button(new Rect(5, 100 + buttonHeight * 5, 200, buttonHeight), "Stop After 3sec Delay"))
        {
            FWTaskManager.Instance.Stop(m_kTask, 3);
        }

        var xPos = Screen.width - 205;
        if (GUI.Button(new Rect(xPos, 5, 200, buttonHeight), "All Stop"))
        {
            FWTaskManager.Instance.AllStop();
        }

        if (GUI.Button(new Rect(xPos, 5 + buttonHeight, 200, buttonHeight), "All Pause"))
        {
            FWTaskManager.Instance.AllPause();
        }

        if (GUI.Button(new Rect(xPos, 5 + buttonHeight * 2, 200, buttonHeight), "All UnPause"))
        {
            FWTaskManager.Instance.AllUnPause();
        }
    }

    void testScene()
    {
        var buttonHeight = 60;

        if (GUI.Button(new Rect(5, 5, 200, buttonHeight), "Go GamePlay"))
        {
            FWSceneManager.Instance.SetScene(FWSceneList.GAME_PLAY);
        }
    }

    void TestEventHandler()
    {
        var buttonHeight = 60;

        if (GUI.Button(new Rect(5, 5, 200, buttonHeight), "EventHandler Regeister"))
        {

            EventHandlerTest args = new EventHandlerTest(UnityEngine.Random.Range(1, 100), "kydculx");

            m_EventHandler = new EventHandler((object sender, EventArgs e) => args.Click(sender, e, args));

            FWEventHandlerManager.Instance.Add("test", m_EventHandler);
        }

        if (GUI.Button(new Rect(5, 5 + buttonHeight, 200, buttonHeight), "EventHandler Execute"))
        {
            FWEventHandlerManager.Instance.Excute("test");
        }

        if (GUI.Button(new Rect(5, 5 + buttonHeight * 2, 200, buttonHeight), "EventHandler Remove target"))
        {
            FWEventHandlerManager.Instance.Remove("test", m_EventHandler);
        }

        if (GUI.Button(new Rect(5, 5 + buttonHeight * 3, 200, buttonHeight), "EventHandler Remove key"))
        {
            FWEventHandlerManager.Instance.Remove("test");
        }

        if (GUI.Button(new Rect(5, 5 + buttonHeight * 4, 200, buttonHeight), "EventHandler Clean"))
        {

            FWEventHandlerManager.Instance.Clean();
        }
    }

    void OnGUI()
    {
        TestEventHandler();
    }
}
