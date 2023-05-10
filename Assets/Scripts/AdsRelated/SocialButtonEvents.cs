using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SocialButtonEvents : MonoBehaviour
{

    public Button[] socialButtons;


    public static string[] socialButtonStatus = new string[] { "liked", "subscribed", "followed", "instagramFollowed" };
    public static int[] socialBonus = new int[] { 1000, 1000, 1000, 100 };

    private string socailText = "{0} for {1} coins";
    private string[] targetTexts = new string[] { "Like", "Subscribe", "Follow", "Follow" };

    public static SocialButtonEvents Instance
    {
        get
        {
            return _instance;
        }
    }
    private static SocialButtonEvents _instance;

    void Awake()
    {
        _instance = this;
    }



    public void Subscribe()
    {
        Application.OpenURL("https://www.youtube.com/c/BigCodeGames");
        SetStatus(socialButtonStatus[(int)SocialEnumType.Subscribe], true);
        StartCoroutine(BonusCash(socialBonus[(int)SocialEnumType.Subscribe]));
    }

    public void Follow()
    {
        Application.OpenURL("https://twitter.com/mtstudio2015");
        SetStatus(socialButtonStatus[(int)SocialEnumType.Follow], true);
        StartCoroutine(BonusCash(socialBonus[(int)SocialEnumType.Follow]));
    }

    public void LikeGame()
    {
        Application.OpenURL("https://www.facebook.com/mtsfreegames/"); //

        SetStatus(socialButtonStatus[(int)SocialEnumType.Like], true);
        //Invoke ("BonusCash", 1);
        StartCoroutine(BonusCash(socialBonus[(int)SocialEnumType.Like]));
    }

    public void InstagramFollow()
    {
        Application.OpenURL("https://www.instagram.com/bigcodegames/"); //

        SetStatus(socialButtonStatus[(int)SocialEnumType.InstagramFollow], true);
        //Invoke ("BonusCash", 1);
        StartCoroutine(BonusCash(socialBonus[(int)SocialEnumType.InstagramFollow]));
    }


    IEnumerator BonusCash(int coins)
    {
        yield return new WaitForSeconds(2f);
        AdsManager.Instance.AddCoins(coins, true);
    }

    public void SetStatus(string str, bool status)
    {
        PlayerPrefs.SetString(str, status.ToString());
        SetBtnEnableStatus();
    }

    void SetBtnEnableStatus()
    {
        for (int i = 0; i < socialButtons.Length; i++)
        {
            if (PlayerPrefs.HasKey(socialButtonStatus[i]) && bool.Parse(PlayerPrefs.GetString(socialButtonStatus[i])))
            {
                socialButtons[i].gameObject.SetActive(false);
            }
            else
            {
                socialButtons[i].gameObject.SetActive(true);
                socialButtons[i].GetComponentInChildren<Text>().text = string.Format(socailText, targetTexts[i], socialBonus[i]);
            }
        }
    }
}

public enum SocialEnumType
{
    Like = 0,
    Subscribe,
    Follow,
    InstagramFollow
}
