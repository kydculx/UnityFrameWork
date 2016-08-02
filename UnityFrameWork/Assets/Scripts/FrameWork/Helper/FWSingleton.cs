using UnityEngine;

public class FWSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T m_sInstance;
    private static object m_sLock = new object();
    private static bool m_bApplicationIsQuitting = false;

    public static T Instance
    {
        get
        {
            if (m_bApplicationIsQuitting)
            {
                Debug.LogWarning("[Singleton] Instance '" + typeof(T) +
                    "' already destroyed on application quit." +
                    " Won't create again - returning null.");
                return null;
            }

            lock (m_sLock)
            {
                if (m_sInstance == null)
                {
                    m_sInstance = (T)FindObjectOfType(typeof(T));

                    if (FindObjectsOfType(typeof(T)).Length > 1)
                    {
                        Debug.LogError("[Singleton] Something went really wrong " +
                            " - there should never be more than 1 singleton!" +
                            " Reopening the scene might fix it.");
                        return m_sInstance;
                    }

                    if (m_sInstance == null)
                    {
                        GameObject singleton = new GameObject();
                        m_sInstance = singleton.AddComponent<T>();
                        singleton.name = "["+typeof(T).ToString()+"]";

                        DontDestroyOnLoad(singleton);

                        Debug.Log("[Singleton] An instance of " + typeof(T) +
                            " is needed in the scene, so '" + singleton +
                            "' was created with DontDestroyOnLoad.");
                    }
                    else
                    {
                        Debug.Log("[Singleton] Using instance already created: " +
                            m_sInstance.gameObject.name);
                    }
                }

                return m_sInstance;
            }
        }
    }

    public void OnDestroy()
    {
        m_bApplicationIsQuitting = true;
    }
}
