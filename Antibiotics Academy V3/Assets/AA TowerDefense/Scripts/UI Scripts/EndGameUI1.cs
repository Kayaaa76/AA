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

        public void TriggerRestart()
        {
            SceneManager.LoadScene(9); // restart tower defense game
        }

        public void TriggerQuit() // win tower defense
        {
            // back to hub
            GameManager.surgeonStage = 3;
            GameManager.npclawyerStage = 1;

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
            coinsChange = true;


            Player.coins += 50;
            Debug.Log("You got 50 coins for winning this game!");
        }

        public void TriggerQuitLost()
        {
            if (ThemeSelectScreen.IsYJ == true)
            {
                SceneManager.LoadScene(7); // back to main
            }
            else if (ThemeSelectScreen.IsClassic == true)
            {
                SceneManager.LoadScene(13); // back to main
            }
            else if (ThemeSelectScreen.IsTrixy == true)
            {
                SceneManager.LoadScene(16); // back to main
            }
            GameManager.surgeonStage = 0;
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
