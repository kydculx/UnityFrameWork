using UnityEngine;
using System.Collections;

public class FWApplicationMain : FWSingleton<FWApplicationMain>
{
    private static bool m_bApplicationRun = false;

    void Awake()
    {
        if (m_bApplicationRun == false)
        {
            m_bApplicationRun = true;
            Debug.Log("FWApplicationMain Awake");

            FWHelper.SetFrame(1/60);
            FWHelper.SetScreenOrientation(ScreenOrientation.LandscapeLeft);
        }
    }

    void Start()
    {
        Debug.Log("FWApplicationMain Start");
    }

    void Destroy()
    {
        Debug.Log("FWApplicationMain Destroy");
    }
}
