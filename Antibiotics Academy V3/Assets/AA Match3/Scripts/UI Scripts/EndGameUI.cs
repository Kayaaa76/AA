using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Match3
{
    public class EndGameUI : MonoBehaviour
    {
        GameObject StartUI;

        void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        
        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            StartUI = GameObject.Find("StartUI");
            //Debug.Log(StartUI);
            if (StartUI != null)
            {
                StartUI.SetActive(false);
            }
            //Debug.Log("OnSceneLoaded: " + scene.name);
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

            SceneManager.LoadScene(14); //match 3 scene
        }

        public void TriggerQuit() //function to quit the game
        {
            GameManager.pharmacistStage = 2;

            //GameManager.receptionistStage = 2;
            if(GameManager.receptionistStage != 3)
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

            SceneManager.LoadScene(13); //main scene

            //Player.coins += 50;
            //Debug.Log("You got 50 coins for winning this game!");
        }

        public void TriggerQuitLost()  //function to quit the game when player lost
        {
            SceneManager.LoadScene(10); //death
        }
    }
}
