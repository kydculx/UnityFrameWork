using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class FWSceneManager : FWSingleton<FWSceneManager>
{
    private string m_strSceneName = "";
    public string SceneName
    {
        get
        {
            return m_strSceneName;
        }
    }

    AsyncOperation m_kSync = null;
    float m_fProgress;
    bool m_bLoading = false;

    public void SetScene(string strSceneName, bool bHeavy = false)
    {
        StopAllCoroutines();

        m_strSceneName = strSceneName;
        m_bLoading = true;

        if (bHeavy)
        {
            m_kSync = SceneManager.LoadSceneAsync(strSceneName);
            m_kSync.allowSceneActivation = false;

            //StartCoroutine(Loading(m_strSceneName));
        }
        else
        {
            SceneManager.LoadScene(m_strSceneName);
        }
    }

    public Scene GetScene()
    {
        return SceneManager.GetSceneByName(m_strSceneName);
    }

    IEnumerator Loading(string strSceneName)
    {
        while (!m_kSync.isDone)
        {
            // [0, 0.9] > [0, 1]
            m_fProgress = Mathf.Clamp01(m_kSync.progress / 0.9f);
            Debug.Log("Loading progress: " + (m_fProgress * 100) + "%");

            // Loading completed
            if (m_kSync.progress == 0.9f)
            {
                Debug.Log("Loading completed");
                m_kSync.allowSceneActivation = true;

                m_bLoading = false;
            }
        }

        yield return null;
    }


    void Update()
    {
        //if (m_bLoading)
        //{
        //    if (!m_kSync.isDone)
        //    {
        //        // [0, 0.9] > [0, 1]
        //        m_fProgress = Mathf.Clamp01(m_kSync.progress / 0.9f);
        //        Debug.Log("Loading progress: " + (m_fProgress * 100) + "%");


        //        // Loading completed
        //        if (m_kSync.progress == 0.9f)
        //        {
        //            Debug.Log("Loading completed");
        //            m_kSync.allowSceneActivation = true;
        //            m_bLoading = false;
        //        }
        //    }
        //}
    }
}