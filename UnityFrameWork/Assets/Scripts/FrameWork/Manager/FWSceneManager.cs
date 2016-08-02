using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class FWSceneManager : FWSingleton<FWSceneManager> {

    string m_strSceneName = null;
    int m_nLevelIndex = 0;

    public void SetScene(string strSceneName)
    {
        m_strSceneName = strSceneName;
        SceneManager.LoadScene(m_strSceneName, LoadSceneMode.Single);
    }

    public Scene GetScene()
    {
        return SceneManager.GetSceneByName(m_strSceneName);
    }

    public string GetSceneName()
    {
        return m_strSceneName;
    }
}