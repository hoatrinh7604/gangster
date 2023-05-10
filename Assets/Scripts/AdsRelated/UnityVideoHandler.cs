using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;

public class UnityVideoHandler : MonoBehaviour {

    #region "Getter"
    private static UnityVideoHandler _instance;
    public static UnityVideoHandler Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = FindObjectOfType<UnityVideoHandler>();
            }
            return _instance;
        }
    }
    #endregion

    private string zoneID = "rewardedVideo";

    void Awake()
    {
        _instance = this;
    }

    void Start()
    {
        Init();
    }

    /// <summary>
    /// Initialize all ads here
    /// </summary>
    void Init()
    {
        //MONSTER REM BEGIN
        //if (Advertisement.isSupported && !Advertisement.isInitialized)
        //{
        //    Advertisement.Initialize(AdsManager.Instance.unityVideoAdID);
        //}
        //MONSTER REM END
    }

    void OnGUI()
    {
#if ADS_TESTING_ON
        if (Advertisement.isInitialized)
        {
            if(GUI.Button(new Rect(10,10,100,60),"Show video"))
            {
                ShowRewardedAd();
            }
        }
#endif
    }

	public bool ShowSkippableAd()
	{
		bool isVideoAvailable = false;

        // If the value of zoneID is an empty string, set the value to null.

        //MONSTER REM BEGIN
        //ShowOptions options = new ShowOptions();
        //options.resultCallback = HandleShowResult;   // Triggered when the ad is closed

        //if (Advertisement.IsReady())
        //{
        //	isVideoAvailable = true;
        //	Advertisement.Show(null, options);
        //}
        //MONSTER REM END

        return isVideoAvailable;
	}


    public bool ShowRewardedAd()
    {
        bool isVideoAvailable = false;

        // If the value of zoneID is an empty string, set the value to null.
        //  The default zone is used when the value of zoneID is null.
        if (string.IsNullOrEmpty(zoneID)) zoneID = null;

        //MONSTER REM BEGIN
        //ShowOptions options = new ShowOptions();
        //options.resultCallback = HandleShowResult;   // Triggered when the ad is closed

        //if (Advertisement.IsReady(zoneID))
        //{
        //    isVideoAvailable = true;
        //    Advertisement.Show(zoneID, options);
        //}else if(Advertisement.IsReady())
        //{
        //    isVideoAvailable = true;
        //    Advertisement.Show(null, options);
        //}
        //MONSTER REM END

        return isVideoAvailable;
    }

	public bool ShowNormalVideodAd()
	{
		bool isVideoAvailable = false;

		// If the value of zoneID is an empty string, set the value to null.
		//  The default zone is used when the value of zoneID is null.
		if (string.IsNullOrEmpty(zoneID)) zoneID = null;

        //MONSTER REM BEGIN
        //ShowOptions options = new ShowOptions();
        //options.resultCallback = HandleShowResult;   // Triggered when the ad is closed

        //if(Advertisement.IsReady())
        //{
        //	isVideoAvailable = true;
        //	Advertisement.Show("video", options);
        //}
        //MONSTER REM END

        return isVideoAvailable;
	}

    //MONSTER REM BEGIN
    //public void HandleShowResult(ShowResult result)
    //{
    //    switch (result)
    //    {
    //        case ShowResult.Finished:
    //            AdsManager.Instance.VideoRewardSuccess();
    //            Debug.Log("The ad was successfully shown.");
    //            break;
    //        case ShowResult.Skipped:
    //            Debug.Log("The ad was skipped before reaching the end.");
    //            break;
    //        case ShowResult.Failed:
    //            Debug.LogError("The ad failed to be shown.");
    //            break;
    //    }
    //}
    //MONSTER REM END

}
