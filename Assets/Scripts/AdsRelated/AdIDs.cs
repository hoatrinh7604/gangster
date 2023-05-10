using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AdIDs : MonoBehaviour {
    [Header("XML Details")]
    public string xmlBaseUrl;
    public string localBaseXmlFileName;
    public string xmlUrl;
    public string localXmlFileName;
    public bool isPortraitGame;
    [Header("Next Scene Details")]
    public string nextSceneName;
    public float nextSceneLoadingDelay = 0.5f;
    [HideInInspector]
    public string moreGamesPackage;
    [HideInInspector]
    public float levelCompleteAdDelay, levelFailAdDelay;
    [HideInInspector]
    public string exitPageUrl;
    [HideInInspector]
    public int adtoAdDelay;
    [HideInInspector]
    public string shareFBUrl, shareIconUrl;
    [HideInInspector]
    public string shareGPlusUrl,shareTwitterUrl, shareWhatsappUrl;
    [Header("Ad IDs")]
    public string admobIntersitialID;
    public string admobVideoID;
    public string unityVideoAdID;
    public string vungleVideoID;
    public string facebookIntersitialID;
    public string ironSourceID;
    [Header("Store/InApps IDs")]
    public string storePublicKey;
    public ProductID[] productIds;
    [Header("PlayGameServices")]
    public string leaderBoardID;
    public string[] achievementIDs;
    [Header("OneSignal")]
    public string onesignalAppId;
    [HideInInspector]
    public string freeGamesPackage;
    [HideInInspector]
    public string freeGamesurl;
}

[System.Serializable]
public class ProductID
{
    public string id;
    public bool isConsumable;
}
