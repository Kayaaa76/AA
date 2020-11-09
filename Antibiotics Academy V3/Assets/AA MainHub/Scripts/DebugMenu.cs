using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DebugMenu : MonoBehaviour
{
    public GameObject DebugButton;
    public GameObject Menu;
    public GameObject Match3Button;
    public GameObject TowerDefenseButton;
    public GameObject EndlessRunnerButton;
    public GameObject CloseButton;
    public static GameObject Scene;

    void Start()
    {
        Scene = GameObject.Find("Scene");
        Menu.SetActive(false);
    }

    public void OpenMenu()
    {
        Menu.SetActive(true);
    }

    public void CloseMenu()
    {
        Menu.SetActive(false);
    }

    public void OpenMatch3()
    {
        if (ThemeSelectScreen.IsClassic == true || ThemeSelectScreen.IsTrixy == true)
        {
            SceneManager.LoadSceneAsync("Match3 OG", LoadSceneMode.Additive);
        }
        else if(ThemeSelectScreen.IsYJ == true)
        {
            SceneManager.LoadSceneAsync("Match3 YJ", LoadSceneMode.Additive);
        }
        else
        {
            SceneManager.LoadSceneAsync("Match3 OG",LoadSceneMode.Additive);
        }

        ChangeSceneManager.SceneActive = false;
    }

    public void OpenTowerDefense()
    {
        SceneManager.LoadSceneAsync("TowerDefense", LoadSceneMode.Additive);
        ChangeSceneManager.SceneActive = false;
    }

    public void OpenEndlessRunner()
    {
        SceneManager.LoadSceneAsync("Endless Runner", LoadSceneMode.Additive);
        ChangeSceneManager.SceneActive = false;
    }

    public void CloseMatch3()
    {
        ChangeSceneManager.SceneActive = true;
        if (ThemeSelectScreen.IsClassic == true || ThemeSelectScreen.IsTrixy == true)
        {
            SceneManager.UnloadSceneAsync("Match3 OG");
        }
        else if (ThemeSelectScreen.IsYJ == true)
        {
            SceneManager.UnloadSceneAsync("Match3 YJ");
        }
        else
        {
            SceneManager.UnloadSceneAsync("Match3 OG");
        }
    }

    public void CloseTowerDefense()
    {
        ChangeSceneManager.SceneActive = true;
        SceneManager.UnloadSceneAsync("TowerDefense");
    }
    public void CloseEndlessRunner()
    {
        ChangeSceneManager.SceneActive = true;
        SceneManager.UnloadSceneAsync("Endless Runner");
    }
}
