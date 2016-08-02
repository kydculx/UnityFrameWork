using UnityEngine;
using System.Collections;

public class FWLoader : MonoBehaviour
{
    public static GameObject PrefabLoad(string strPrefabName)
    {
        GameObject a_kObject = null;
        a_kObject = GameObject.Instantiate(Resources.Load(strPrefabName, typeof(GameObject)), Vector3.zero, Quaternion.identity) as GameObject;
        Debug.Assert(a_kObject, "(Prefab Not Found " + strPrefabName + ")");

        return a_kObject;
    }
}
