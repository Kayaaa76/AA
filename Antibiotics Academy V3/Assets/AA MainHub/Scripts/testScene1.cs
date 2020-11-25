using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class testScene1 : MonoBehaviour
{
    GameObject player; // get player game object

    public void Start()
    {
        player = GameObject.Find("Player"); // find the player game object
    }

    public void Load() // function to load the match 3 mini game
    {
        if (Player.lives > 0)
        {
            if (ThemeSelectScreen.IsYJ == true)
            {
                SceneManager.LoadScene(8); // match 3
                Player.lives -= 1;
            }
            else if (ThemeSelectScreen.IsClassic == true || ThemeSelectScreen.IsTrixy == true)
            {
                SceneManager.LoadScene(14);
                Player.lives -= 1;
            }
            else
            {
                SceneManager.LoadScene(14);
                Player.lives -= 1;
            }
        }
        else
        {
            Debug.Log("You do not have enough lives to play the game!");
        }
    }
}
