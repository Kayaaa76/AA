using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using SimpleJSON;

namespace Match3
{
    public class NotificationManager : MonoBehaviour
    {
        static bool showInfoNugget = false;

        public Text notificationText;
        private string[] sentences = new string[10];  //instantiate an array that has 5 elements

        public GameObject m3levelselect;
        int m3notification;
        public bool isSwitching;

        string[] advices;

        bool toutdated = false;

        void Start()
        {
            toutdated = Login.outdated;
            sentences[0] = "Rest X2!";          //store the five strings into the array
            sentences[1] = "Fruits and Vegetables X2!";
            sentences[2] = "Water X2!";
            sentences[3] = "Running X2!";
            sentences[4] = "No Bonus";

            sentences[5] = "Getting at least 7 hours of quality sleep helps us to stay physically healthy";
            sentences[6] = "A balanced diet which includes some fruits and vegetables builds a strong immune system for the body";
            sentences[7] = "Boost your immunity by taking vaccines to protect yourself against viruses (e.g. flu vaccine)";
            sentences[8] = "Exercise helps keep our bodies healthy, improving energy levels, and relieves stress";
            sentences[9] = "Quality sleep, a balanced diet, updated immunization, and exercise keep us healthy";

            UpdateInfoNuggets();
            sentences[5] = advices[0];
            sentences[6] = advices[1];

            //sentences[5-9] = advices[0-1]
            if (toutdated == true)
            {
                Debug.Log("Updating info nuggets");
                UpdateInfoNuggets();
                sentences[5] = advices[0];
                sentences[6] = advices[1];
            }
            else if (toutdated == false)
            {
                Debug.Log("Info Nuggets are up to date");
            }
        }

        void Update()
        {
            m3notification = m3levelselect.GetComponent<M3LevelSelect>().notification;

            if (!isSwitching)
            {
                StartCoroutine(SwitchingTexts());
            }
            if (showInfoNugget == true)
            {
                StartCoroutine(PostInfoNuggetActivity());
            }

            if (Input.GetKeyDown(KeyCode.U))
            {
                Debug.Log("U is pressed");
                UpdateInfoNuggets();
            }
        }

        public void DisplayNotification(int index)      //function to change the notification based on the index given in the argument
        {
            notificationText.text = sentences[index];
        }

        IEnumerator SwitchingTexts()
        {
            isSwitching = true;
            //Debug.Log(m3notification+"start");

            DisplayNotification(m3notification);
            yield return new WaitForSeconds(5);

            //notificationText.text = "The Info Nugget";
            DisplayNotification(m3notification + 5);
            showInfoNugget = true;
            yield return new WaitForSeconds(5);

            isSwitching = false;
            //Debug.Log(m3notification + "end");
        }

        IEnumerator PostInfoNuggetActivity()
        {
            showInfoNugget = false;

            WWWForm formPostInfoNuggetActivity = new WWWForm();
            WWW wwwPostInfoNuggetActivity = new WWW("http://103.239.222.212/ALIVE2Service/api/game/PostActivity?ActivityTypeName=" + "InfoNugget&" + "username=" + Login.tnameField.text + "&ActivityDataValue=" + "InfoNugget", formPostInfoNuggetActivity);
            yield return wwwPostInfoNuggetActivity;
            Debug.Log(wwwPostInfoNuggetActivity.text);
            Debug.Log(wwwPostInfoNuggetActivity.error);
            Debug.Log(wwwPostInfoNuggetActivity.url);

        }

        void UpdateInfoNuggets()
        {
            advices = new string[2];

            string infoString1 = File.ReadAllText(Application.persistentDataPath + "/InfoNug 1.json");
            JSONObject infoNugget1 = (JSONObject)JSON.Parse(infoString1);
            string infoString2 = File.ReadAllText(Application.persistentDataPath + "/InfoNug 2.json");
            JSONObject infoNugget2 = (JSONObject)JSON.Parse(infoString2);

            advices[0] = infoNugget1["infoNuggetValue"];
            advices[1] = infoNugget2["infoNuggetValue"];

            //Debug.Log(advices[0] + "\n" + advices[1]);
        }
    }
}
