using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DailySpin : MonoBehaviour
{
    public float rotationTime;
    public int spinTime;
    public int stoppedAngle;
    public GameObject wheel;
    public GameObject menu;
    public Button button;
    public Text prizeText;
    public Text spinText;
    string daily;

    // Start is called before the first frame update
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        //active after prize has been chosen
        if (daily != null)
        {
            //when screen is clicked/touched
            if (Input.GetMouseButtonDown(0) || Input.touchCount > 0)
            {
                //deactivate menu
                menu.SetActive(false);
            }
        }
    }

    public void spin()
    {
        //start spin process
        StartCoroutine(Spinning());

        //disable spin button
        button.interactable = false;

        //change text on spin button to completed
        spinText.text = "Completed";
    }

    IEnumerator Spinning()
    {
        //random spinning time span to land on different rewards
        spinTime = Random.Range(280, 320);

        //time per interval
        rotationTime = 0.001f;

        //CONTINUOUSLY SPIN UNTIL SPIN TIME IS REACHED
        //at each interval:
        for (int i = 0; i < spinTime; i++)
        {
            //rotate 9 degree (as per each interval)
            wheel.transform.Rotate(0, 0, 9);

            //when spin time reach 50%
            if (i > Mathf.RoundToInt(spinTime * 0.5f))
            {
                //increased time per interval - make it slower
                rotationTime = 0.01f;
                //Debug.Log("started");
            }

            //when spin time reach 80%
            if (i > Mathf.RoundToInt(spinTime * 0.8f))
            {
                //increased time per interval - make it slower
                rotationTime = 0.04f;
                //Debug.Log("half");
            }

            //when spin time reaches 90%
            if (i > Mathf.RoundToInt(spinTime * 0.9f))
            {
                //increased time per interval - make it slower
                rotationTime = 0.08f;
                //Debug.Log("soon");
            }

            //when spin time reaches 96%
            if (i > Mathf.RoundToInt(spinTime * 0.96f))
            {
                //increased time per interval - make it slower
                rotationTime = 0.2f;
                //Debug.Log("ending");
            }

            //wait for time set for each interval to be done
            yield return new WaitForSeconds(rotationTime);
        }

        //angle the wheel stopped
        stoppedAngle = Mathf.RoundToInt(wheel.transform.eulerAngles.z);

        //when wheel stops at 1 life (angles based on wheel)
        if (stoppedAngle <= 90)
        {
            daily = "1 Life";
            Player.lives += 1;
            Debug.Log("You got 1 life from the Reward Wheel!");
            WWWForm formPostLifeActivity = new WWWForm();
            WWW wwwPostLifeActivity = new WWW("http://103.239.222.212/ALIVE2Service/api/game/PostActivity?ActivityTypeName=" + "Player Lifes&" + "username=" + Login.tnameField.text + "&ActivityDataValue=" + "Player Lifes", formPostLifeActivity);
            yield return wwwPostLifeActivity;
            Debug.Log(wwwPostLifeActivity.text);
            Debug.Log(wwwPostLifeActivity.error);
            Debug.Log(wwwPostLifeActivity.url);

            StartCoroutine(Login.UpdateLives());
        }
        //when wheel stops at 10 coins (angles based on wheel)
        else if (stoppedAngle <= 234)
        {
            daily = "10 Coins";
            Player.coins += 10;
            Debug.Log("You got 10 coins from the Reward Wheel!");
            WWWForm formPostCoinActivity = new WWWForm();
            WWW wwwPostCoinActivity = new WWW("http://103.239.222.212/ALIVE2Service/api/game/PostActivity?ActivityTypeName=" + "Player Coins&" + "username=" + Login.tnameField.text + "&ActivityDataValue=" + "Player Coins", formPostCoinActivity);
            yield return wwwPostCoinActivity;
            Debug.Log(wwwPostCoinActivity.text);
            Debug.Log(wwwPostCoinActivity.error);
            Debug.Log(wwwPostCoinActivity.url);

            StartCoroutine(Login.UpdateCoins());
        }
        //when wheel stops at 500 coins (angles based on wheel)
        else if (stoppedAngle <= 252)
        {
            daily = "500 Coins";
            Player.coins += 500;
            Debug.Log("You got 500 coins from the Reward Wheel!");
            WWWForm formPostCoinActivity = new WWWForm();
            WWW wwwPostCoinActivity = new WWW("http://103.239.222.212/ALIVE2Service/api/game/PostActivity?ActivityTypeName=" + "Player Coins&" + "username=" + Login.tnameField.text + "&ActivityDataValue=" + "Player Coins", formPostCoinActivity);
            yield return wwwPostCoinActivity;
            Debug.Log(wwwPostCoinActivity.text);
            Debug.Log(wwwPostCoinActivity.error);
            Debug.Log(wwwPostCoinActivity.url);

            StartCoroutine(Login.UpdateCoins());
        }
        //when wheel stops at 50 coins (angles based on wheel)
        else if (stoppedAngle <= 324)
        {
            daily = "50 Coins";
            Player.coins += 50;
            Debug.Log("You got 50 coins from the Reward Wheel!");
            WWWForm formPostCoinActivity = new WWWForm();
            WWW wwwPostCoinActivity = new WWW("http://103.239.222.212/ALIVE2Service/api/game/PostActivity?ActivityTypeName=" + "Player Coins&" + "username=" + Login.tnameField.text + "&ActivityDataValue=" + "Player Coins", formPostCoinActivity);
            yield return wwwPostCoinActivity;
            Debug.Log(wwwPostCoinActivity.text);
            Debug.Log(wwwPostCoinActivity.error);
            Debug.Log(wwwPostCoinActivity.url);

            StartCoroutine(Login.UpdateCoins());
        }
        //when wheel stops at 100 coins (angles based on wheel)
        else if (stoppedAngle <= 360)
        {
            daily = "100 Coins";
            Player.coins += 100;
            Debug.Log("You got 100 coins from the Reward Wheel!");
            WWWForm formPostCoinActivity = new WWWForm();
            WWW wwwPostCoinActivity = new WWW("http://103.239.222.212/ALIVE2Service/api/game/PostActivity?ActivityTypeName=" + "Player Coins&" + "username=" + Login.tnameField.text + "&ActivityDataValue=" + "Player Coins", formPostCoinActivity);
            yield return wwwPostCoinActivity;
            Debug.Log(wwwPostCoinActivity.text);
            Debug.Log(wwwPostCoinActivity.error);
            Debug.Log(wwwPostCoinActivity.url);

            StartCoroutine(Login.UpdateCoins());
        }

        //change text to daily won
        prizeText.text = daily;

        //if (Input.GetMouseButtonDown(0))
        //{
        //    menu.SetActive(false);
        //}

        Player.Save();
    }
}
