using UnityEngine;
using System.Collections;

public class BigCodeLibHandler : MonoBehaviour
{
    #region "Getter"
    private static BigCodeLibHandler _instance;
    public static BigCodeLibHandler Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = FindObjectOfType<BigCodeLibHandler>();
            }
            return _instance;
        }
    }

    #endregion
    private AndroidJavaObject contextActivity = null;

    private AndroidJavaClass nativeFunctionalities;

    void Awake()
    {
        _instance = this;
#if !UNITY_EDITOR && UNITY_ANDROID
        nativeFunctionalities = new AndroidJavaClass("com.bigcode.nativefunctionalities.MainActivity");

        using (AndroidJavaClass activityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            if (activityClass != null)
            {
                contextActivity = activityClass.GetStatic<AndroidJavaObject>("currentActivity");
            }
            else
            {
                Debug.Log("activity class is null");
            }
        }
#endif
    }

    /// <summary>
    /// Shows the message as toast
    /// </summary>
    /// <param name="msg"></param>
    public void ShowToast(string msg)
    {
#if !UNITY_EDITOR && UNITY_ANDROID
             nativeFunctionalities.CallStatic("showMessage", msg);
#endif
    }

    /// <summary>
    /// To share the GameName and description(url)
    /// </summary>
    /// <param name="gameName"></param>
    /// <param name="description"></param>
    public void NativeShare(string gameName,string description)
    {
#if !UNITY_EDITOR && UNITY_ANDROID
        nativeFunctionalities.CallStatic("shareText", gameName,description);
#endif
    }
    /// <summary>
    /// To share the game on FB
    /// </summary>
    /// <param name="gameName"></param>
    /// <param name="description"></param>
    public void FBShare(string gameName, string description)
    {
#if !UNITY_EDITOR && UNITY_ANDROID
        nativeFunctionalities.CallStatic("shareTextFB", gameName,description);
#endif
    }

    /// <summary>
    /// To share the game on WhatsApp
    /// </summary>
    /// <param name="gameName"></param>
    /// <param name="description"></param>
    public void WhatsAppShare(string gameName, string description)
    {
#if !UNITY_EDITOR && UNITY_ANDROID
        nativeFunctionalities.CallStatic("shareTextWA", gameName,description);
#endif
    }

    /// <summary>
    /// To share the game on Twitter
    /// </summary>
    /// <param name="gameName"></param>
    /// <param name="description"></param>
    public void TwitterShare(string gameName, string description)
    {
#if !UNITY_EDITOR && UNITY_ANDROID
        nativeFunctionalities.CallStatic("shareTextTwitter", gameName,description);
#endif
    }

    /// <summary>
    /// To check whether package name is installed or not
    /// </summary>
    /// <param name="packageName"></param>
    /// <returns></returns>
    public bool IsGameInstalled(string packageName)
    {
#if  UNITY_EDITOR
        return false;
#elif UNITY_ANDROID
       return nativeFunctionalities.CallStatic<bool>("isAppInstalled", packageName);
#endif
    }

    public void ShowLoading(string title, string message)
    {
#if !UNITY_EDITOR && UNITY_ANDROID
        contextActivity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
        {
            nativeFunctionalities.CallStatic("showLoading", title, message);
        }));
#endif
    }

    public void DismissLoading()
    {
#if !UNITY_EDITOR && UNITY_ANDROID
        nativeFunctionalities.CallStatic("dismissLoading");
#endif
    }
    public void LoadWebview()
    {
#if !UNITY_EDITOR && UNITY_ANDROID
        nativeFunctionalities.CallStatic("showWebView", AdsManager.Instance.exitPageUrl, name);
#endif
    }

    public void ShowWebview()
    {
#if !UNITY_EDITOR && UNITY_ANDROID
        nativeFunctionalities.CallStatic("showWebView");
#endif
    }

    public void HideWebView()
    {
#if !UNITY_EDITOR && UNITY_ANDROID
        nativeFunctionalities.CallStatic("hideWebView");
#endif
    }

    public void ShowGPlus()
    {
#if !UNITY_EDITOR && UNITY_ANDROID
        nativeFunctionalities.CallStatic("showGplus");
#endif
    }

    public void HideGPlus()
    {
#if !UNITY_EDITOR && UNITY_ANDROID
        nativeFunctionalities.CallStatic("hideGplus");
#endif
    }

    public void ShowAlert(string title,string msg, string yes,string no,bool needTextField)
    {
#if !UNITY_EDITOR && UNITY_ANDROID
        nativeFunctionalities.CallStatic("showAlertDialog",title,msg,yes,no,needTextField);
#endif
    }

    public void SetLocalNotification(int id,long timeInMilliSeconds,string message)
    {
#if !UNITY_EDITOR && UNITY_ANDROID
#if UNITY_5_6_OR_NEWER
        nativeFunctionalities.CallStatic("SetNotification",id,timeInMilliSeconds,Application.productName,message,"icon_localnotification","icon_localnotification",Application.identifier);
#else
        nativeFunctionalities.CallStatic("SetNotification", id, timeInMilliSeconds, Application.productName, message,"icon_localnotification","icon_localnotification", Application.bundleIdentifier);
#endif
#endif
    }

    public void CancelNotification(int id)
    {
#if !UNITY_EDITOR && UNITY_ANDROID
        nativeFunctionalities.CallStatic("CancelNotification", id);
#endif
    }

}
