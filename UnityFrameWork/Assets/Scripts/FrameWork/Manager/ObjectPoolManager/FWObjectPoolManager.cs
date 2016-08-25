using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FWObjectPoolManager : FWSingleton<FWObjectPoolManager>
{
    public Hashtable m_kObjectPools = new Hashtable();

    public void Create(string strObjectName, string strResourcePath, int nSize)
    {
        Stack<GameObject> a_kGameObjectList = new Stack<GameObject>(nSize);

        GameObject a_kObject = new GameObject();
        a_kObject.transform.parent = FWObjectPoolManager.Instance.gameObject.transform;
        a_kObject.name = ">"+strObjectName;

        for (int nCount = 0; nCount < nSize; nCount++)
        {
            GameObject a_kGameObject = FWLoader.PrefabLoad(strResourcePath);
            a_kGameObject.name = strObjectName + "(Clone)";
            a_kGameObject.SetActive(false);
            a_kGameObject.transform.parent = a_kObject.transform;
            a_kGameObjectList.Push(a_kGameObject);
        }

        m_kObjectPools.Add(strObjectName, a_kGameObjectList);
    }

    public GameObject Pop(string strObjectName)
    {
        Stack<GameObject> a_kObjectList = (Stack<GameObject>)m_kObjectPools[strObjectName];

        GameObject a_kGameObject = a_kObjectList.Pop();
        Debug.Assert(a_kGameObject);

        a_kGameObject.SetActive(true);

        return a_kGameObject;
    }

    public void Push(string strObjectName, GameObject kGameObject)
    {
        Stack<GameObject> a_kObjectList = (Stack<GameObject>)m_kObjectPools[strObjectName];

        if (a_kObjectList != null)
        {
            GameObject a_kObject = FWFinder.Find("["+typeof(FWObjectPoolManager).ToString()+"]");
            Transform a_kTransform = FWFinder.FindChild(a_kObject, ">"+kGameObject.name.Remove(kGameObject.name.Length - 7));
            kGameObject.transform.parent = a_kTransform;

            kGameObject.SetActive(false);
            a_kObjectList.Push(kGameObject);
        }
    }

    public void Remove(string strObjectName)
    {
        Stack<GameObject> a_kObjectList = (Stack<GameObject>)m_kObjectPools[strObjectName];

        if (a_kObjectList != null)
        {
            bool a_bRootRemove = false;

            foreach(var obj in a_kObjectList)
            {
                if (a_bRootRemove == false)
                {
                    GameObject a_kObject = FWFinder.Find("[" + typeof(FWObjectPoolManager).ToString() + "]");
                    Transform a_kTransform = FWFinder.FindChild(a_kObject, ">" + obj.name.Remove(obj.name.Length - 7));
                    Destroy(a_kTransform.gameObject);

                    a_bRootRemove = true;
                }

                obj.SetActive(false);
                Destroy(obj);
            }

            a_kObjectList.Clear();
            m_kObjectPools.Remove(strObjectName);
        }
    }
}