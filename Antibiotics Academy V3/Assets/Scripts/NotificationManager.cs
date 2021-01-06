using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Notifications.Android;

public class NotificationManager : MonoBehaviour
{
    void Start()
    {
        CreateNotifChannel();
        //SendNotification();
    }

    void CreateNotifChannel()
    {
        var c = new AndroidNotificationChannel()
        {
            Id = "notif1",
            Name = "dailyReminder",
            Importance = Importance.High,
            Description = "Reminds the player to login today",
        };
        AndroidNotificationCenter.RegisterNotificationChannel(c);
    }

    public static void SendNotification()
    {
        var notification = new AndroidNotification();
        notification.Title = "Daily Login";
        notification.Text = "You can login now for daily rewards!";
        //Debug.Log("before noti sends");
        //notification.FireTime = System.DateTime.Now.AddSeconds(10);
        notification.FireTime = Login.lastLogin.AddHours(18);
        notification.LargeIcon = "large_icon_1";

        AndroidNotificationCenter.SendNotification(notification, "notif1");
    }
}
