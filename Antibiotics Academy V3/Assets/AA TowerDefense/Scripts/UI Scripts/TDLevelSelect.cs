using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense
{
    public class TDLevelSelect : MonoBehaviour
    {
        public Sprite levelAntibiotic;
        public Sprite levelEat;
        public Sprite levelWater;
        public Sprite levelLocked;

        public Font gameFont;
        public int fontSize = 65;

        public GameObject LevelSelectUI;
        public GameObject tutorialUI;

        public GameObject heart;
        private AudioSource src;

        Button[] totalLevels;

        public int playableLevels = 1;                                                         //number of levels unlocked


        // Start is called before the first frame update
        void Start()
        {
            totalLevels = new Button[transform.childCount];                                    //get total number of levels based on number of buttons
        }

        // Update is called once per frame
        void Update()
        {
            currentLevels();
        }

        void currentLevels()                                                                   //display levels
        {
            for (int i = 0; i < totalLevels.Length; i++)                                       //go through each button
            {
                totalLevels[i] = transform.GetChild(i).GetComponent<Button>();                 //get button

                if (i < playableLevels)                                                        //when level is unlocked
                {
                    if (i < 3)                                                                 //at levels 1-3
                    {
                        totalLevels[i].GetComponent<Image>().sprite = levelAntibiotic;         //set button as given image
                    }
                    else if (i < 6)                                                            //at levels 4-6
                    {
                        totalLevels[i].GetComponent<Image>().sprite = levelEat;                //set button as given image
                    }
                    else if (i < 9)                                                            //at levels 7-9
                    {
                        totalLevels[i].GetComponent<Image>().sprite = levelWater;              //set button as given image
                    }

                    totalLevels[i].interactable = true;                                         //make button interactable
                    totalLevels[i].GetComponentInChildren<Text>().text = (i + 1).ToString();    //change button text to level number
                    totalLevels[i].GetComponentInChildren<Text>().fontSize = fontSize;
                    totalLevels[i].GetComponentInChildren<Text>().font = gameFont;
                }
                else
                {                                                                               //when level is not unlocked
                    totalLevels[i].GetComponent<Image>().sprite = levelLocked;                  //change button image to locked
                    totalLevels[i].interactable = false;                                        //make button not interactable
                    totalLevels[i].GetComponentInChildren<Text>().text = null;                  //change button text to nothing
                }
            }
        }

        public void differentLevel()
        {
            LevelSelectUI.SetActive(false);
            tutorialUI.SetActive(false);

            Time.timeScale = 1f;

            src = heart.GetComponent<AudioSource>();
            if (!src.isPlaying)
            {
                src.Play();
            }
        }
    }
}
