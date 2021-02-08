using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Match3
{
    public enum HealthStates          //the 3 different health states
    { 
        Sick, // red
        Neutral, // yellow
        Healthy // green
    }


    public class HealthManager : MonoBehaviour
    {
        static bool coinsChange = false;

        public HealthStates healthState;
        private NotificationManager nm;
        private DisplayEndUI display;
        private FindMatch fm;
        public SpriteRenderer sr;
        public Sprite[] sprites;

        public GameObject emojiState;

        public int maxHealth = 100;
        public float currentHealth;
        public float min_sickHealth = 1f;
        public float min_neutralHealth = 25f;
        public float min_betterHealth = 50f;
        public float min_healthyHealth = 75f;

        private float rateOfDecrease;
        private float addNormal = 1f;
        private float addBonus = 2f;

        private float addHealth;

        public HealthBar healthBar;

        //LevelSelect levelSelect;
        //public GameObject LevelSelectUI;

        public string bonusTag;
        public string bonusTag1;
        private float healthToAdd;

        private Animator anim;
        public string boolNameSleep; // red
        public string boolNameEat; // yellow
        public string boolNameRun; // green

        public GameObject bannerEat;
        public GameObject bannerSleep;
        public GameObject bannerRun;

        public AudioSource audiosrc;

        public GameObject m3levelselectscript;

        public bool startedLevel;

        void Start()
        {
            fm = FindObjectOfType<FindMatch>();
            display = FindObjectOfType<DisplayEndUI>();
            //nm = FindObjectOfType<NotificationManager>();
            currentHealth = min_neutralHealth;
            healthState = HealthStates.Sick;
            healthBar.SetHealth(min_neutralHealth);

            anim = GetComponent<Animator>();
            audiosrc = GetComponent<AudioSource>();
        }

        void Update()
        {
            if (coinsChange == true)
            {
                StartCoroutine(PostCoinAcitivty());
            }

            if(startedLevel == true)
            {
                StartCoroutine(PostGameLevelActivity());
            }
            else
            {
                return;
            }
        }

        void FixedUpdate()
        {
            currentHealth -= rateOfDecrease * Time.deltaTime;  //reduces health over time based on the rate of decrease
            healthBar.SetHealth(currentHealth);                //updates the health bar by calling the sethealth function, passing the currenthealth in the argument

            CheckState();                                      //calls checkstate to check the state of the health 
        }

        private void CheckState()                              //function to do things according to the state of the health
        {
            if (currentHealth > 100)                           //if currentHealth is greater than 100, it means that the player won
            {
                display.DisplayWinUI();                        //displays the Win UI
                if (m3levelselectscript.GetComponent<M3LevelSelect>().playingLevel == Player.m3unlockedlevels) //check if playing level is same as unlocked level
                {
                    if (Player.m3unlockedlevels < 6)            //when completing levels 1-5
                    {
                        Player.coins += 10;                    //award 10 coins for passing level (one time claim)
                        Debug.Log("You got 10 coins for winning this level!");
                        StartCoroutine(Login.UpdateCoins());
                        coinsChange = true;
                    }
                    else if (Player.m3unlockedlevels < 11)      //when completing levels 6-10
                    {
                        Player.coins += 30;                    //award 30 coins for passing level (one time claim)
                        Debug.Log("You got 30 coins for winning this level!");
                        StartCoroutine(Login.UpdateCoins());
                        coinsChange = true;
                    }
                    else if (Player.m3unlockedlevels < 16)     //when completing levels 11-15
                    {
                        Player.coins += 50;                    //award 50 coins for passing level (one time claim)
                        Debug.Log("You got 50 coins for winning this level!");
                        StartCoroutine(Login.UpdateCoins());
                        coinsChange = true;
                    }
                    else if (Player.m3unlockedlevels < 21)     //when completing levels 16-20
                    {
                        Player.coins += 60;                    //award 60 coins for passing level (one time claim)
                        Debug.Log("You got 60 coins for winning this level!");
                        StartCoroutine(Login.UpdateCoins());
                        coinsChange = true;
                    }
                    else if (Player.m3unlockedlevels < 26)     //when completing levels 21-25
                    {
                        if(Player.m3unlockedlevels == 25)
                        {
                            Player.dateEndM3 = System.DateTime.Now;
                            Player.m3Duration = (Player.dateEndM3 - Player.dateStartM3).ToString();
                        }
                        Player.coins += 80;                    //award 80 coins for passing level (one time claim)
                        Debug.Log("You got 80 coins for winning this level!");
                        StartCoroutine(Login.UpdateCoins());
                        coinsChange = true;
                    }

                    Player.m3unlockedlevels += 1;              //increase unlocked level
                }
            }
            else if (currentHealth < 1)                        //if currentHealth is lesser than 1, meaning 0, it means that the player lost
            {
                display.DisplayDeathUI();                      //displays the Death UI
            }
            else if (currentHealth < min_neutralHealth && currentHealth > 1) //red health, sick state
            {
                healthState = HealthStates.Sick;
                //nm.DisplayNotification(0);                                  
                sr.sprite = sprites[0];

                bannerSleep.SetActive(true);
                bannerEat.SetActive(false);
                bannerRun.SetActive(false);

                SetBoolActiveSleep();
                SetBoolInactiveRun();
                SetBoolInactiveEat();

                if (!audiosrc.isPlaying)
                {
                    audiosrc.Play();
                }
            }

            else if (currentHealth >= min_neutralHealth && currentHealth < min_healthyHealth) //yellow health, neutral health
            {
                healthState = HealthStates.Neutral;
                //nm.DisplayNotification(1);
                sr.sprite = sprites[1];

                if (audiosrc.isPlaying)
                {
                    audiosrc.Stop();
                }

                bannerSleep.SetActive(false);
                bannerEat.SetActive(true);
                bannerRun.SetActive(false);

                SetBoolActiveEat();
                SetBoolInactiveRun();
                SetBoolInactiveSleep();
            }

            else if (currentHealth >= min_healthyHealth && currentHealth < 100f) //green health, healthy state
            {
                healthState = HealthStates.Healthy;
                //nm.DisplayNotification(2);
                sr.sprite = sprites[2];

                if (audiosrc.isPlaying)
                {
                    audiosrc.Stop();
                }

                bannerSleep.SetActive(false);
                bannerEat.SetActive(false);
                bannerRun.SetActive(true);

                SetBoolActiveRun();
                SetBoolInactiveEat();
                SetBoolInactiveSleep();
            }
            SetBonus(healthState);
        }

        private void SetBonus(HealthStates states)     //function that changes the bonus health for the matches based on the specific state, as well as changing the health decrease rate
        {
            switch (states)
            {
                case HealthStates.Sick:                //if state is sick, then matching water and sleep gives bonus health
                    //bonusTag = "Water";
                    //bonusTag1 = "Sleeping";
                    rateOfDecrease = 1.0f;             //changes the rate of decrease to 1
                    break;
                case HealthStates.Neutral:             //if state is neutral, then matching fruit and veg gives bonus health  
                    //bonusTag = "Fruit";
                    //bonusTag1 = "Vegetable";
                    rateOfDecrease = 1.25f;            //changes the rate of decrease to 1.25
                    break;
                case HealthStates.Healthy:             //if state is healthy, then matching running gives bonus health
                    //bonusTag = "Running";
                    //bonusTag1 = "?";
                    rateOfDecrease = 1.5f;             //changes the rate of decrease to 1.5
                    break;
            }
        }

        public void CalcAddHealth(string pieceTag)    //function to calculate the health
        {
            int prefabCount = 0;                      //the prefab count
            int bonusCount = 0;                       //the bonus count

            if (pieceTag == bonusTag || pieceTag == bonusTag1) //if the piece is the same as the bonus tag, then it will add onto the bonus count
            {
                bonusCount++;
            }
            else                                               //otherwise, it will just add to the prefab count
            {
                prefabCount++;

            }
            healthToAdd = (prefabCount * addNormal) + (bonusCount * addBonus); //calculates the health to add

            //Debug.Log(healthToAdd);

            currentHealth += healthToAdd; //adds healthtoadd to current health

            healthToAdd = 0;              //reset all variables back to 0
            bonusCount = 0;
            prefabCount = 0;
        }

        // Yellow Health
        public void SetAnimBoolEat(bool active)
        {
            anim.SetBool(boolNameEat, active);
        }

        public void SetBoolActiveEat()
        {
            anim.SetBool(boolNameEat, true);
        }

        public void SetBoolInactiveEat()
        {
            anim.SetBool(boolNameEat, false);
        }

        // Red Health
        public void SetAnimBoolSleep(bool active)
        {
            anim.SetBool(boolNameSleep, active);
        }

        public void SetBoolActiveSleep()
        {
            anim.SetBool(boolNameSleep, true);
        }

        public void SetBoolInactiveSleep()
        {
            anim.SetBool(boolNameSleep, false);
        }

        // Green Health
        public void SetAnimBoolRun(bool active)
        {
            anim.SetBool(boolNameRun, active);
        }

        public void SetBoolActiveRun()
        {
            anim.SetBool(boolNameRun, true);
        }

        public void SetBoolInactiveRun()
        {
            anim.SetBool(boolNameRun, false);
        }

        IEnumerator PostCoinAcitivty()
        {
            coinsChange = false;

            WWWForm formPostCoinActivity = new WWWForm();
            WWW wwwPostCoinActivity = new WWW("http://103.239.222.212/ALIVE2Service/api/game/PostActivity?ActivityTypeName=" + "Player Coins&" + "username=" + Login.tnameField.text + "&ActivityDataValue=" + "Player Coins", formPostCoinActivity);
            yield return wwwPostCoinActivity;
            Debug.Log(wwwPostCoinActivity.text);
            Debug.Log(wwwPostCoinActivity.error);
            Debug.Log(wwwPostCoinActivity.url);
        }

        IEnumerator PostGameLevelActivity()
        {
            startedLevel = false;

            WWWForm formPostGameLevelActivity = new WWWForm();
            WWW wwwPostGameLevelActivity = new WWW("http://103.239.222.212/ALIVE2Service/api/game/PostActivity?ActivityTypeName=" + "Game Level&" + "username=" + Login.tnameField.text + "&ActivityDataValue=" + "Game Level", formPostGameLevelActivity);
            yield return wwwPostGameLevelActivity;
            Debug.Log(wwwPostGameLevelActivity.text);
            Debug.Log(wwwPostGameLevelActivity.error);
            Debug.Log(wwwPostGameLevelActivity.url);
        }
    }
}

