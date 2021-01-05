﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Match3
{
    public class NotificationManager : MonoBehaviour
    {
        public Text notificationText;
        private string[] sentences = new string[5];  //instantiate an array that has 3 elements

        void Start()
        {
            sentences[0] = "Rest X2!";          //store the three strings into the array
            sentences[1] = "Fruits and Vegetables X2!";
            sentences[2] = "Water X2!";
            sentences[3] = "Running X2!";
            sentences[4] = "None";
        }


        public void DisplayNotification(int index)      //function to change the notification based on the index given in the argument
        {
            notificationText.text = sentences[index];
        }

        public void restBonusText()
        {
            DisplayNotification(0);
        }

        public void foodBonusText()
        {
            DisplayNotification(1);
        }

        public void immunisationBonusText()
        {
            DisplayNotification(2);
        }

        public void exerciseBonusText()
        {
            DisplayNotification(3);
        }

        public void healthyBonusText()
        {
            DisplayNotification(4);
        }
    }
}
