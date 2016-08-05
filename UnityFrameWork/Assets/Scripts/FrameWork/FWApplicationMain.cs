using UnityEngine;
using System.Collections;

public class FWApplicationMain : FWSingleton<FWApplicationMain>
{
    private static bool m_bApplicationRun = false;

    void Awake()
    {
        if (m_bApplicationRun == false)
        {
            Debug.Log("FWApplicationMain Awake");
            m_bApplicationRun = true;
            ApplicationSetting();
        }
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
