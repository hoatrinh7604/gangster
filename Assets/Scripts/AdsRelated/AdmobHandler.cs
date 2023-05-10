using UnityEngine;
//using admob;
using GoogleMobileAds.Api;
using System;

public class AdmobHandler : MonoBehaviour {

    #region "Getter"
    private static AdmobHandler _instance;
    public static AdmobHandler Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = FindObjectOfType<AdmobHandler>();
            }
            return _instance;
        }
    }
    #endregion

    public delegate void VideoAdLoadStatus(bool success);
    public static event VideoAdLoadStatus VideoAdLoadStatusEvent;

    InterstitialAd interstitial;
    RewardBasedVideoAd rewardBasedVideo;
    private bool isLoading, showOnLoad;

    private bool videoWatchedSuceesfully;

    void Awake()
    {
        _instance = this;
    }

    void Start()
    {
        Init();
    }

    void Init()
    {
        rewardBasedVideo = RewardBasedVideoAd.Instance;

        
        rewardBasedVideo.OnAdLoaded += rewardBasedVideoOnLoad;
        rewardBasedVideo.OnAdRewarded += rewardBasedVideoOnReward;
        rewardBasedVideo.OnAdFailedToLoad += rewardBasedVideoFailedToLoad;
    }


    public void RequestInterstitial()
    {
        // Initialize an InterstitialAd.
        interstitial = new InterstitialAd(AdsManager.Instance.admobIntersitialID);
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        isLoading = true;
        interstitial.LoadAd(request);
        interstitial.OnAdLoaded += interstitialOnLoad;

        AdsManager.Instance.canRequestAd = false;
    }

    private void interstitialOnLoad(object sender, EventArgs e)
    {
        Debug.Log("Ad loaded " + e.ToString());
        isLoading = false;
        if(showOnLoad)
        {
            ShowIntersitial();
        }
        showOnLoad = false;
    }

    void OnGUI()
    {
#if ADS_TESTING_ON
        if (GUI.Button(new Rect(10, 80, 100, 60), "Show Intersitial"))
        {
            RequestInterstitial();
        }

        if (GUI.Button(new Rect(120, 80, 100, 60), "Show Reward admob"))
        {
            RequestRewardBasedVideo();
        }
#endif
    }

	public bool ShowIntersitial()
	{
		bool isAdShownOrLoading = false;
		if (interstitial.IsLoaded())
		{
			isAdShownOrLoading = true;
			interstitial.Show();
			interstitial.OnAdLoaded -= interstitialOnLoad;
			StartCoroutine(AdsManager.Instance.MakeRequestAdAvailable());
		}
		else
		{
			if(isLoading)
			{
				isAdShownOrLoading = true;
				showOnLoad = true;
			}
		}
		return isAdShownOrLoading;
	}
    public void RequestRewardBasedVideo()
    {
        AdRequest request = new AdRequest.Builder().Build();
        rewardBasedVideo.LoadAd(request, AdsManager.Instance.admobVideoID);
        BigCodeLibHandler.Instance.ShowLoading("Please Wait","Loading video");
    }

    private void rewardBasedVideoOnReward(object sender, Reward e)
    {
        videoWatchedSuceesfully = true;
    }

    private void rewardBasedVideoOnLoad(object sender, EventArgs e)
    {
        Debug.Log("Ad loaded rewarded video " + e.ToString());
        BigCodeLibHandler.Instance.DismissLoading();
        if (rewardBasedVideo.IsLoaded())
        {
            rewardBasedVideo.Show();
        }

        if(VideoAdLoadStatusEvent != null)
        {
            VideoAdLoadStatusEvent(true);
        }
    }

    private void rewardBasedVideoFailedToLoad(object sender, AdFailedToLoadEventArgs e)
    {
        BigCodeLibHandler.Instance.DismissLoading();
        if (VideoAdLoadStatusEvent != null)
        {
            VideoAdLoadStatusEvent(false);
        }
    }

    private void OnApplicationPause(bool pause)
    {
        if(!pause)
        {
            if(videoWatchedSuceesfully)
            {
              AdsManager.Instance.VideoRewardSuccess();
            }
            videoWatchedSuceesfully = false;
        }
    }
    public bool IsRewardedVideoLoaded()
    {
        bool isloaded = false;
        if (rewardBasedVideo.IsLoaded())
        {
            isloaded = true;
        }
        else
        {
            isloaded = false;
        }
        return isloaded;
    }
}
