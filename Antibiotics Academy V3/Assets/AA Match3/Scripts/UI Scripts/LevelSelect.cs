using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Match3
{
    public class LevelSelect : MonoBehaviour
    {
        public Sprite levelSleep;
        public Sprite levelNutrition1;
        public Sprite levelNutrition2;
        public Sprite levelImmunisation;
        public Sprite levelExercise;
        public Sprite levelHealthy1;
        public Sprite levelHealthy2;
        public Sprite levelHealthy3;
        public Sprite levelHealthy4;
        public Sprite levelHealthy5;
        public Sprite levelLocked;

        public Font gameFont;
        public int fontSize = 65;

        public GameObject LevelSelectUI;

        Button[] totalLevels;

        public int playableLevels = 1;                                                         //number of levels unlocked

        HealthManager hm;
        //NotificationManager nm;


        // Start is called before the first frame update
        void Start()
        {
            totalLevels = new Button[transform.childCount];                                    //get total number of levels based on number of buttons

            hm = FindObjectOfType<HealthManager>();
            //nm = FindObjectOfType<NotificationManager>();
        }

        // Update is called once per frame
        void Update()
        {
            currentLevels();
        }

        void currentLevels()                                                                    //display levels
        {
            for (int i = 0; i < totalLevels.Length; i++)                                        //go through each button
            {
                totalLevels[i] = transform.GetChild(i).GetComponent<Button>();                  //get button

                if (i < playableLevels)                                                         //when level is unlocked
                {
                    if (i < 5)                                                                  //at levels 1-5
                    {
                        totalLevels[i].GetComponent<Image>().sprite = levelSleep;               //set button as given image
                        //nm.DisplayNotification(0);
                    }
                    else if (i < 8)                                                             //at levels 5-8
                    {
                        totalLevels[i].GetComponent<Image>().sprite = levelNutrition1;          //set button as given image
                    }
                    else if (i < 10)                                                            //at levels 8-10
                    {
                        totalLevels[i].GetComponent<Image>().sprite = levelNutrition2;          //set button as given image
                    }
                    else if (i < 15)                                                            //at levels 10-15
                    {
                        totalLevels[i].GetComponent<Image>().sprite = levelImmunisation;        //set button as given image
                    }
                    else if (i < 20)                                                            //at levels 15-20
                    {
                        totalLevels[i].GetComponent<Image>().sprite = levelExercise;            //set button as given image
                    }
                    else if (i < 21)                                                            //at level 21
                    {
                        totalLevels[i].GetComponent<Image>().sprite = levelHealthy1;            //set button as given image
                    }
                    else if (i < 22)                                                            //at level 22
                    {
                        totalLevels[i].GetComponent<Image>().sprite = levelHealthy2;            //set button as given image
                    }
                    else if (i < 23)                                                            //at level 23
                    {
                        totalLevels[i].GetComponent<Image>().sprite = levelHealthy3;            //set button as given image
                    }
                    else if (i < 24)                                                            //at level 24
                    {
                        totalLevels[i].GetComponent<Image>().sprite = levelHealthy4;            //set button as given image
                    }
                    else if (i < 25)                                                            //at level 24
                    {
                        totalLevels[i].GetComponent<Image>().sprite = levelHealthy5;            //set button as given image
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

        public void easyLevel()
        {
            LevelSelectUI.SetActive(false);

            hm.currentHealth = hm.min_healthyHealth;

            Time.timeScale = 1f;
        }

        public void mediumLevel()
        {
            LevelSelectUI.SetActive(false);

            hm.currentHealth = hm.min_betterHealth;

            Time.timeScale = 1f;
        }

        public void difficultLevel()
        {
            LevelSelectUI.SetActive(false);

            hm.currentHealth = hm.min_neutralHealth;

            Time.timeScale = 1f;
        }
    }
}
