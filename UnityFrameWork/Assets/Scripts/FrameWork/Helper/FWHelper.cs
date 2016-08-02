using UnityEngine;
using System.Collections;

public class FWHelper : MonoBehaviour
{
    public static float GetValue(float fMaxValue, float fPercent)
    {
        return fMaxValue * fPercent / 100.0f;
    }

    public static float GetPercent(float fCurrnetValue, float fMaxValue)
    {
        return ((fCurrnetValue) / (fMaxValue) * 100.0f);
    }

    public static void SetFrame(int nFrame)
    {
        Application.targetFrameRate = nFrame;
    }

    public static void SetScreenOrientation(ScreenOrientation orientation)
    {
        Screen.orientation = orientation;
    }
}
