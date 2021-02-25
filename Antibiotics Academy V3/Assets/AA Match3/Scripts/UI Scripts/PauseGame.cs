using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject levelsConfirmMenu;
    public GameObject quitConfirmMenu;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void pause()
    {
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
    }

    public void resume()
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
    }

    public void confirmLevels()
    {
        levelsConfirmMenu.SetActive(true);
    }

    public void undoLevels()
    {
        levelsConfirmMenu.SetActive(false);
    }

    public void confirmQuit()
    {
        quitConfirmMenu.SetActive(true);
    }

    public void undoQuit()
    {
        quitConfirmMenu.SetActive(false);
    }
}
