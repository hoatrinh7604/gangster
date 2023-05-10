using UnityEngine;
using UnityEngine.UI;
using System.Collections;
//MONSTER REM using Facebook.Unity;
using System.Collections.Generic;
public class MoreGamesPopupHandler : MonoBehaviour {

    public GameObject MoreGameButton;

    public static bool isInstallingMoreGames;


    public static MoreGamesPopupHandler Instance
    {
        get
        {
            return _instance;
        }
    }
    private static MoreGamesPopupHandler _instance;

    void Awake()
    {
        _instance = this;
        SetMoreGamesButton();
    }

    void Start()
    {
        AdsManager.Instance.HandleMoreGamesDownload();
    }

    public void SetMoreGamesButton()
    {
        if (!string.IsNullOrEmpty(AdsManager.Instance.currentPackageMoreGame) && AdsManager.Instance.moreGamesPopupTexture != null)
        {
            MoreGameButton.SetActive(true);
            if (AdsManager.Instance.moreGamesPopupBundleIdsValues[AdsManager.Instance.currentPackageMoreGame] > 0)
            {
                MoreGameButton.GetComponentInChildren<Text>().enabled = true;
                MoreGameButton.GetComponentInChildren<Text>().text = "Download & get " + AdsManager.Instance.moreGamesPopupBundleIdsValues[AdsManager.Instance.currentPackageMoreGame] + " Coins";
            }else
            {
                MoreGameButton.GetComponentInChildren<Text>().enabled = false;
            }
            MoreGameButton.GetComponentInChildren<Image>().sprite = Sprite.Create(AdsManager.Instance.moreGamesPopupTexture,
                new Rect(0, 0, AdsManager.Instance.moreGamesPopupTexture.width, AdsManager.Instance.moreGamesPopupTexture.height), Vector2.zero);
        }
        else
        {
            MoreGameButton.SetActive(false);
        }
    }

   

    public void OpenGame()
    {
        isInstallingMoreGames = true;
#if UNITY_EDITOR
        Application.OpenURL("https://play.google.com/store/apps/details?id=" + AdsManager.Instance.currentPackageMoreGame);
#elif UNITY_ANDROID
        Application.OpenURL("market://details?id=" + AdsManager.Instance.currentPackageMoreGame);
#endif
        //FirebaseAnalyticsHandler.TrackEvent("FreeGames1");

        var clickedmoregamesname = new Dictionary<string, object>();
        clickedmoregamesname["FreeGames1_packagename"] = AdsManager.Instance.currentPackageMoreGame;
        //MONSTER REM FB.LogAppEvent("FreeGames1", null, clickedmoregamesname);

    }

}


