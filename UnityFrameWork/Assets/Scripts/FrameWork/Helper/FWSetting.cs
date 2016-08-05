using UnityEngine;

public class FWSetting : MonoBehaviour
{
    public static void SetFrameRate(int nFrame)
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = nFrame;
    }

    public static void SetScreenOrientation(ScreenOrientation orientation)
    {
        Screen.orientation = orientation;
    }

    public static void SetNeverSleep(bool bSleep)
    {
        if (bSleep)
        {
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
        }
    }
}
