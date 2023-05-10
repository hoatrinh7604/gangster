using UnityEngine;
using System.Collections;
using System;

public class WebviewHandler : MonoBehaviour {

    protected internal bool isShowingWebView = false;
    private string hideCallBackMessage = string.Empty;

    void Start()
    {
    }

    public void webViewCallback(string url)
    {
        if (url.Equals("close"))
        {
            Application.Quit();
        }
        if (url.Equals("no"))
        {
            BigCodeLibHandler.Instance.HideWebView();
        }
        if (url.Equals("rateit"))
        {
            BigCodeLibHandler.Instance.HideWebView();
            Application.OpenURL("market://details?id=" + Application.identifier);
        }
        if (url.Contains("com."))
        {
            BigCodeLibHandler.Instance.HideWebView();
            Application.OpenURL("market://details?id=" + url);
        }
        isShowingWebView = false;
    }
    void OnGUI()
    {
#if ADS_TESTING_ON
        if (GUI.Button(new Rect(10, 10, 100, 60), "Webview"))
        {
            BigCodeLibHandler.Instance.ShowWebview();
        }
#endif
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!AdsManager.IsInternetAvailable())
            {
                AdsManager.Instance.ShowAlertDialog("Are you sure, Do you want to exit?", "Yes", "No",AlertType.Quit);
            }
            else
            {
                if (!isShowingWebView)
                {
                    BigCodeLibHandler.Instance.ShowWebview();
                }
                else
                {
                    BigCodeLibHandler.Instance.HideWebView();
                }
                isShowingWebView = !isShowingWebView;
            }
        }
    }

}
