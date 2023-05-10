using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class VungleAdsHandler : MonoBehaviour
{

    #region "Getter"
    private static VungleAdsHandler _instance;
    public static VungleAdsHandler Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<VungleAdsHandler>();
            }
            return _instance;
        }
    }
    #endregion

    private Dictionary<string, object> options;

    string placement;

    Dictionary<string, bool> placementsID = new Dictionary<string, bool>
    {
        { "DEFAULT63997", false },
        { "PLMT02I58745", false }

    };

    List<string> placementIdList;
    bool adInited = false;


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


        options = new Dictionary<string, object>();
        options["incentivized"] = true;


        Vungle.onInitializeEvent += Oninit;
        placementIdList = new List<string>(placementsID.Keys);
        string[] array = new string[placementsID.Keys.Count];
        placementsID.Keys.CopyTo(array, 0);
        Vungle.init(AdsManager.Instance.vungleVideoID, array);

        Debug.Log("PlacmentIDDDD______" + placementsID);





        Vungle.onAdFinishedEvent += OnAdFinished;

        //
    }

    private void Oninit()
    {

        adInited = true;
        Debug.Log("INittttttttttttttttttttttttttttt:" + adInited);
        initializeEventHandlers();
        Debug.Log("Vungle sdk initialized");
    }

    private void initializeEventHandlers()
    {
        //Event triggered during when an ad is about to be played
        Vungle.onAdStartedEvent += (placementID) => {
            Debug.Log("Ad " + placementID + " is starting!  Pause your game  animation or sound here.");
        };

        //Event is triggered when a Vungle ad finished and provides the entire information about this event
        //These can be used to determine how much of the video the user viewed, if they skipped the ad early, etc.
        Vungle.onAdFinishedEvent += (placementID, args) => {
            Debug.Log("Ad finished - placementID " + placementID + ", was call to action clicked:" + args.WasCallToActionClicked + ", is completed view:"
                + args.IsCompletedView);
        };

        //Event is triggered when the ad's playable state has been changed
        //It can be used to enable certain functionality only accessible when ad plays are available
        Vungle.adPlayableEvent += (placementID, adPlayable) => {
            Debug.Log("Ad's playable state has been changed! placementID " + placementID + ". Now: " + adPlayable);
            placementsID[placementID] = adPlayable;
        };

        //Fired log event from sdk
        Vungle.onLogEvent += (log) => {
            Debug.Log("Log: " + log);
        };

        //Fired initialize event from sdk
        Vungle.onInitializeEvent += () => {
            adInited = true;
            Debug.Log("SDK initialized");
        };
    }

    void OnGUI()
    {
#if ADS_TESTING_ON
        if (GUI.Button(new Rect(10, 150, 100, 60), "Vungle video"))
        {
            ShowRewardAd();
        }
#endif
    }



    private void OnAdFinished(string scenename, AdFinishedEventArgs obj)
    {
        if (obj.IsCompletedView)
        {
            AdsManager.Instance.VideoRewardSuccess();
        }
    }

    public bool ShowRewardAd()
    {
        if (Vungle.isAdvertAvailable(placementIdList[0]))
        {
            Vungle.playAd(options, placementIdList[0]);
            // Vungle.playAdWithOptions(options);
            return true;
        }
        else
        {
            Debug.Log("Vungle ad is not ready");
            return false;
        }
    }
    void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            Vungle.onPause();
        }
        else
        {
            Vungle.onResume();
        }
    }

    public bool ShowSkippableAd()
    {
        if (Vungle.isAdvertAvailable(placementIdList[1]))
        {
            Debug.Log("ShowSkippableAd:" + placementIdList[1]);
            Vungle.playAd(options, placementIdList[1]);
            return true;
        }
        else
        {
            Debug.Log("Vungle ad is not ready_____Skippable");
            return false;
        }
    }
}
