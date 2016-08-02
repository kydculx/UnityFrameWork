using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FWObjectPoolManager : FWSingleton<FWObjectPoolManager>
{
    public Hashtable m_kObjectPools = new Hashtable();

    public void Create<T>(string strResourcePath, int nSize)
    {
        Stack<GameObject> a_kGameObjectList = new Stack<GameObject>(nSize);

        for (int nCount = 0; nCount < nSize; nCount++)
        {
            GameObject a_kGameObject = FWLoader.PrefabLoad(strResourcePath);
            a_kGameObject.SetActive(false);
            a_kGameObjectList.Push(a_kGameObject);
        }

        m_kObjectPools.Add(typeof(T).ToString(), a_kGameObjectList);
    }

    public GameObject Pop<T>()
    {
        Stack<GameObject> a_kObjectList = (Stack<GameObject>)m_kObjectPools[typeof(T).ToString()];

        GameObject a_kGameObject = a_kObjectList.Pop();
        Debug.Assert(a_kGameObject);

        a_kGameObject.SetActive(true);

        return a_kGameObject;
    }

    public void Push<T>(GameObject kGameObject)
    {
        Stack<GameObject> a_kObjectList = (Stack<GameObject>)m_kObjectPools[typeof(T).ToString()];

        if (a_kObjectList != null)
        {
            kGameObject.SetActive(false);
            a_kObjectList.Push(kGameObject);
        }
    }

    public void Remove<T>()
    {
        Stack<GameObject> a_kObjectList = (Stack<GameObject>)m_kObjectPools[typeof(T).ToString()];
        if (a_kObjectList != null)
        {
            a_kObjectList.Clear();
            m_kObjectPools.Remove(typeof(T).ToString());
        }
    }
}