using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
//MONSTER REM using Facebook.Unity;
public class MoreGames2PopupHandler : MonoBehaviour
{
    public GameObject MoreGame2Button;

    public static bool isInstallingMoreGames2;

    public static MoreGames2PopupHandler Instance
    {
        get
        {
            return _instance;
        }
    }
    private static MoreGames2PopupHandler _instance;

    void Awake()
    {
        _instance = this;
        SetMoreGames2Button();
    }

    void Start ()
    {
	
	}

	void Update ()
    {
	
	}
    public void SetMoreGames2Button()
    {
        if (AdsManager.Instance.moreGames2PopupTexture != null)
        {
            MoreGame2Button.SetActive(true);
           
            MoreGame2Button.GetComponentInChildren<Image>().sprite = Sprite.Create(AdsManager.Instance.moreGames2PopupTexture,
                new Rect(0, 0, AdsManager.Instance.moreGames2PopupTexture.width, AdsManager.Instance.moreGames2PopupTexture.height), Vector2.zero);
        }
        else
        {
            MoreGame2Button.SetActive(false);
        }
    }
    public void OpenGame()
    {
        Application.OpenURL(AdsManager.Instance.freeGamesPackage);

        //FirebaseAnalyticsHandler.TrackEvent("FreeGames2");

        var clickedmoregamesname = new Dictionary<string, object>();
        clickedmoregamesname["FreeGames2_packagename"] = AdsManager.Instance.freeGamesPackage;
        //MONSTER REM FB.LogAppEvent("FreeGames2", null, clickedmoregamesname);
    }
}
