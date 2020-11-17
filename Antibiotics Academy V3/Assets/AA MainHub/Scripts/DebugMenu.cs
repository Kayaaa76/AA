using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DebugMenu : MonoBehaviour
{
    public GameObject Menu;
    public GameObject CloseButton;
    public static bool isDeveloper = false;

    void Start()
    {
        Menu.SetActive(false);
        CloseButton.SetActive(false);
    }

    void Update()
    {
        if(isDeveloper == true)
        {
            CloseButton.SetActive(true);
        }
        else if(isDeveloper == false)
        {
            CloseButton.SetActive(false);
        }
    }

    public void OpenMenu()
    {
        Menu.SetActive(true);
        Player.TotalCoins();
    }

    public void CloseMenu()
    {
        Menu.SetActive(false);
    }

    public void OpenMatch3()
    {
        isDeveloper = true;
        if (ThemeSelectScreen.IsClassic == true || ThemeSelectScreen.IsTrixy == true)
        {
            SceneManager.LoadScene("Match3 OG");
        }
        else if(ThemeSelectScreen.IsYJ == true)
        {
            SceneManager.LoadScene("Match3 YJ");
        }
        else
        {
            SceneManager.LoadScene("Match3 OG");
        }
    }

    public void OpenTowerDefense()
    {
        isDeveloper = true;
        SceneManager.LoadScene("TowerDefense");
    }

    public void OpenEndlessRunner()
    {
        isDeveloper = true;
        SceneManager.LoadScene("Endless Runner");
    }

    public void CloseMatch3()
    {
        isDeveloper = false;
        if (ThemeSelectScreen.IsClassic == true)
        {
            SceneManager.UnloadScene("Match3 OG");
            SceneManager.LoadScene("Main OG");
        }
        else if (ThemeSelectScreen.IsYJ == true)
        {
            SceneManager.UnloadScene("Match3 YJ");
            SceneManager.LoadScene("Main YJ");
        }
        else if (ThemeSelectScreen.IsTrixy == true)
        {
            SceneManager.UnloadScene("Match3 OG");
            SceneManager.LoadScene("Main Trixy");
        }
        else
        {
            SceneManager.UnloadScene("Match3 OG");
            SceneManager.LoadScene("Main OG");
        }
    }

    public void CloseTowerDefense()
    {
        isDeveloper = false;
        if (ThemeSelectScreen.IsClassic == true)
        {
            SceneManager.UnloadScene("TowerDefense");
            SceneManager.LoadScene("Main OG");
        }
        else if (ThemeSelectScreen.IsYJ == true)
        {
            SceneManager.UnloadScene("TowerDefense");
            SceneManager.LoadScene("Main YJ");
        }
        else if (ThemeSelectScreen.IsTrixy == true)
        {
            SceneManager.UnloadScene("TowerDefense");
            SceneManager.LoadScene("Main Trixy");
        }
        else
        {
            SceneManager.UnloadScene("TowerDefense");
            SceneManager.LoadScene("Main OG");
        }
    }
    public void CloseEndlessRunner()
    {
        isDeveloper = false;
        if (ThemeSelectScreen.IsClassic == true)
        {
            SceneManager.UnloadScene("Endless Runner");
            SceneManager.LoadScene("Main OG");
        }
        else if (ThemeSelectScreen.IsYJ == true)
        {
            SceneManager.UnloadScene("Endless Runner");
            SceneManager.LoadScene("Main YJ");
        }
        else if (ThemeSelectScreen.IsTrixy == true)
        {
            SceneManager.UnloadScene("Endless Runner");
            SceneManager.LoadScene("Main Trixy");
        }
        else
        {
            SceneManager.UnloadScene("Endless Runner");
            SceneManager.LoadScene("Main OG");
        }
    }
}
