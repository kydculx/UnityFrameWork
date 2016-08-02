using UnityEngine;
using System.Collections;

public class GamePlay : MonoBehaviour
{
    void Start()
    {
        for (int i = 0; i < 1000000; i++)
        {
            GameObject a_kObject = new GameObject();

            a_kObject.name = "" + i;
            a_kObject.transform.parent = gameObject.transform;
        }
    }

    void Update()
    {

    }

    void OnGUI()
    {
        var buttonHeight = 60;

        if (GUI.Button(new Rect(5, 5, 200, buttonHeight), "Go GameMain"))
        {
            FWSceneManager.Instance.SetScene(FWSceneList.GAME_MAIN);
        }
    }
}
