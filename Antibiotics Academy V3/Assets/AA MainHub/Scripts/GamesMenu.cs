using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamesMenu : MonoBehaviour
{
    public GameObject gamesMenu;
    
    public Button TDBtn;
    public Button RBtn;

    public Sprite TDLogo;
    public Sprite RLogo;
    public Sprite gameLocked;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        unlockingGames();
    }

    public void openGamesMenu()
    {
        gamesMenu.SetActive(true);
    }

    public void closeGamesMenu()
    {
        gamesMenu.SetActive(false);
    }

    public void unlockingGames()
    {
        if (Player.tdunlockedlevels < 1)
        {
            TDBtn.GetComponent<Image>().sprite = gameLocked;
            TDBtn.GetComponentInChildren<Text>().text = "?";
            TDBtn.interactable = false;
        }
        else
        {
            TDBtn.GetComponent<Image>().sprite = TDLogo;
            TDBtn.GetComponentInChildren<Text>().text = "Tower Defense";
            TDBtn.interactable = true;
        }

        //if (Player.runlockedlevels < 1)
        //{
        //    RBtn.GetComponent<Image>().sprite = gameLocked;
        //    RBtn.GetComponentInChildren<Text>().text = "?";
        //    RBtn.interactable = false;
        //}
        //else
        //{
        //    RBtn.GetComponent<Image>().sprite = RLogo;
        //    RBtn.GetComponentInChildren<Text>().text = "Runner";
        //    RBtn.interactable = true;
        //}
    }

    public void match3Unlocked()
    {
        GameManager.currentPosition = GameManager.player.transform.position;
        GameManager.sceneCounter = 2;

        SceneManager.LoadScene(14); //match 3 scene
    }

    public void towerDefenseUnlocked()
    {
        //GameManager.currentPosition = GameManager.player.transform.position;
        //GameManager.sceneCounter = 2;

        //SceneManager.LoadScene(); //tower defense scene
    }

    public void runnerUnlocked()
    {
        //GameManager.currentPosition = GameManager.player.transform.position;
        //GameManager.sceneCounter = 2;

        //SceneManager.LoadScene(); //runner scene
    }
}
