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

        static bool next;
        bool levels;
        static bool restarted;

        GameObject StartUI;
        GameObject LevelSelectUI;

        Criteria criteria;

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

            if (next == true || restarted == true) //to directly display game (when starting next level or restarting)
            {
                //disable start menu
                StartUI = GameObject.Find("StartUI");
                if (StartUI != null)
                {
                    StartUI.SetActive(false);
                }

                //disable level select menu
                LevelSelectUI = GameObject.Find("Match 3 Level Select");
                if (LevelSelectUI != null)
                {
                    LevelSelectUI.SetActive(false);
                }
                //Debug.Log("OnSceneLoaded: " + scene.name);
            }
        }

        void Start()
        {
            criteria = GameObject.Find("Criteria").GetComponent<Criteria>();

            if (next == true) //if starting next level
            {
                M3LevelSelect.levelDifficulty += 1; //increase level by 1
                criteria.currentLevel = M3LevelSelect.levelDifficulty; //set as increased level

                criteria.moveCounter = criteria.levels[criteria.currentLevel].limitMoves; //set move counter as number of moves given
                criteria.DisplayCritera(); //show tile criteria of level

                next = false;
                Time.timeScale = 1f;
            }

            if (restarted == true) //if restarting level
            {
                criteria.currentLevel = M3LevelSelect.levelDifficulty; //set as original level
                restarted = false;
                Time.timeScale = 1f;
            }
        }

        public void TriggerNext() // function to go to next level
        {
            next = true;
            SceneManager.LoadScene(14); // macth 3 scene
        }

        public void TriggerLevels() // function to go to levels menu
        {
            levels = true;
            SceneManager.LoadScene(14); // match 3 scene
        }

        public void TriggerRestart()  //function to restart match 3
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

        public void TriggerQuit() //function to quit match 3
        {
            if (Player.m3unlockedlevels < 2) //when players have not won level 1 of match 3
            {
                GameManager.surgeonStage = 0; //reset stage progress
            }
            else if (GameManager.receptionistStage == 1) //update stage progress (one-time trigger upon leaving minigame for the first time)
            {
                GameManager.receptionistStage = 2;
                GameManager.pharmacistStage = 2;
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
