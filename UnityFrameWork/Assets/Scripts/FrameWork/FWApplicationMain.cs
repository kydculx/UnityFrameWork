using UnityEngine;
using System.Collections;

public class FWApplicationMain : FWSingleton<FWApplicationMain>
{
    private static bool             m_bApplicationRun       = false;

    public FWSceneManager           m_kSceneManager         = null;
    public FWTouchEventManager      m_kTouchEventManager    = null;
    public FWEventHandlerManager    m_kEventHandlerManager  = null;
    public FWTaskManager            m_kTaskManager          = null;
    public FWSoundManager           m_kSoundManager         = null;

    void Awake()
    { 
        if (m_bApplicationRun == false)
        {
            Debug.Log("FWApplicationMain Awake");
            m_bApplicationRun = true;
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

        m_kSoundManager.PlayBGM("bgm_battle");
    }

    void Destroy()
    {
        m_bApplicationRun = false;
        Debug.Log("FWApplicationMain Destroy");
    }

    void ApplicationSetting()
    {
        FWSetting.SetFrameRate(60);
        FWSetting.SetNeverSleep(true);
        FWSetting.SetScreenOrientation(ScreenOrientation.LandscapeLeft);
    }
}
