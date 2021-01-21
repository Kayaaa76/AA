using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class testScene1 : MonoBehaviour
{
    GameObject player; // get player game object
    bool lifeDeducted = false;

    public void Start()
    {
        player = GameObject.Find("Player"); // find the player game object
    }

    void Update()
    {
        StartCoroutine(PostLifeActivity());
    }

    IEnumerator PostLifeActivity()
    {
        if(lifeDeducted == true)
        {
            WWWForm formPostLifeActivity = new WWWForm();
            WWW wwwPostLifeActivity = new WWW("http://103.239.222.212/ALIVE2Service/api/game/PostActivity?ActivityTypeName=" + "Player Lifes&" + "username=" + Login.tnameField.text + "&ActivityDataValue=" + "Player Lifes", formPostLifeActivity);
            yield return wwwPostLifeActivity;
            Debug.Log(wwwPostLifeActivity.text);
            Debug.Log(wwwPostLifeActivity.error);
            Debug.Log(wwwPostLifeActivity.url);
            lifeDeducted = false;

            WWWForm formPostGameLevelActivity = new WWWForm();
            WWW wwwPostGameLevelActivity = new WWW("http://103.239.222.212/ALIVE2Service/api/game/PostActivity?ActivityTypeName=" + "Game Level&" + "username=" + Login.tnameField.text + "&ActivityDataValue=" + "Game Level", formPostGameLevelActivity);
            yield return wwwPostGameLevelActivity;
            Debug.Log(wwwPostGameLevelActivity.text);
            Debug.Log(wwwPostGameLevelActivity.error);
            Debug.Log(wwwPostGameLevelActivity.url);
        }
    }

    public void Load() // function to load the match 3 mini game
    {
        if (Player.lives > 0)
        {
            lifeDeducted = true;
            if (ThemeSelectScreen.IsYJ == true)
            {
                Player.lives -= 1;

                SceneManager.LoadScene(8); // match 3
            }
            else if (ThemeSelectScreen.IsClassic == true || ThemeSelectScreen.IsTrixy == true)
            {
                Player.lives -= 1;

                SceneManager.LoadScene(14);
            }
            else
            {
                Player.lives -= 1;

                SceneManager.LoadScene(14);
            }
        }
        else
        {
            Debug.Log("You do not have enough lives to play the game!");
        }
    }
}
