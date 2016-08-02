using UnityEngine;
using System.Collections;

public class FWApplicationMain : FWSingleton<FWApplicationMain>
{
    void Destroy()
    {
        Debug.Log("");
    }

    void SetUp()
    {
        SetFrame(60);
    }

    void SetFrame(int nFrame)
    {
        Application.targetFrameRate = nFrame;
    }

    void SetScreenOrientation(ScreenOrientation orientation)
    {
        Screen.orientation = orientation;
    }
}
