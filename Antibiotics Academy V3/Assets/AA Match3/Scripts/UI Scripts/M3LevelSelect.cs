using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Match3
{
    public class M3LevelSelect : MonoBehaviour
    {
        public Sprite PastelLevelSleep;
        public Sprite PastelLevelNutrition1;
        public Sprite PastelLevelNutrition2;
        public Sprite PastelLevelImmunisation;
        public Sprite PastelLevelExercise;
        public Sprite PastelLevelHealthy1;
        public Sprite PastelLevelHealthy2;
        public Sprite PastelLevelHealthy3;
        public Sprite PastelLevelHealthy4;
        public Sprite PastelLevelHealthy5;

        public Sprite ClassicLevelSleep;
        public Sprite ClassicLevelNutrition1;
        public Sprite ClassicLevelNutrition2;
        public Sprite ClassicLevelImmunisation;
        public Sprite ClassicLevelExercise;
        public Sprite ClassicLevelHealthy1;
        public Sprite ClassicLevelHealthy2;
        public Sprite ClassicLevelHealthy3;
        public Sprite ClassicLevelHealthy4;
        public Sprite ClassicLevelHealthy5;

        public Sprite BoldLevelSleep;
        public Sprite BoldLevelNutrition1;
        public Sprite BoldLevelNutrition2;
        public Sprite BoldLevelImmunisation;
        public Sprite BoldLevelExercise;
        public Sprite BoldLevelHealthy1;
        public Sprite BoldLevelHealthy2;
        public Sprite BoldLevelHealthy3;
        public Sprite BoldLevelHealthy4;
        public Sprite BoldLevelHealthy5;

        public Sprite levelLocked;

        public Font gameFont;
        public int fontSize = 65;

        public GameObject LevelSelectUI;
        public GameObject LevelSelectMenu;
        public GameObject StartUI;

        Button[] totalLevels;

        public int playableLevels;                                                              //number of levels unlocked
        public int playingLevel;                                                                //level currently playing

        HealthManager hm;
        NotificationManager nm;

        public int notification;

        // Start is called before the first frame update
        void Start()
        {
            totalLevels = new Button[transform.childCount];                                     //get total number of levels based on number of buttons

            hm = FindObjectOfType<HealthManager>();
            nm = FindObjectOfType<NotificationManager>();

            if(StartUI.activeSelf == true)
            {
                LevelSelectMenu.SetActive(false);
            }

            if(Player.m3unlockedlevels < 1)
            {
                Player.m3unlockedlevels = 1;
            }

            playableLevels = Player.m3unlockedlevels;
        }

        // Update is called once per frame
        void Update()
        {
            currentLevels();
            if (EventSystem.current.currentSelectedGameObject.name.Contains("Lvl"))             //check if selected object name contains "Lvl" (for level buttons)
            {
                playingLevel = int.Parse(EventSystem.current.currentSelectedGameObject.GetComponentInChildren<Text>().text); //set playing level to button text
            }
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
                        if(PlayerPrefs.GetString("Theme") == "Pastel")
                        {
                            totalLevels[i].GetComponent<Image>().sprite = PastelLevelSleep;     //set button as given image
                        }
                        else if (PlayerPrefs.GetString("Theme") == "Classic")
                        {
                            totalLevels[i].GetComponent<Image>().sprite = ClassicLevelSleep;    //set button as given image
                        }
                        else if (PlayerPrefs.GetString("Theme") == "Bold")
                        {
                            totalLevels[i].GetComponent<Image>().sprite = BoldLevelSleep;       //set button as given image
                        }
                    }
                    else if (i < 8)                                                             //at levels 6-8
                    {
                        if (PlayerPrefs.GetString("Theme") == "Pastel")
                        {
                            totalLevels[i].GetComponent<Image>().sprite = PastelLevelNutrition1;               //set button as given image
                        }
                        else if (PlayerPrefs.GetString("Theme") == "Classic")
                        {
                            totalLevels[i].GetComponent<Image>().sprite = ClassicLevelNutrition1;               //set button as given image
                        }
                        else if (PlayerPrefs.GetString("Theme") == "Bold")
                        {
                            totalLevels[i].GetComponent<Image>().sprite = BoldLevelNutrition1;               //set button as given image
                        }
                    }
                    else if (i < 10)                                                            //at levels 9-10
                    {
                        if (PlayerPrefs.GetString("Theme") == "Pastel")
                        {
                            totalLevels[i].GetComponent<Image>().sprite = PastelLevelNutrition2;               //set button as given image
                        }
                        else if (PlayerPrefs.GetString("Theme") == "Classic")
                        {
                            totalLevels[i].GetComponent<Image>().sprite = ClassicLevelNutrition2;               //set button as given image
                        }
                        else if (PlayerPrefs.GetString("Theme") == "Bold")
                        {
                            totalLevels[i].GetComponent<Image>().sprite = BoldLevelNutrition2;               //set button as given image
                        }
                    }
                    else if (i < 15)                                                            //at levels 11-15
                    {
                        if (PlayerPrefs.GetString("Theme") == "Pastel")
                        {
                            totalLevels[i].GetComponent<Image>().sprite = PastelLevelImmunisation;               //set button as given image
                        }
                        else if (PlayerPrefs.GetString("Theme") == "Classic")
                        {
                            totalLevels[i].GetComponent<Image>().sprite = ClassicLevelImmunisation;               //set button as given image
                        }
                        else if (PlayerPrefs.GetString("Theme") == "Bold")
                        {
                            totalLevels[i].GetComponent<Image>().sprite = BoldLevelImmunisation;               //set button as given image
                        }
                    }
                    else if (i < 20)                                                            //at levels 16-20
                    {
                        if (PlayerPrefs.GetString("Theme") == "Pastel")
                        {
                            totalLevels[i].GetComponent<Image>().sprite = PastelLevelExercise;               //set button as given image
                        }
                        else if (PlayerPrefs.GetString("Theme") == "Classic")
                        {
                            totalLevels[i].GetComponent<Image>().sprite = ClassicLevelExercise;               //set button as given image
                        }
                        else if (PlayerPrefs.GetString("Theme") == "Bold")
                        {
                            totalLevels[i].GetComponent<Image>().sprite = BoldLevelExercise;               //set button as given image
                        }
                    }
                    else if (i < 21)                                                            //at level 21
                    {
                        if (PlayerPrefs.GetString("Theme") == "Pastel")
                        {
                            totalLevels[i].GetComponent<Image>().sprite = PastelLevelHealthy1;               //set button as given image
                        }
                        else if (PlayerPrefs.GetString("Theme") == "Classic")
                        {
                            totalLevels[i].GetComponent<Image>().sprite = ClassicLevelHealthy1;               //set button as given image
                        }
                        else if (PlayerPrefs.GetString("Theme") == "Bold")
                        {
                            totalLevels[i].GetComponent<Image>().sprite = BoldLevelHealthy1;               //set button as given image
                        }
                    }
                    else if (i < 22)                                                            //at level 22
                    {
                        if (PlayerPrefs.GetString("Theme") == "Pastel")
                        {
                            totalLevels[i].GetComponent<Image>().sprite = PastelLevelHealthy2;               //set button as given image
                        }
                        else if (PlayerPrefs.GetString("Theme") == "Classic")
                        {
                            totalLevels[i].GetComponent<Image>().sprite = ClassicLevelHealthy2;               //set button as given image
                        }
                        else if (PlayerPrefs.GetString("Theme") == "Bold")
                        {
                            totalLevels[i].GetComponent<Image>().sprite = BoldLevelHealthy2;               //set button as given image
                        }
                    }
                    else if (i < 23)                                                            //at level 23
                    {
                        if (PlayerPrefs.GetString("Theme") == "Pastel")
                        {
                            totalLevels[i].GetComponent<Image>().sprite = PastelLevelHealthy3;               //set button as given image
                        }
                        else if (PlayerPrefs.GetString("Theme") == "Classic")
                        {
                            totalLevels[i].GetComponent<Image>().sprite = ClassicLevelHealthy3;               //set button as given image
                        }
                        else if (PlayerPrefs.GetString("Theme") == "Bold")
                        {
                            totalLevels[i].GetComponent<Image>().sprite = BoldLevelHealthy3;               //set button as given image
                        }
                    }
                    else if (i < 24)                                                            //at level 24
                    {
                        if (PlayerPrefs.GetString("Theme") == "Pastel")
                        {
                            totalLevels[i].GetComponent<Image>().sprite = PastelLevelHealthy4;               //set button as given image
                        }
                        else if (PlayerPrefs.GetString("Theme") == "Classic")
                        {
                            totalLevels[i].GetComponent<Image>().sprite = ClassicLevelHealthy4;               //set button as given image
                        }
                        else if (PlayerPrefs.GetString("Theme") == "Bold")
                        {
                            totalLevels[i].GetComponent<Image>().sprite = BoldLevelHealthy4;               //set button as given image
                        }
                    }
                    else if (i < 25)                                                            //at level 24
                    {
                        if (PlayerPrefs.GetString("Theme") == "Pastel")
                        {
                            totalLevels[i].GetComponent<Image>().sprite = PastelLevelHealthy5;               //set button as given image
                        }
                        else if (PlayerPrefs.GetString("Theme") == "Classic")
                        {
                            totalLevels[i].GetComponent<Image>().sprite = ClassicLevelHealthy5;               //set button as given image
                        }
                        else if (PlayerPrefs.GetString("Theme") == "Bold")
                        {
                            totalLevels[i].GetComponent<Image>().sprite = BoldLevelHealthy5;               //set button as given image
                        }
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
            hm.currentHealth = hm.min_healthyHealth;
        }

        public void mediumLevel()
        {
            hm.currentHealth = hm.min_betterHealth;
        }

        public void difficultLevel()
        {
            hm.currentHealth = hm.min_neutralHealth;
        }

        public void restLevel()
        {
            hm.bonusTag = "Sleeping";
            notification = 0;
            nm.isSwitching = false;
        }

        public void foodLevel()
        {
            hm.bonusTag = "Fruit";
            hm.bonusTag1 = "Vegetable";
            notification = 1;
            nm.isSwitching = false;
        }

        public void immunisationLevel()
        {
            hm.bonusTag = "Water";
            notification = 2;
            nm.isSwitching = false;
        }

        public void runningLevel()
        {
            hm.bonusTag = "Running";
            notification = 3;
            nm.isSwitching = false;
        }

        public void healthyLevel()
        {
            hm.bonusTag = "None";
            notification = 4;
            nm.isSwitching = false;
        }

        public void enterLevel()
        {
            if (Player.lives > 0)
            {
                LevelSelectUI.SetActive(false);

                Player.lives -= 1;
                Debug.Log("used life");

                Time.timeScale = 1f;
            }
            else
            {
                Debug.Log("You do not have enough lifes to play the game!");
            }
        }
    }
}
