using UnityEngine;
using System.Collections;

public class FWHelper : MonoBehaviour
{
    public static GameObject ObjectFind(string strObjectName)
    {
        GameObject a_kObject = null;
        a_kObject = GameObject.Find(strObjectName);
        Debug.Assert(a_kObject, "(Object Not Found " + strObjectName +")");

        return a_kObject;
    }

    public static GameObject Find(string strObjectName)
    {
        GameObject a_kObject = null;
        a_kObject = GameObject.Find(strObjectName);
        Debug.Assert(a_kObject, "(Object Not Found " + strObjectName +")");

        return a_kObject;
    }

    public static Transform FindChild(GameObject kParentObject, string strObjectName)
    {
        Transform a_kTransform = null;
        a_kTransform = kParentObject.transform.Find(strObjectName);
        Debug.Assert(a_kTransform, "(Object Not Found " + strObjectName + ")");

        return a_kTransform;
    }

    public static Transform FindChild(Transform kParentTransform, string strObjectName)
    {
        Transform a_kTransform = null;
        a_kTransform = kParentTransform.Find(strObjectName);
        Debug.Assert(a_kTransform, "(Object Not Found " + strObjectName + ")");

        return a_kTransform;
    }

    public static GameObject PrefabLoad(string strPrefabName)
    {
        GameObject a_kObject = null;
        a_kObject = GameObject.Instantiate(Resources.Load(strPrefabName, typeof(GameObject)), Vector3.zero, Quaternion.identity) as GameObject;
        Debug.Assert(a_kObject, "(Prefab Not Found " + strPrefabName + ")");

        return a_kObject;
    }

    public static float GetValue(float fMaxValue, float fPercent)
    {
        return fMaxValue * fPercent / 100;
    }

    public static float GetPercent(float fCurrnetValue, float fMaxValue)
    {
        return ((fCurrnetValue) / (fMaxValue) * 100.0f);
    }
}
