using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Hub : MonoBehaviour
{
    public void BackToHub() // function to restart the whole game
    {
        // reset all the stages back to 0
        GameManager.sceneCounter = 0;
        GameManager.receptionistStage = 0;
        GameManager.pharmacistStage = 0;
        GameManager.doctorStage = 0;
        GameManager.surgeonStage = 0;

        //if (ThemeSelectScreen.IsYJ == true)
        //{
        //    SceneManager.LoadScene(7); // load main scene
        //}
        //else if(ThemeSelectScreen.IsClassic == true)
        //{
        //    SceneManager.LoadScene(13);
        //}

        SceneManager.LoadScene(13);
    }

}
