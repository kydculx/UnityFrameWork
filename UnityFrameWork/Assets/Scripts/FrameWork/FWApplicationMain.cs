using UnityEngine;
using System.Collections;

public class FWApplicationMain : FWSingleton<FWApplicationMain>
{
    private static bool             m_sbApplicationRun      = false;

    public FWSceneManager           m_kSceneManager         = null;
    public FWTouchEventManager      m_kTouchEventManager    = null;
    public FWEventHandlerManager    m_kEventHandlerManager  = null;
    public FWTaskManager            m_kTaskManager          = null;
    public FWSoundManager           m_kSoundManager         = null;

    void Awake()
    {
        if (m_sbApplicationRun == false)
        {
            Debug.Log("FWApplicationMain Awake");
            m_sbApplicationRun = true;
            ApplicationSetting();
            Init();
        }
    }

    void Init()
    {
        m_kSceneManager         = FWSceneManager.Instance;
        m_kTouchEventManager    = FWTouchEventManager.Instance;
        m_kEventHandlerManager  = FWEventHandlerManager.Instance;
        m_kTaskManager          = FWTaskManager.Instance;
        m_kSoundManager         = FWSoundManager.Instance;

        FWObjectPoolManager.Instance.Create("HP", "Prefabs/Player_HP", 10);
    }

    void Destroy()
    {
        m_sbApplicationRun = false;
        Debug.Log("FWApplicationMain Destroy");
    }

    void ApplicationSetting()
    {
        FWSetting.SetFrameRate(60);
        FWSetting.SetNeverSleep(true);
        FWSetting.SetScreenOrientation(ScreenOrientation.LandscapeLeft);
    }
}