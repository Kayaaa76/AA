using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Match3
{
    public class EndGameUI : MonoBehaviour
    {
        static bool sceneChange;
        static bool ready;

        bool restarted;

        GameObject StartUI;

        void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (restarted == true)
            {
                StartUI = GameObject.Find("StartUI");
                //Debug.Log(StartUI);
                if (StartUI != null)
                {
                    StartUI.SetActive(false);
                    restarted = false;
                }
                //Debug.Log("OnSceneLoaded: " + scene.name);
            }
        }

        public void TriggerRestart()  //function to restart the game
        {
            //if (ThemeSelectScreen.IsClassic == true)
            //{
            //    SceneManager.LoadScene(14); // match 3
            //}
            //else if(ThemeSelectScreen.IsYJ == true)
            //{
            //    SceneManager.LoadScene(8);
            //}
            //else
            //{
            //    SceneManager.LoadScene(14);
            //}

            restarted = true;

            SceneManager.LoadScene(14); //match 3 scene
        }

        public void TriggerQuit() //function to quit the game
        {
            GameManager.pharmacistStage = 2;

            //GameManager.receptionistStage = 2;
            if (GameManager.receptionistStage != 3)
            {
                GameManager.receptionistStage = 2;
            }

            //if (ThemeSelectScreen.IsClassic == true)
            //{
            //    SceneManager.LoadScene(13); //main
            //}
            //else if (ThemeSelectScreen.IsYJ == true)
            //{
            //    SceneManager.LoadScene(7);
            //}
            //else if(ThemeSelectScreen.IsTrixy == true)
            //{
            //    SceneManager.LoadScene(16);
            //}
            //else
            //{
            //    SceneManager.LoadScene(13);
            //}

            sceneChange = true;
          
            //Player.coins += 50;
            //Debug.Log("You got 50 coins for winning this game!");
        }

        public void TriggerQuitLost()  //function to quit the game when player lost
        {
            SceneManager.LoadScene(10); //death
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
            SceneManager.LoadScene(13); //main scene
        }

        void Update()
        {
            if (sceneChange == true)
            {
                StartCoroutine(PostGameLevelActivity());
            }
        }
    }
}
