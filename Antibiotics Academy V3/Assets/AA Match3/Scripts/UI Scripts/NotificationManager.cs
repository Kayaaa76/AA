using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Match3
{
    public class NotificationManager : MonoBehaviour
    {
        public Text notificationText;
        private string[] sentences = new string[5];  //instantiate an array that has 5 elements

        void Start()
        {
            sentences[0] = "Rest X2!";          //store the five strings into the array
            sentences[1] = "Fruits and Vegetables X2!";
            sentences[2] = "Water X2!";
            sentences[3] = "Running X2!";
            sentences[4] = "None";
        }


        public void DisplayNotification(int index)      //function to change the notification based on the index given in the argument
        {
            notificationText.text = sentences[index];
        }
    }
}
