using UnityEngine;
using System.Collections;

public class FWFinder : MonoBehaviour
{
    public static GameObject Find(string strObjectName)
    {
        GameObject a_kObject = null;
        a_kObject = GameObject.Find(strObjectName);
        Debug.Assert(a_kObject, "Object Not Found" + " (" + strObjectName + ")");

        return a_kObject;
    }

    public static Transform FindChild(GameObject kParentObject, string strObjectName)
    {
        Transform a_kTransform = null;
        a_kTransform = kParentObject.transform.Find(strObjectName);
        Debug.Assert(a_kTransform, "Object Not Found" + " (" + strObjectName + ")");

        return a_kTransform;
    }

    public static Transform FindChild(Transform kParentTransform, string strObjectName)
    {
        Transform a_kTransform = null;
        a_kTransform = kParentTransform.Find(strObjectName);
        Debug.Assert(a_kTransform, "Object Not Found" + " (" + strObjectName + ")");

        return a_kTransform;
    }
}
