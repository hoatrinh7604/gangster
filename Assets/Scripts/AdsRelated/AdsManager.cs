using UnityEngine;
using System.Collections;
using GameAnalyticsSDK;
public class AdsManager : ReadXML {

    #region "Getter"
    private static AdsManager _instance;
    public static AdsManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<AdsManager>();
            }
            return _instance;
        }
    }
    #endregion

    private string alternativeAdsString = string.Empty;
    protected internal bool canRequestAd = true;
    private ReachedPage currentPage = ReachedPage.None;
    private RewardType currentRewardType = RewardType.None;
    private AlertType alertType = AlertType.None;

    public delegate void CoinsAdded(int total);
    public static event CoinsAdded CoinsAddedEvent;
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        //MONSTER REM AdmobHandler.VideoAdLoadStatusEvent += AdmobVideoHandler;
    }

    void Start()
    {
        //MONSTER REM StartCoroutine(LoadBaseXMLData());
        if (!PlayerPrefs.HasKey(GameConstants.INSTALLCOUNT_INCREASE))
        {
           //StartCoroutine(IncreaseInstallCount());
        }

        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        //MONSTER REM GameAnalytics.Initialize();
    }

    #region "Event Handlers"
    public void PurchaseSuccess(string productId)
    {
        // To Purchase No Ads call PurchaseNoAds()
        if (productId == productIds[0].id)
        {
            PurchasedNoAds();
        }
        else if (productId == productIds[1].id)
        {
            // GameManager.UnlockAllVehicles();
            PlayerPrefs.SetInt("LevelUnlock", 10);
            PlayerPrefs.SetInt("Level1Unlock", 1);
            PlayerPrefs.SetInt("Level2Unlock", 1);
            PlayerPrefs.SetInt("Level3Unlock", 1);
            PlayerPrefs.SetInt("Level4Unlock", 1);
            PlayerPrefs.SetInt("Level5Unlock", 1);
            PlayerPrefs.SetInt("Level6Unlock", 1);
            PlayerPrefs.SetInt("Level7Unlock", 1);
            PlayerPrefs.SetInt("Level8Unlock", 1);
            PlayerPrefs.SetInt("Level9Unlock", 1);
            PlayerPrefs.SetInt("Level10Unlock", 1);
        }
        else if (productId == productIds[2].id) //10000
        {
            //  GameManager.AddCash(10000);
            PlayerPrefs.SetInt("Sachin", 3);
        }
        else if (productId == productIds[3].id) //20000
        {
            //  GameManager.AddCash(20000);
            PlayerPrefs.SetInt("Sachin", 3);
            PlayerPrefs.SetInt("LevelUnlock", 10);
            PlayerPrefs.SetInt("Level1Unlock", 1);
            PlayerPrefs.SetInt("Level2Unlock", 1);
            PlayerPrefs.SetInt("Level3Unlock", 1);
            PlayerPrefs.SetInt("Level4Unlock", 1);
            PlayerPrefs.SetInt("Level5Unlock", 1);
            PlayerPrefs.SetInt("Level6Unlock", 1);
            PlayerPrefs.SetInt("Level7Unlock", 1);
            PlayerPrefs.SetInt("Level8Unlock", 1);
            PlayerPrefs.SetInt("Level9Unlock", 1);
            PlayerPrefs.SetInt("Level10Unlock", 1);
        }
        else if (productId == productIds[4].id) //50000
        {
            //  GameManager.AddCash(50000);
            PlayerPrefs.SetInt("TotalMoney", PlayerPrefs.GetInt("TotalMoney") + 150000);
        }
        else if (productId == productIds[5].id) //75000
        {
            //  GameManager.AddCash(75000);
            PlayerPrefs.SetInt("TotalMoney", PlayerPrefs.GetInt("TotalMoney") + 300000);
        }
        else if (productId == productIds[6].id) //100000
        {
            //  GameManager.AddCash(100000);
        }
        else if (productId == productIds[8].id) //unlock 6 vehicles
        {
            Debug.Log("Unlock6Vehicles");

        }
        else if (productId == productIds[9].id) //unlock 20 vehicles
        {
            //GameManager.AddCash(100000);


        }

        Debug.Log("purchased successfully : " + productId);


    }

    void PurchasedNoAds()
    {
        PlayerPrefs.SetString(GameConstants.NOADS, "success");
    }

    private bool isPurchasedNoads()
    {
        if(PlayerPrefs.HasKey(GameConstants.NOADS))
        {
            return true;
        }
        return false;
    }

    #endregion

    IEnumerator IncreaseInstallCount()
    {
#if UNITY_5_6_OR_NEWER
        WWW www = new WWW("http://mygameshare.com/gdgbk/" + Application.identifier);
#else
        WWW www = new WWW("http://mygameshare.com/gdgbk/" + Application.bundleIdentifier);
#endif
        yield return www;
        if(www.error == null)
        {
            Debug.Log("Download count increased Successfully");
            PlayerPrefs.SetString(GameConstants.INSTALLCOUNT_INCREASE, "Increased");
        }
        else
        {
            Debug.Log("Download count couldn't be increased");
        }

    }

    /// <summary>
    /// Check whether game installed in the mobile , In editor returns false
    /// </summary>
    /// <param name="packageName"></param>
    /// <returns></returns>
    public bool isGameInstalled(string packageName)
    {
#if UNITY_EDITOR
        return false;
#elif UNITY_ANDROID
        return BigCodeLibHandler.Instance.IsGameInstalled(packageName);
#endif
    }

    /// <summary>
    /// Add coins on success events
    /// </summary>
    /// <param name="addCoins"></param>
    /// <param name="showToast"></param>
    public void AddCoins(int addCoins, bool showToast = false)
    {
        int coinsHave = PlayerPrefs.GetInt(GameConstants.COINS_PLAYERPREF, 0);
        coinsHave += addCoins;
        PlayerPrefs.SetInt(GameConstants.COINS_PLAYERPREF, coinsHave);
        if (showToast && (addCoins > 0))
        {
            ShowToast(addCoins + " Coins added successfully");
        }
        if(CoinsAddedEvent != null)
        {
            CoinsAddedEvent(coinsHave);
        }

    }

#region "Alert dialog"
    /// <summary>
    /// To show alert/success messages in popup format
    /// </summary>
    /// <param name="msg"></param>
    /// <param name="yesButtonName"></param>
    /// <param name="noButtonName"></param>
    public void ShowAlertDialog(string msg, string yesButtonName, string noButtonName = null, AlertType currentAlert = AlertType.None)
    {
        alertType = currentAlert;
        if(string.IsNullOrEmpty(noButtonName))
        {
            noButtonName = string.Empty;
        }
        //MONSTER REM BigCodeLibHandler.Instance.ShowAlert(Application.productName, msg, yesButtonName, noButtonName, false);
    }
    /// <summary>
    /// Same like AlertDialog additionally player can enter text, used for PromoCode, Callback comes in PromoCallback method
    /// </summary>
    /// <param name="msg"></param>
    /// <param name="yesButtonName"></param>
    /// <param name="noButtonName"></param>
    public void ShowDialogWithEdittext(string msg,string yesButtonName, string noButtonName)
    {
        //MONSTER REM BigCodeLibHandler.Instance.ShowAlert(Application.productName, msg, yesButtonName, noButtonName, true);
    }

    public void ShowPromoPopup()
    {
        ShowDialogWithEdittext("Enter promo code", "Ok", "Cancel");
    }
    
    void PromoCallback(string enteredCode)
    {
        // Check whether entered string matches promo code or not. If it matches unlock specified
        Debug.Log("Promocode entered is :: " + enteredCode);
        if(!string.IsNullOrEmpty(promoSuccessCode) && enteredCode.Equals(promoSuccessCode))
        {
            // Entered correct promo
            if (!PlayerPrefs.GetString(GameConstants.PROMOS_USED, string.Empty).Contains("_" + promoSuccessCode))
            {
                PlayerPrefs.SetString(GameConstants.PROMOS_USED, PlayerPrefs.GetString(GameConstants.PROMOS_USED) + "_" + promoSuccessCode);
                PromoSuccess();
                ShowToast("Promo code applied successfully");
            }
            else
            {
                ShowToast("Promo code is valid for one time");
            }
        }
        else
        {
            ShowToast("Invalid code entered");
        }
    }

    void PromoSuccess()
    {
        // Promo success here..
    }

   void AlertCallback(string input)
    {
        Debug.Log("Alert dialog call back :: " + input);

        if (input.Equals("yes"))
        {
            switch(alertType)
            {
                case AlertType.Quit:
                    Application.Quit();
                    break;
                case AlertType.Rate:
                    RateGame();
                    break;
                case AlertType.Share:
                    //MONSTER REM FacebookHandler.Instance.Share();
                    break;
            }

        }else if(input.Equals("no"))
        {

        }
    }

#endregion

#region "Share"

    /// <summary>
    /// to show native share popup
    /// </summary>
    public void ShowShareDialog()
    {
        ShowAlertDialog(shareMessage, "Share", "Cancel", AlertType.Share);
    }

    public void ShowShareDialog(int currentLevel)
    {
        if (shareLevels.Contains(currentLevel))
        {
            ShowAlertDialog(shareMessage, "Share", "Cancel", AlertType.Share);
        }
    }

    /// <summary>
    /// After sharing
    /// </summary>
    public void ShareCallback()
    {
        if(!IsShared)
        {
            AddCoins(shareBonus);
            PlayerPrefs.SetString(GameConstants.SHARED, "Done");
        }
    }

    /// <summary>
    /// To check whether game has been shared already and got incentive
    /// </summary>
    public bool IsShared
    {
        get
        {
            return PlayerPrefs.HasKey(GameConstants.SHARED);
        }
    }

#endregion

#region "Rate"

    /// <summary>
    /// To show native rate popup
    /// </summary>
    public void ShowRateDialog()
    {
        if (!IsRated)
        {
            ShowAlertDialog(rateMessage, "Ok", "Cancel", AlertType.Rate);
        }
    }

    /// <summary>
    /// To show rate popup, Itself checks whether rate dialog should come in current level or not
    /// </summary>
    /// <param name="currentLevel"></param>
    public void ShowRateDialog(int currentLevel)
    {
        if (rateLevels.Contains(currentLevel) && !IsRated)
        {
            ShowAlertDialog(rateMessage, "Ok", "Cancel", AlertType.Rate);
        }
    }

    /// <summary>
    /// To rate the game
    /// </summary>
    public void RateGame()
    {
#if UNITY_EDITOR
                Application.OpenURL("https://play.google.com/store/apps/details?id=" + Application.identifier);
#elif UNITY_ANDROID
#if UNITY_5_6_OR_NEWER
                Application.OpenURL("market://details?id=" +  Application.identifier);
#else
                Application.OpenURL("market://details?id=" +  Application.bundleIdentifier);
#endif
#endif
        if (!IsRated)
        {
            AddCoins(rateBonus,true);
            PlayerPrefs.SetString(GameConstants.RATED, "Done");
        }
    }

    /// <summary>
    /// To know whether game has been rated already 
    /// </summary>
    public bool IsRated
    {
        get
        {
            return PlayerPrefs.HasKey(GameConstants.RATED);
        }
    }

#endregion

    /// <summary>
    /// To show toast like ShowToast in BigCode
    /// </summary>
    /// <param name="msg"></param>
    public void ShowToast(string msg)
    {
        //MONSTER REM BigCodeLibHandler.Instance.ShowToast(msg);
    }

    /// <summary>
    /// Handles MoreGames popupad one after another from xml
    /// </summary>
    public void HandleMoreGamesDownload()
    {
        if (!isGameInstalled(currentPackageMoreGame))
        {
            return;
        }

        if (!PlayerPrefs.HasKey(DOWNLOADED_GAMES))
        {
            PlayerPrefs.SetString(DOWNLOADED_GAMES, currentPackageMoreGame);
        }
        else
        {
            PlayerPrefs.SetString(DOWNLOADED_GAMES, PlayerPrefs.GetString(DOWNLOADED_GAMES) + "#" + currentPackageMoreGame);
        }
        AddCoins(moreGamesPopupBundleIdsValues[currentPackageMoreGame]);
#if !UNITY_EDITOR && UNITY_ANDROID
        if(moreGamesPopupBundleIdsValues[currentPackageMoreGame] > 0) {
              ShowToast("Thank you for installing, You have earned " + moreGamesPopupBundleIdsValues[currentPackageMoreGame] + " coins");
        }
#endif
        MoreGamesPopupHandler.Instance.MoreGameButton.SetActive(false);
        currentPackageMoreGame = string.Empty;
        moreGamesPopupTexture = null;

        StartCoroutine(SaveMoreGameDetails());
    }

    /// <summary>
    /// To show Video and pass the parameter to handle reward in correct manner
    /// </summary>
    /// <param name="type"></param>
    public void ShowRewardedVideo(RewardType type)
    {
        //MONSTER REM BEGIN
        //currentRewardType = type;
        //if (isUnityVideoOnTop)
        //{
        //    if (((type != RewardType.None) && !UnityVideoHandler.Instance.ShowRewardedAd()) || !UnityVideoHandler.Instance.ShowSkippableAd())
        //    {
        //        if (!VungleAdsHandler.Instance.ShowRewardAd())
        //         {
        //            AdmobHandler.Instance.RequestRewardBasedVideo();
        //         }
        //        //if (AdmobHandler.Instance.IsRewardedVideoLoaded())
        //       // {
        //       //     AdmobHandler.Instance.RequestRewardBasedVideo();
        //       // }
        //       // else
        //      //  {
        //       //     VungleAdsHandler.Instance.ShowRewardAd();
        //       // }

        //    }
        //}
        //else
        //{
        //    AdmobHandler.Instance.RequestRewardBasedVideo();
        //}
        //MONSTER REM END
    }
    void ShowSkippableVideoAd()
    {
        if (!UnityVideoHandler.Instance.ShowSkippableAd())
        {
            if (!VungleAdsHandler.Instance.ShowSkippableAd())
            {
                AdmobHandler.Instance.RequestRewardBasedVideo();
            }
        }
    }
    void AdmobVideoHandler(bool loaded)
    {
        if(!loaded)
        {
            if(isUnityVideoOnTop)
            {
                VungleAdsHandler.Instance.ShowRewardAd();
            }else
            {
                if(!UnityVideoHandler.Instance.ShowRewardedAd())
                {
                    VungleAdsHandler.Instance.ShowRewardAd();
                }
            }
        }
    }


    /// <summary>
    /// Video watched successfullly with out skipping
    /// </summary>
    public void VideoRewardSuccess()
    {
        Debug.Log("VideoRewardSuccess here");
        switch (currentRewardType)
        {
            case RewardType.None:
                break;
            case RewardType.Coins:
                PlayerPrefs.SetInt("TotalMoney", PlayerPrefs.GetInt("TotalMoney") + 1000);
                // GameManager.AddCash(1000);
                //MONSTER REM AdsManager.Instance.ShowToast("1000 Cash Added Successfully");
                // Add coins here, AddCoins method is available in this class
                break;
            case RewardType.Unlock:
                // Unlock related code here
                //  GameManager.DecVideoCount(PurchaseChecr.vname);

                break;
            case RewardType.WatchToResume:
                break;
            case RewardType.DoubleReward:
                PlayerPrefs.SetInt("TotalMoney", PlayerPrefs.GetInt("TotalMoney") + 4000);
                //MONSTER REM AdsManager.Instance.ShowToast("4000 Cash Added Successfully");
                //  GameManager.AddCash(LevelComplete.obj.earnedStuntCash);
                //   LevelComplete.obj.UpdateCash();
                //    AdsManager.Instance.ShowToast(LevelComplete.obj.earnedStuntCash + " Cash Added Successfully!");
                break;
        }
        currentRewardType = RewardType.None;

    }

    /// <summary>
    /// Set Status with delay
    /// </summary>
    /// <param name="page"></param>
    /// <param name="delay"></param>
    /// <param name="levelNum"></param>
    public void SetStatusWithDelay(ReachedPage page,float delay, int levelNum = 0)
    {
        StartCoroutine(SetStatus(page, delay, levelNum));
    }

    IEnumerator SetStatus(ReachedPage page, float delay, int levelNum = 0)
    {
        yield return new WaitForSeconds(delay);
        SetStatus(page, levelNum);
    }

    /// <summary>
    /// Call with page name to set the status, should be called in the pages Menu, InGame, LevelComplete, LevelFail
    /// </summary>
    /// <param name="page"></param>
    /// <param name="levelNum"></param>
    public void SetStatus(ReachedPage page,int levelNum = 0)
    {
        if(page == ReachedPage.Menu)
        {
            if (!PlayerPrefs.HasKey(GameConstants.GAME_OPENED_FIRST_TIME))
            {
                //PlayGameServicesHandler.Instance.AuthenticateOrLogin();
                PlayerPrefs.SetString(GameConstants.GAME_OPENED_FIRST_TIME, "PlayedAlready");
            }
        }

        currentPage = page;
        ShowAds((int)page);

        if(canRequestAd)
        {
            RequestAd();
        }



        if(levelNum > 0)
        {
            //FirebaseAnalyticsHandler.TrackEvent(levelNum, page.ToString());
        }else
        {
            //FirebaseAnalyticsHandler.TrackEvent(page.ToString());
        }

    }


    /// <summary>
    /// Show more games. Externally handled whether to open publisher games or a single game
    /// </summary>
    public void ShowMoreGames()
    {
          if(moreGamesPackage.Contains("com."))
          {
  #if UNITY_EDITOR
              Application.OpenURL("https://play.google.com/store/apps/details?id=" + moreGamesPackage);
  #elif UNITY_ANDROID
              Application.OpenURL("market://details?id=" + moreGamesPackage); 
  #endif
          }
          else
          {
  #if UNITY_EDITOR
              Application.OpenURL("https://play.google.com/store/search?q=pub:" + moreGamesPackage);
  #elif UNITY_ANDROID
              Application.OpenURL("market://search?q=pub:" + moreGamesPackage);
  #endif
          }
        //Application.OpenURL("https://play.google.com/store/search?q=pub:" + "Redcorner Games");
    }


    void ShowAds(int num)
    {
        //MONSTER REM BEGIN
        //if (isPurchasedNoads())
        //{
        //    return;
        //}
        //if (admobPlacementsInfo.Contains(num.ToString()) && otherAdsPlacementsInfo.Contains(num.ToString()))
        //{
        //    if (alternativeAdsString == string.Empty || alternativeAdsString == "admob")
        //    {
        //        //if (AdmobHandler.Instance.ShowIntersitial())
        //        //{
        //        IronSourceAdsHandler.Instance.ShowInterstitial();
        //        alternativeAdsString = "others";
        //        StartCoroutine(MakeRequestAdAvailable());
        //        //}
        //    }
        //    else
        //    {
        //        // show other ads here and request for admob
        //        if (canRequestAd)
        //        {
        //            canRequestAd = false;
        //            alternativeAdsString = "admob";
        //            StartCoroutine(MakeRequestAdAvailable());
        //            //ShowRewardedVideo(RewardType.None);
        //            IronSourceAdsHandler.Instance.ShowInterstitial();
        //        }
        //    }

        //}
        //else if (admobPlacementsInfo.Contains(num.ToString()))
        //{
        //    //AdmobHandler.Instance.ShowIntersitial();
        //    IronSourceAdsHandler.Instance.ShowInterstitial();
        //}
        //else if (otherAdsPlacementsInfo.Contains(num.ToString()))
        //{
        //    // Show other ads here
        //    if (canRequestAd)
        //    {
        //        canRequestAd = false;
        //        StartCoroutine(MakeRequestAdAvailable());
        //        //ShowRewardedVideo(RewardType.None);
        //        IronSourceAdsHandler.Instance.ShowInterstitial();
        //    }
        //}
        //MONSTER REM END
    }

    public IEnumerator MakeRequestAdAvailable()
    {
        yield return new WaitForSeconds(adtoAdDelay);
        canRequestAd = true;
        RequestAd();
    }

    void RequestAd()
    {
        //MONSTER REM BEGIN
        //if (isPurchasedNoads())
        //{
        //    return;
        //}

        //if (currentPage == ReachedPage.InGame)
        //{
        //    if (string.IsNullOrEmpty(alternativeAdsString) || (alternativeAdsString == "admob"))
        //    {
        //        //AdmobHandler.Instance.RequestInterstitial();
        //        IronSourceAdsHandler.Instance.LoadInterstitial();
        //    }
        //    else
        //    {
        //        // Request otherAds
        //        IronSourceAdsHandler.Instance.LoadInterstitial();
        //    }
        //}
        //MONSTER REM END
    }


    void OnApplicationPause(bool isPaused)
    {
        if (!isPaused)
        {
            //MONSTER REM BEGIN
            //if (MoreGamesPopupHandler.isInstallingMoreGames && (MoreGamesPopupHandler.Instance != null))
            //{
            //    HandleMoreGamesDownload();
            //}
            //MoreGamesPopupHandler.isInstallingMoreGames = false;
            //MONSTER REM END
        }
    }
}

public enum ReachedPage
{
    None = 0,
    Menu = 1,
    InGame = 2,
    LevelComplete = 3,
    LevelFail = 4,
    Others = 5
}

public enum RewardType
{
    None = 0,
    Coins = 1,
    Unlock = 2,
    WatchToResume = 3,
    DoubleReward=4,
}

public enum AlertType
{
    None = 0,
    Quit = 1,
    Rate = 2,
    Share = 3
}