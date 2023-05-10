using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IronSourceAdsHandler : MonoBehaviour
{
    #region "Getter"
    private static IronSourceAdsHandler _instance;
    public static IronSourceAdsHandler Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<IronSourceAdsHandler>();
            }
            return _instance;
        }
    }
    #endregion
    void Awake()
    {
        _instance = this;
    }
    void Start ()
    {
        //IronSource.Agent.reportAppStarted();
        IronSourceConfig.Instance.setClientSideCallbacks(true);
        string id = IronSource.Agent.getAdvertiserId();
        IronSource.Agent.setUserId(id);
        
        IronSource.Agent.init(AdsManager.Instance.ironSourceID);
        //IronSource.Agent.validateIntegration();

        // Add Interstitial Events
        IronSourceEvents.onInterstitialAdReadyEvent += InterstitialAdReadyEvent;
        IronSourceEvents.onInterstitialAdLoadFailedEvent += InterstitialAdLoadFailedEvent;
        IronSourceEvents.onInterstitialAdShowSucceededEvent += InterstitialAdShowSucceededEvent;
        IronSourceEvents.onInterstitialAdShowFailedEvent += InterstitialAdShowFailEvent;
        IronSourceEvents.onInterstitialAdClickedEvent += InterstitialAdClickedEvent;
        IronSourceEvents.onInterstitialAdOpenedEvent += InterstitialAdOpenedEvent;
        IronSourceEvents.onInterstitialAdClosedEvent += InterstitialAdClosedEvent;
        // Add Rewarded Interstitial Events
        IronSourceEvents.onInterstitialAdRewardedEvent += InterstitialAdRewardedEvent;

        //Add Rewarded Video Events
        IronSourceEvents.onRewardedVideoAdOpenedEvent += RewardedVideoAdOpenedEvent;
        IronSourceEvents.onRewardedVideoAdClosedEvent += RewardedVideoAdClosedEvent;
        IronSourceEvents.onRewardedVideoAvailabilityChangedEvent += RewardedVideoAvailabilityChangedEvent;
        IronSourceEvents.onRewardedVideoAdStartedEvent += RewardedVideoAdStartedEvent;
        IronSourceEvents.onRewardedVideoAdEndedEvent += RewardedVideoAdEndedEvent;
        IronSourceEvents.onRewardedVideoAdRewardedEvent += RewardedVideoAdRewardedEvent;
        IronSourceEvents.onRewardedVideoAdShowFailedEvent += RewardedVideoAdShowFailedEvent;

        //LoadInterstitial();
    }
	
	void Update ()
    { 

	}
    //for the ironsource interstetial events
    public void LoadInterstitial()
    {
        IronSource.Agent.loadInterstitial();
    }

    public bool ShowInterstitial()
    {
        bool isVideoAvailable = false;
        if (IronSource.Agent.isInterstitialReady())
        {
            isVideoAvailable = true;
            IronSource.Agent.showInterstitial();
            Invoke("LoadInterstitial", 2);
        }
        else
        {
            isVideoAvailable = false;
            Debug.Log("IronSource.Agent.isInterstitialReady - False");
        }
        return isVideoAvailable;
    }

    void InterstitialAdReadyEvent()
    {
    }

    void InterstitialAdLoadFailedEvent(IronSourceError error)
    {
    }

    void InterstitialAdShowSucceededEvent()
    {
        Debug.Log("I got InterstitialAdShowSucceededEvent");
    }

    void InterstitialAdShowFailEvent(IronSourceError error)
    {
    }

    void InterstitialAdClickedEvent()
    {
    }

    void InterstitialAdOpenedEvent()
    {
    }

    void InterstitialAdClosedEvent()
    {
    }

    void InterstitialAdRewardedEvent()
    {
    }
    //up to here


    //for the ironsource rewardedvideo events
    public bool ShowRewardedVideo()
    {
        bool isVideoAvailable = false;
        Debug.Log("ShowRewardedVideoButtonClicked");
        if (IronSource.Agent.isRewardedVideoAvailable())
        {
            isVideoAvailable = true;
            IronSource.Agent.showRewardedVideo();
        }
        else
        {
            isVideoAvailable = false;
            Debug.Log("IronSource.Agent.isRewardedVideoAvailable - False");
        }
        return isVideoAvailable;
    }

    void RewardedVideoAvailabilityChangedEvent(bool canShowAd)
    {
        if (canShowAd)
        {
        }
        else
        {
        }
    }

    void RewardedVideoAdOpenedEvent()
    {
    }

    void RewardedVideoAdRewardedEvent(IronSourcePlacement ssp)
    {
        //Debug.Log("I got RewardedVideoAdRewardedEvent, amount = " + ssp.getRewardAmount() + " name = " + ssp.getRewardName());
        AdsManager.Instance.VideoRewardSuccess();
    }

    void RewardedVideoAdClosedEvent()
    {
    }

    void RewardedVideoAdStartedEvent()
    {
    }

    void RewardedVideoAdEndedEvent()
    {
    }

    void RewardedVideoAdShowFailedEvent(IronSourceError error)
    {
    }

    void OnApplicationPause(bool isPaused)
    {
        IronSource.Agent.onApplicationPause(isPaused);
    }
}
