using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Match3
{
    public class AntibioticAbility : MonoBehaviour
    {
        public HealthBar healthBar;                                                  //reference to the Healthbar class
        public HealthManager healthManager;                                          //reference to the HealthManager class
        private int counter = 3;

        public Button btn;

        static bool coinsChange = false;

        private void Start()
        {
            btn.onClick.AddListener(Effectiveness);
        }


        private void Update()
        {
            if (healthManager.healthState == HealthStates.Sick && counter > 0 && Player.coins >= 25)       //if health state is in sick and counter is greater than 0
            {
                btn.interactable = true;                                             //antibiotic button will be interactable
            }
            else
            {
                btn.interactable = false;                                            //otherwise, it is not interactable
            }

            if (coinsChange == true)
            {
                StartCoroutine(PostCoinAcitivty());
                StartCoroutine(Login.UpdateCoins());
            }
        }

        private void Effectiveness()                                                  //function that reduces the effectiveness of the antibiotic ability after every use
        {
            if (counter == 3)                                                        //if counter is 3, health adds by 25 and counter reduces by 1
            {
                healthManager.currentHealth += 25;                                    //if counter is 3, adds health by 25
                counter -= 1;                                                         //reduces counter by 1
                Player.coins -= 25;
                coinsChange = true;
            }

            else if (counter == 2)                                                   //if counter is 2, health adds by 15 and counter reduces by 1
            {
                healthManager.currentHealth += 15;                                    //if counter is 2, adds health by 15
                counter -= 1;                                                         //reduces counter by 1
                Player.coins -= 25;
                coinsChange = true;
            }

            else if (counter == 1)                                                   //if counter is 1, health adds by 10 and counter reduces by 1, which means counter is at 0 and the ability can no longer be used
            {
                healthManager.currentHealth += 10;                                    //if counter is 1, adds health by 10
                counter -= 1;                                                         //reduces counter by 1, now counter is 0 and the ability can never be used again
                Player.coins -= 25;
                coinsChange = true;
            }
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
    }
}
