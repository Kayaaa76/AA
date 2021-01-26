using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class testScene : MonoBehaviour
{
    static bool sceneChange = false;
    static bool ready = false;

    public static void LoadLastScene()
    {
        sceneChange = true;
    }

    void Update()
    {
        if (sceneChange == true)
        {
            StartCoroutine(PostGameLevelActivity());
            if (ready == true)
            {
                StartCoroutine(CallLoadLastScene());
            }
            else
            {
                return;
            }
        }
        else
        {
            return;
        }
    }

    IEnumerator PostGameLevelActivity()
    {
        sceneChange = false;
        WWWForm formPostGameLevelActivity = new WWWForm();
        WWW wwwPostGameLevelActivity = new WWW("http://103.239.222.212/ALIVE2Service/api/game/PostActivity?ActivityTypeName=" + "Game Level&" + "username=" + Login.tnameField.text + "&ActivityDataValue=" + "Game Level", formPostGameLevelActivity);
        yield return wwwPostGameLevelActivity;
        Debug.Log(wwwPostGameLevelActivity.text);
        Debug.Log(wwwPostGameLevelActivity.error);
        Debug.Log(wwwPostGameLevelActivity.url);
        yield return new WaitForSecondsRealtime(2);
        ready = true;
    }

    IEnumerator CallLoadLastScene()
    {
        yield return new WaitForSecondsRealtime(2);
        SceneManager.LoadScene(PlayerPrefs.GetString("Main"));
    }
}
