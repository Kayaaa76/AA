﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    public class StartTrigger1 : MonoBehaviour
    {
        public GameObject StartUI;
        public GameObject LevelsUI;

        //public GameObject heart;
        //private AudioSource src;

        public void TriggerStart()                       //function to start the game
        {
            //Time.timeScale = 1f;
            //src = heart.GetComponent<AudioSource>();
            StartUI.SetActive(false);
            LevelsUI.SetActive(true);

            //if (!src.isPlaying)
            //{
            //    src.Play();
            //}
        }
    }
}
