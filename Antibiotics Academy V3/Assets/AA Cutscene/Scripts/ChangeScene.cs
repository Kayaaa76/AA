using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void CutScene2()
    {
        SceneManager.LoadScene(3); // Change to second cutscene
    }

    public void CutScene3()
    {
        SceneManager.LoadScene(4); // Change to third cutscene
    }

    public void CutScene4()
    {
        SceneManager.LoadScene(5); // Change to fourth cutscene
    }

    //public void CharacterSelectionScene()
    //{
    //    SceneManager.LoadScene(6); // Change to character selection scene
    //}

    public void ThemeSelectionScene()
    {
        //if(PlayerPrefs.GetString("Theme") != null && PlayerPrefs.GetString("Gender") != null)
        //{
        //    SceneManager.LoadScene("Main OG"); // go to main scene
        //}
        //else SceneManager.LoadScene(15); // Change to theme selection scene
        SceneManager.LoadScene(15);

    }
}
