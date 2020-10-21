using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Match3
{
    public class EndGameUI : MonoBehaviour
    {
        public void TriggerRestart()  //function to restart the game
        {
            if (ThemeSelectScreen.IsClassic == true)
            {
                SceneManager.LoadScene(14); // match 3
            }
            else if(ThemeSelectScreen.IsYJ == true)
            {
                SceneManager.LoadScene(8);
            }
        }

        public void TriggerQuit() //function to quit the game
        {
            GameManager.pharmacistStage = 2;
            GameManager.receptionistStage = 2;

            if (ThemeSelectScreen.IsClassic == true)
            {
                SceneManager.LoadScene(13); //main
            }
            else if (ThemeSelectScreen.IsYJ == true)
            {
                SceneManager.LoadScene(7);
            }
            else if(ThemeSelectScreen.IsTrixy == true)
            {
                SceneManager.LoadScene(16);
            }
        }

        public void TriggerQuitLost()  //function to quit the game when player lost
        {
            SceneManager.LoadScene(10); //death
        }
    }
}
