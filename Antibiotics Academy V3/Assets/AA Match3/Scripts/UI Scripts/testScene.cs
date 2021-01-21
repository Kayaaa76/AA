using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class testScene : MonoBehaviour
{
    static bool sceneChange = false;

    public static void LoadLastScene()
    {
        sceneChange = true;
        SceneManager.LoadScene(PlayerPrefs.GetString("Main"));
    }

    void Update()
    {
        StartCoroutine(PostGameLevelActivity());
    }

    IEnumerator PostGameLevelActivity()
    {
        if (sceneChange == true)
        {
            WWWForm formPostGameLevelActivity = new WWWForm();
            WWW wwwPostGameLevelActivity = new WWW("http://103.239.222.212/ALIVE2Service/api/game/PostActivity?ActivityTypeName=" + "Game Level&" + "username=" + Login.tnameField.text + "&ActivityDataValue=" + "Game Level", formPostGameLevelActivity);
            yield return wwwPostGameLevelActivity;
            Debug.Log(wwwPostGameLevelActivity.text);
            Debug.Log(wwwPostGameLevelActivity.error);
            Debug.Log(wwwPostGameLevelActivity.url);
            sceneChange = false;
        }
    }
}
