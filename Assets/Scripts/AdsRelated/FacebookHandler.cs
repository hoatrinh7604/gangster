using UnityEngine;
using System.Linq;
//MONSTER REM using Facebook.Unity;
using System;
using System.Collections.Generic;

public class FacebookHandler : MonoBehaviour
{

    #region "Getter"
    private static FacebookHandler _instance;
    public static FacebookHandler Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<FacebookHandler>();
            }
            return _instance;
        }
    }

    #endregion

    protected internal ShareType shareType = ShareType.None;
    protected internal string descriptionLocal;

    public delegate void LogIn(bool isLoggedIn);
    public static event LogIn LogInEvent;

    void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        //MONSTER REM BEGIN
        //if (!FB.IsInitialized)
        //    FB.Init(onInitComplete, onHideUnity);
        //else
        //    FB.ActivateApp();
        //MONSTER REM END
    }

    private void onHideUnity(bool isUnityShown)
    {
        Debug.Log("On Unity hide status :: " + isUnityShown);
    }

    private void onInitComplete()
    {
        Debug.Log("On Init completes..");
        //MONSTER REM BEGIN
        //FB.ActivateApp();
        //if (FB.IsLoggedIn)
        //{
        //    //PlayFabHandler.Instance.LoginWithFacebook();
        //    if (LogInEvent != null)
        //        LogInEvent(true);
        //}
        //MONSTER REM END
    }

    public void LoginClick()
    {
        //MONSTER REM BEGIN
        //if (!FB.IsLoggedIn)
        //    FB.LogInWithPublishPermissions(new List<string>() { "publish_actions" }, AuthCallback);
        //else
        //{
        //    FB.LogOut();
        //    if (LogInEvent != null)
        //        LogInEvent(false);
        //}
        //MONSTER REM END
    }

    //MONSTER REM BEGIN
    //public bool IsLoggedIn
    //{
    //    get
    //    {
    //        return FB.IsLoggedIn;
    //    }
    //}
    //MONSTER REM END

    public void Share()
    {
        //BigCodeLibHandler.Instance.FBShare(Application.productName, "https://play.google.com/store/apps/details?id=" + Application.bundleIdentifier);
        //BigCodeLibHandler.Instance.FBShare(Application.productName, AdsManager.Instance.shareUrl);

        //MONSTER REM BEGIN
        //if (FB.IsLoggedIn)
        //{
        //    FB.ShareLink(new Uri("https://play.google.com/store/apps/details?id="+Application.identifier), Application.productName, string.Empty, null, ShareCallback);
        //    //FB.ShareLink(new Uri(AdsManager.Instance.shareFBUrl), Application.productName, string.Empty, new Uri(AdsManager.Instance.shareIconUrl), ShareCallback);
        //}
        //else
        //{
        //    shareType = ShareType.NormalShare;
        //    FB.LogInWithPublishPermissions(new List<string>() { "publish_actions" }, AuthCallback);
        //}
        //MONSTER REM END

        //AdsManager.Instance.Invoke("ShareCallback", 2f);
    }

    //MONSTER REM BEGIN
    //private void AuthCallback(ILoginResult result)
    //{
    //    if(result.Error == null)
    //    {
    //        if (LogInEvent != null)
    //            LogInEvent(FB.IsLoggedIn);
    //        //PlayFabHandler.Instance.LoginWithFacebook();

    //        switch(shareType)
    //        {
    //            case ShareType.NormalShare:
    //                Share();
    //                break;
    //            case ShareType.ShareWithDescription:
    //                Share(descriptionLocal);
    //                break;
    //        }
    //    }
    //}
    //MONSTER REM END

    public void Share(string description)
    {
        //MONSTER REM BEGIN
        //if (FB.IsLoggedIn)
        //{
        //    FB.ShareLink(new Uri("https://play.google.com/store/apps/details?id=" + Application.identifier), description, string.Empty, null, ShareCallback);
        //    //FB.ShareLink(new Uri(AdsManager.Instance.shareFBUrl), Application.productName, description, new Uri(AdsManager.Instance.shareIconUrl), ShareCallback);
        //}
        //else
        //{
        //    descriptionLocal = description;
        //    shareType = ShareType.ShareWithDescription;
        //    FB.LogInWithPublishPermissions(new List<string>() { "publish_actions" }, AuthCallback);
        //}
        //MONSTER REM END
    }

    //MONSTER REM BEGIN
    //private void ShareCallback(IShareResult result)
    //{
    //    if (result.Error == null)
    //    {
    //        //AdsManager.Instance.ShareCallback();
    //    }
    //}
    //MONSTER REM END
}

public enum ShareType
{
    None,
    NormalShare,
    ShareWithDescription
}
