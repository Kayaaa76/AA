using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Match3
{
    public class StartTrigger : MonoBehaviour
    {
        public GameObject StartUI;
        public GameObject LevelsUI;

        public void TriggerStart()    //function to start the game
        {
            //Time.timeScale = 1f;
            StartUI.SetActive(false);
            LevelsUI.SetActive(true);
        }
    }
}
