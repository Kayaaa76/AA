﻿using System.Collections;
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
        if (ThemeSelectScreen.IsYJ == true)
        {
            SceneManager.LoadSceneAsync(8); // match 3
        }
        else if(ThemeSelectScreen.IsClassic == true)
        {
            SceneManager.LoadSceneAsync(14);
        }
    }
}
