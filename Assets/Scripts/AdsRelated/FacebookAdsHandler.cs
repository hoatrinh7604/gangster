using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
//MONSTER REM using AudienceNetwork;
using UnityEngine.SceneManagement;

public class FacebookAdsHandler : MonoBehaviour
{
    #region "Getter"
    private static FacebookAdsHandler _instance;
    public static FacebookAdsHandler Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<FacebookAdsHandler>();
            }
            return _instance;
        }
    }
    #endregion

    //MONSTER REM private InterstitialAd interstitialAd;
    private bool isLoaded;
#pragma warning disable 0414
    private bool didClose;
#pragma warning restore 0414

    void Awake()
    {
        _instance = this;
    }
    void Start ()
    {
		
	}
	
	void Update ()
    {
		
	}
    public void LoadInterstitial()
    {
        //MONSTER REM BEGIN
        // Create the interstitial unit with a placement ID (generate your own on the Facebook app settings).
        // Use different ID for each ad placement in your app.
        //        this.interstitialAd = new InterstitialAd(AdsManager.Instance.facebookIntersitialID); 
        //        this.interstitialAd.Register(this.gameObject);

        //        // Set delegates to get notified on changes or when the user interacts with the ad.
        //        this.interstitialAd.InterstitialAdDidLoad = (delegate () {
        //            Debug.Log("Interstitial ad loaded.");
        //            this.isLoaded = true;
        //            this.didClose = false;

        //        });
        //        interstitialAd.InterstitialAdDidFailWithError = (delegate (string error) {
        //            Debug.Log("Interstitial ad failed to load with error: " + error);
        //        });
        //        interstitialAd.InterstitialAdWillLogImpression = (delegate () {
        //            Debug.Log("Interstitial ad logged impression.");

        //        });
        //        interstitialAd.InterstitialAdDidClick = (delegate () {
        //            Debug.Log("Interstitial ad clicked.");
        //        });

        //        this.interstitialAd.interstitialAdDidClose = (delegate () {
        //            Debug.Log("Interstitial ad did close.");
        //            this.didClose = true;
        //            if (this.interstitialAd != null)
        //            {
        //                this.interstitialAd.Dispose();
        //            }
        //        });

        //#if UNITY_ANDROID
        //        /*
        //         * Only relevant to Android.
        //         * This callback will only be triggered if the Interstitial activity has
        //         * been destroyed without being properly closed. This can happen if an
        //         * app with launchMode:singleTask (such as a Unity game) goes to
        //         * background and is then relaunched by tapping the icon.
        //         */
        //        this.interstitialAd.interstitialAdActivityDestroyed = (delegate () {
        //            if (!this.didClose)
        //            {
        //                Debug.Log("Interstitial activity destroyed without being closed first.");
        //                Debug.Log("Game should resume.");
        //            }
        //        });
        //#endif

        //        // Initiate the request to load the ad.
        //        this.interstitialAd.LoadAd();
        //MONSTER REM END
    }
    public bool ShowInterstitial()
    {
        bool isAdShownOrLoading = false;
        if (this.isLoaded)
        {
            Debug.Log("facebookInterstetial");
            isAdShownOrLoading = true;
            //MONSTER REM this.interstitialAd.Show();
            this.isLoaded = false;
        }
        else
        {
            Debug.Log("otherfacebookInterstetial");
            AdsManager.Instance.ShowRewardedVideo(RewardType.None);
            isAdShownOrLoading = true;
        }
        return isAdShownOrLoading;
    }
    void OnDestroy()
    {
        //MONSTER REM BEGIN
        //if (this.interstitialAd != null)
        //{
        //    this.interstitialAd.Dispose();
        //}
        //MONSTER REM END
        Debug.Log("InterstitialAdTest was destroyed!");
    }
}
