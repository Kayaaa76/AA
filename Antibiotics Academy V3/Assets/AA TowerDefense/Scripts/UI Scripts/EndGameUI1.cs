using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TowerDefense
{
    public class EndGameUI1 : MonoBehaviour
    {
        static bool coinsChange;
        static bool sceneChange = false;

        static bool next;
        bool levels;
        static bool restarted;

        GameObject StartUI;
        GameObject LevelSelectUI;

        GameManagerBehavior gMB;

        void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (levels == true) //to display levels
            {
                //disable start menu
                StartUI = GameObject.Find("StartUI");
                if (StartUI != null)
                {
                    StartUI.SetActive(false);
                }

                //levels = false;
            }

            if(next == true || restarted == true) //to directly display game (when starting next level or restarting)
            {
                //disable start menu
                StartUI = GameObject.Find("StartUI");
                if (StartUI != null)
                {
                    StartUI.SetActive(false);
                }

                //disable level select menu
                LevelSelectUI = GameObject.Find("Tower Defense Level Select");
                if (LevelSelectUI != null)
                {
                    LevelSelectUI.SetActive(false);
                }
            }
        }

        void Start()
        {
            gMB = GameObject.Find("GameManager").GetComponent<GameManagerBehavior>();
            
            if(next == true) //if starting next level
            {
                TDLevelSelect.levelDifficulty += 1; //increase level by 1
                gMB.Wave = TDLevelSelect.levelDifficulty; //set as increased level
                next = false;
                Time.timeScale = 1f;
            }
            
            if(restarted == true) //if restarting level
            {
                gMB.Wave = TDLevelSelect.levelDifficulty; //set as original level
                restarted = false;
                Time.timeScale = 1f;
            }
        }

        public void TriggerNext()
        {
            next = true;
            SceneManager.LoadScene(9); // restart tower defense game
        }

        public void TriggerLevels()
        {
            levels = true;
            SceneManager.LoadScene(9); // restart tower defense game
        }

        public void TriggerQuit() // win tower defense
        {
            Player.Save();
            StartCoroutine(Login.UpdateCoins());
            StartCoroutine(Login.UpdateLives());

            if (Player.tdunlockedlevels < 2) //when players have not won level 1 of tower defense
            {
                GameManager.surgeonStage = 0; //reset stage progress
            }
            else if (GameManager.npclawyerStage == 0) //update stage progress (one-time trigger upon leaving minigame for the first time)
            {
                GameManager.surgeonStage = 3;
                GameManager.npclawyerStage = 1;
            }

            SceneManager.LoadScene(13); // back to main

            //if (ThemeSelectScreen.IsYJ == true)
            //{
            //    SceneManager.LoadScene(7); // go back to hospital
            //}
            //else if (ThemeSelectScreen.IsClassic == true)
            //{
            //    SceneManager.LoadScene(13); // go back to hospital
            //}
            //else if (ThemeSelectScreen.IsTrixy == true)
            //{
            //    SceneManager.LoadScene(16); // go back to hospital
            //}
            //else
            //{
            //    SceneManager.LoadScene(13);
            //}
            //coinsChange = true;
            //Player.coins += 50;
            //Debug.Log("You got 50 coins for winning this game!");
            //StartCoroutine(Login.UpdateCoins());
            Player.Save();
        }

        public void TriggerRestart()
        {
            restarted = true;
            SceneManager.LoadScene(9); // restart tower defense game
        }

        IEnumerator PostCoinAcitivty()
        {
            coinsChange = false;

            WWWForm formPostCoinActivity = new WWWForm();
            WWW wwwPostCoinActivity = new WWW("http://103.239.222.212/ALIVE2Service/api/game/PostActivity?ActivityTypeName=" + "Player Coins&" + "username=" + Login.tnameField.text + "&ActivityDataValue=" + "Player Coins", formPostCoinActivity);
            //WWW wwwPostCoinActivity = new WWW("http://103.239.222.212/ALIVE2Service/api/game/PostActivity?ActivityTypeName=Player Coins&username=player1&ActivityDataValue=Player Coins", formPostCoinActivity);
            yield return wwwPostCoinActivity;
            Debug.Log(wwwPostCoinActivity.text);
            Debug.Log(wwwPostCoinActivity.error);
            Debug.Log(wwwPostCoinActivity.url);
            sceneChange = true;

        }

        IEnumerator PostGameLevelActivity()
        {
            sceneChange = false;

            WWWForm formPostGameLevelActivity = new WWWForm();
            WWW wwwPostGameLevelActivity = new WWW("http://103.239.222.212/ALIVE2Service/api/game/PostActivity?ActivityTypeName=" + "Game Level&" + "username=" + Login.tnameField.text + "&ActivityDataValue=" + "Game Level", formPostGameLevelActivity);
            //WWW wwwPostGameLevelActivity = new WWW("http://103.239.222.212/ALIVE2Service/api/game/PostActivity?ActivityTypeName=Game Level&username=player1&ActivityDataValue=Game Level", formPostGameLevelActivity);

            yield return wwwPostGameLevelActivity;
            Debug.Log(wwwPostGameLevelActivity.text);
            Debug.Log(wwwPostGameLevelActivity.error);
            Debug.Log(wwwPostGameLevelActivity.url);
            SceneManager.LoadScene(13); //main scene

        }

        void Update()
        {
            if (coinsChange == true)
            {
                StartCoroutine(PostCoinAcitivty());
            }
            if (sceneChange == true)
            {
                StartCoroutine(PostGameLevelActivity());
            }
        }
    }
}
