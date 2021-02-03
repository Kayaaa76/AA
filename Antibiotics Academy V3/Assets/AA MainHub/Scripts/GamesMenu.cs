using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamesMenu : MonoBehaviour
{
    public GameObject gamesMenuPanel;
    
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
        gamesMenuPanel.SetActive(true);
    }

    public void closeGamesMenu()
    {
        gamesMenuPanel.SetActive(false);
    }

    public void unlockingGames()
    {
        if (Player.tdunlockedlevels < 2) //when players have not won level 1 of tower defense
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

        if (Player.runlocked == false)
        {
            RBtn.GetComponent<Image>().sprite = gameLocked;
            RBtn.GetComponentInChildren<Text>().text = "?";
            RBtn.interactable = false;
        }
        else
        {
            RBtn.GetComponent<Image>().sprite = RLogo;
            RBtn.GetComponentInChildren<Text>().text = "Runner";
            RBtn.interactable = true;
        }
    }

    public void match3Unlocked()
    {
        GameManager.currentPosition = GameManager.player.transform.position;
        GameManager.sceneCounter = 2;

        SceneManager.LoadScene(14); //match 3 scene
    }

    public void towerDefenseUnlocked()
    {
        GameManager.currentPosition = GameManager.player.transform.position;
        GameManager.sceneCounter = 2;

        SceneManager.LoadScene(9); //tower defense scene
    }

    public void runnerUnlocked()
    {
        GameManager.currentPosition = GameManager.player.transform.position;
        GameManager.sceneCounter = 2;

        SceneManager.LoadScene(11); //runner scene
    }
}
