using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalNotificationHandler : MonoBehaviour {

    public string[] notificationTexts;
    private static string notificationString;
    private static int notificationId;
    private int numberOfNotifications = 7;
    private static List<int> allNotifications = new List<int>();
    private static int currentNotification = 0;

    void Start()
    {
        CancelNotifications();
    }

    public static void CancelNotifications()
    {
        notificationString = PlayerPrefs.GetString(GameConstants.NOTIFICATION_IDS, "");
        string[] notificationIDs = notificationString.Split(new string[] { "#" },System.StringSplitOptions.None);
        for(int i = 0; i < notificationIDs.Length; i++)
        {
            int.TryParse(notificationIDs[i], out notificationId);
            if(notificationId > 0)
            {
                BigCodeLibHandler.Instance.CancelNotification(notificationId);
            }
        }
        PlayerPrefs.SetString(GameConstants.NOTIFICATION_IDS, "");
    }
    public static void CancelNotification(int id)
    {
        BigCodeLibHandler.Instance.CancelNotification(id);
    }

    void ScheduleNotifications()
    {
        for(int i = 1; i <= numberOfNotifications; i++)
        {
            notificationId = 1000 + i;
            notificationString = PlayerPrefs.GetString(GameConstants.NOTIFICATION_IDS, "") +notificationId + "#";
            PlayerPrefs.SetString(GameConstants.NOTIFICATION_IDS,notificationString );
            BigCodeLibHandler.Instance.SetLocalNotification(notificationId, DaysToMilliSeconds(i), notificationTexts[Random.Range(0, notificationTexts.Length)]);
        }
    }

    public static int ScheduleNotification(long timeInSeconds, string notificationText)
    {
        currentNotification++;
        notificationString = PlayerPrefs.GetString(GameConstants.NOTIFICATION_IDS, "") + currentNotification + "#";
        PlayerPrefs.SetString(GameConstants.NOTIFICATION_IDS, notificationString);
        BigCodeLibHandler.Instance.SetLocalNotification(currentNotification, SecondsToMilliSeconds(timeInSeconds), notificationText);
        return currentNotification;
    }


    void OnApplicationPause(bool isPaused)
    {
        if (!isPaused)
        {
            CancelNotifications();
        }else
        {
            ScheduleNotifications();
        }
    }

  
    public static long SecondsToMilliSeconds(long seconds)
    {
        return seconds * 1000;
    }

    public static long MinutesToSeconds(long minutes)
    {
        return minutes * 60;
    }

    public static long HoursToMinutes(long hours)
    {
        return hours * 60;
    }
    public static long DaysToHours(long days)
    {
        return days * 24;
    }

    public static long HoursToMilliSeconds(long hours)
    {
        return SecondsToMilliSeconds(MinutesToSeconds(HoursToMinutes(hours)));
    }

    public static long DaysToMilliSeconds(long days)
    {
        return SecondsToMilliSeconds(MinutesToSeconds(HoursToMinutes(DaysToHours(days))));
    }
}
