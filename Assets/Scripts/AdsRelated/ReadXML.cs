using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System;
using UnityEngine.SceneManagement;
using System.Net;
using System.IO;

public class ReadXML : AdIDs {

	internal static XmlDocument xmlBaseDoc, xmlDoc;
    protected internal bool isMenuAdShown;
    protected internal Texture2D menuAdTexture;
    protected internal string menuAdMarketUrl;
    protected internal string moreGamesPopupImageLocation;
    public Dictionary<string, int> moreGamesPopupBundleIdsValues;
    protected internal Texture2D moreGamesPopupTexture, moreGames2PopupTexture;
    protected internal string currentPackageMoreGame, currentPackageMoreGame2;
    protected internal string admobPlacementsInfo;
    protected internal string otherAdsPlacementsInfo;
    protected internal const string DOWNLOADED_GAMES = "DownloadedMoreGames";
    protected internal List<int> rateLevels, shareLevels;
    protected internal string rateMessage, shareMessage;
    protected internal int rateBonus, shareBonus;
    protected internal string promoSuccessCode = string.Empty;
    protected internal bool isUnityVideoOnTop;
    protected internal int promoCoins;
    protected internal bool isDiscountOn;
    protected internal string discountDescription;

    private string menuAdImageURL;
    private string menuAdImageName;
    private bool shouldLoadNextScene,isDownloadedMenuAd;
    private string menuAdVersion = string.Empty;
    private bool isFullScreenAd = false;

    public IEnumerator LoadBaseXMLData()
    {
        if (IsInternetAvailable())
        {
            WWW www;
            www = new WWW(xmlBaseUrl);
            yield return www;
            if (www.error == null)
            {
                xmlBaseDoc = new XmlDocument();
                xmlBaseDoc.LoadXml(www.text);
            }
            else
            {
                LoadLocalBaseXML();
            }
        }
        else
        {
            LoadLocalBaseXML();
        }
        ReadDataToVariables();
        //StartCoroutine(LoadXMLData());
    }

    private void LoadLocalBaseXML()
    {
        try
        {
            xmlBaseDoc = new XmlDocument();
            if (localBaseXmlFileName.EndsWith(".xml"))
            {
                localBaseXmlFileName = localBaseXmlFileName.Substring(0, (localBaseXmlFileName.Length - 4));
            }
            TextAsset textAsset = (TextAsset)Resources.Load(localBaseXmlFileName);
                xmlBaseDoc.LoadXml(textAsset.text);
        }
        catch (XmlException ex)
        {
            Debug.Log("Xml Exception : " + ex.Message);
        }
        catch (Exception ex)
        {
            Debug.Log("Exception : " + ex.Message);
        }
    }

    public IEnumerator LoadXMLData()
    {
        if (IsInternetAvailable())
        {
            WWW www;
            www = new WWW(xmlUrl);
            yield return www;
            if (www.error == null)
            {
                xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(www.text);
            }
            else
            {
                 LoadLocalXML();
            }
        }
        else
        {
            LoadLocalXML();
        }
        ReadGameRelatedVariables();
        SceneManager.LoadScene(nextSceneName);

        //ReadDataToVariables();
    }

    private void LoadLocalXML()
    {
        try
        {
            xmlDoc = new XmlDocument();
            if (localXmlFileName.EndsWith(".xml"))
            {
               localXmlFileName = localXmlFileName.Substring(0, (localXmlFileName.Length - 4));
            }
            TextAsset textAsset = (TextAsset)Resources.Load(localXmlFileName);
            xmlDoc.LoadXml(textAsset.text);
        }
        catch (XmlException ex)
        {
            Debug.Log("Xml Exception : " + ex.Message);
        }
        catch (Exception ex)
        {
            Debug.Log("Exception : "+ex.Message);
        }
    }

    void ReadDataToVariables()
    {
       

        moreGamesPackage = xmlBaseDoc.SelectNodes("gameconfigs/MainMoreGames")[0].Attributes.GetNamedItem("linkto").Value;

        moreGamesPopupImageLocation = xmlBaseDoc.SelectNodes("gameconfigs/moregamepngs")[0].Attributes.GetNamedItem("FolderLocation").Value;

        


        XmlNodeList nodes = xmlBaseDoc.SelectNodes("gameconfigs/moregame");
        moreGamesPopupBundleIdsValues = new Dictionary<string, int>();
        foreach (XmlNode node in nodes)
        {
            string packageName = node.Attributes.GetNamedItem("packageID").Value;
            int coins = int.Parse(node.Attributes.GetNamedItem("downloadcoins").Value);
            if(!moreGamesPopupBundleIdsValues.ContainsKey(packageName))
                moreGamesPopupBundleIdsValues.Add(packageName, coins);
        }
  
        if (isPortraitGame)
        {
            exitPageUrl = xmlBaseDoc.SelectNodes("gameconfigs/mg")[0].Attributes.GetNamedItem("exitport").Value;
        }
        else
        {
            exitPageUrl = xmlBaseDoc.SelectNodes("gameconfigs/mg")[0].Attributes.GetNamedItem("exitland").Value;
        }

        XmlNode discountNode = xmlBaseDoc.SelectNodes("gameconfigs/discount")[0];
        if(discountNode != null)
        {
            if(discountNode.Attributes.GetNamedItem("show") != null)
            {
                isDiscountOn = (discountNode.Attributes.GetNamedItem("show").Value == "1") ? true : false;
            }

            if(discountNode.Attributes.GetNamedItem("text") != null)
            {
                discountDescription = discountNode.Attributes.GetNamedItem("text").Value;
            }
        }
        if(!string.IsNullOrEmpty(exitPageUrl))
        {
            BigCodeLibHandler.Instance.LoadWebview();
        }

        ShowMenuAd();

        StartCoroutine(SaveMoreGameDetails());
    }

    public void ReadGameRelatedVariables()
    {
        string adsInfo = xmlDoc.SelectNodes("gameconfigs/gdetails")[0].Attributes.GetNamedItem("st").Value;

        string[] adsDetails = adsInfo.Split(new char[] { '_' });
        otherAdsPlacementsInfo = adsDetails[0];
        admobPlacementsInfo = adsDetails[1];

        levelCompleteAdDelay = float.Parse(xmlDoc.SelectNodes("gameconfigs/gdetails")[0].Attributes.GetNamedItem("lcaddelay").Value);
        levelFailAdDelay = float.Parse(xmlDoc.SelectNodes("gameconfigs/gdetails")[0].Attributes.GetNamedItem("lfaddelay").Value);

        string adDelay = xmlDoc.SelectNodes("gameconfigs/gdetails")[0].Attributes.GetNamedItem("adtoaddelay").Value;
        adtoAdDelay = int.Parse(adDelay);

        shareFBUrl = xmlDoc.SelectNodes("gameconfigs/share")[0].Attributes.GetNamedItem("urlFB").Value;
        shareIconUrl = xmlDoc.SelectNodes("gameconfigs/share")[0].Attributes.GetNamedItem("urlICON").Value;

        if (xmlDoc.SelectNodes("gameconfigs/share")[0].Attributes.GetNamedItem("urlgplus") != null)
            shareGPlusUrl = xmlDoc.SelectNodes("gameconfigs/share")[0].Attributes.GetNamedItem("urlgplus").Value;

        if (xmlDoc.SelectNodes("gameconfigs/share")[0].Attributes.GetNamedItem("urlTW") != null)
            shareTwitterUrl = xmlDoc.SelectNodes("gameconfigs/share")[0].Attributes.GetNamedItem("urlTW").Value;

        if (xmlDoc.SelectNodes("gameconfigs/share")[0].Attributes.GetNamedItem("urlWA") != null)
            shareWhatsappUrl = xmlDoc.SelectNodes("gameconfigs/share")[0].Attributes.GetNamedItem("urlWA").Value;

        rateLevels = new List<int>();
        shareLevels = new List<int>();

        XmlNode rateNode = xmlDoc.SelectNodes("gameconfigs/ratepopup")[0];
        if (rateNode != null)
        {
            string[] levels = rateNode.Attributes.GetNamedItem("levelNums").Value.Split(new char[] { '_' });
            for (int i = 0; i < levels.Length; i++)
            {
                rateLevels.Add(int.Parse(levels[i]));
            }
            rateMessage = rateNode.Attributes.GetNamedItem("ratemsg").Value;
            rateBonus = int.Parse(rateNode.Attributes.GetNamedItem("ratecoins").Value);
        }


        XmlNode shareNode = xmlDoc.SelectNodes("gameconfigs/sharepopup")[0];
        if (shareNode != null)
        {
            string[] levels = shareNode.Attributes.GetNamedItem("levelNums").Value.Split(new char[] { '_' });
            for (int i = 0; i < levels.Length; i++)
            {
                shareLevels.Add(int.Parse(levels[i]));
            }
            shareMessage = shareNode.Attributes.GetNamedItem("sharemsg").Value;
            shareBonus = int.Parse(shareNode.Attributes.GetNamedItem("sharecoins").Value);
        }

        XmlNode promoNode = xmlDoc.SelectNodes("gameconfigs/promo")[0];
        if (promoNode != null)
        {
            bool isPromoEnabled = (promoNode.Attributes.GetNamedItem("enabled").Value == "1") ? true : false;
            if (isPromoEnabled)
            {
                promoSuccessCode = promoNode.Attributes.GetNamedItem("code").Value;
                promoCoins = int.Parse(promoNode.Attributes.GetNamedItem("promocoins").Value);
            }
        }

        isUnityVideoOnTop = (xmlDoc.SelectNodes("gameconfigs/adpref")[0].Attributes.GetNamedItem("topVideo").Value == "1") ? true : false;
    }


    void Update()
    {
        if(shouldLoadNextScene)
        {
            shouldLoadNextScene = false;
            File.Delete(GameConstants.PERSISTENT_PATH + "/" + menuAdImageName);
            StartCoroutine(LoadNextLevel(nextSceneLoadingDelay));
        }else if(isDownloadedMenuAd)
        {
            isDownloadedMenuAd = false;
            PlayerPrefs.SetString(GameConstants.MENUAD_DOWNLOADED, DateTime.Today.ToString());
            setMenuAd();
        }
    }

    public IEnumerator SaveMoreGameDetails()
    {
        string imageUrl = string.Empty;
        List<string> downloadedGamesList = new List<string>();
        if (PlayerPrefs.HasKey(DOWNLOADED_GAMES))
        {
            string[] downloadedGamesIDs = PlayerPrefs.GetString(DOWNLOADED_GAMES).Split(new char[] { '#' });
            for (int i = 0; i < downloadedGamesIDs.Length; i++)
            {
                downloadedGamesList.Add(downloadedGamesIDs[i]);
            }
        }
        foreach (string key in moreGamesPopupBundleIdsValues.Keys)
        {
            if (!downloadedGamesList.Contains(key))
            {
                imageUrl = moreGamesPopupImageLocation + key.Substring(key.LastIndexOf('.') + 1);
                imageUrl += ".jpg";
                currentPackageMoreGame = key;
                break;
            }
        }
        if (string.IsNullOrEmpty(imageUrl))
        {
            Debug.LogError("All games installed ");
            yield break;
        }
        Debug.Log("ImageURL"+imageUrl);
        WWW www = new WWW(imageUrl);
        yield return www;
        if (www.error == null)
        {
            moreGamesPopupTexture = www.texture;
            if (MoreGamesPopupHandler.Instance != null)
            {
                MoreGamesPopupHandler.Instance.SetMoreGamesButton();
            }
        }
        else
        {
            Debug.LogError("Download failed :: " + www.error);
            currentPackageMoreGame = string.Empty;
        }

        freeGamesurl = xmlBaseDoc.SelectNodes("gameconfigs/Freegame")[0].Attributes.GetNamedItem("FreeImg").Value;

        freeGamesPackage = xmlBaseDoc.SelectNodes("gameconfigs/Freegame")[0].Attributes.GetNamedItem("FreeLink").Value;

        WWW www1 = new WWW(freeGamesurl);
        yield return www1;
        if (www1.error == null)
        {
            moreGames2PopupTexture = www1.texture;
            if (MoreGames2PopupHandler.Instance != null)
            {
                MoreGames2PopupHandler.Instance.SetMoreGames2Button();
            }
        }
        else
        {
            Debug.LogError("Download failed :: " + www1.error);
        }

        Debug.Log(freeGamesurl);
        Debug.Log(freeGamesPackage);
    }

    void ShowMenuAd()
    {
        if (isMenuAdShown)
        {
            return;
        }

        XmlNode menuAdNode = xmlBaseDoc.SelectNodes("gameconfigs/mpad")[0];

        if(menuAdNode == null)
        {
            StartCoroutine(LoadNextLevel(nextSceneLoadingDelay));
            return;
        }

        bool isMenuAdNeeded = true;
        string[] noMenuAdInTheseGames = xmlBaseDoc.SelectNodes("gameconfigs/menuad")[0].Attributes.GetNamedItem("noneed").Value.Split(new char[] {'#'});
        for(int i=0; i < noMenuAdInTheseGames.Length; i++)
        {
#if UNITY_5_6_OR_NEWER
            if(noMenuAdInTheseGames[i] == Application.identifier)

#else
            if (noMenuAdInTheseGames[i] == Application.bundleIdentifier)
#endif
            {
                isMenuAdNeeded = false;
                break;
            }
        }
        if (!isMenuAdNeeded)
        {
            StartCoroutine(LoadNextLevel(nextSceneLoadingDelay));
            return;
        }

        if (isPortraitGame)
        {
            menuAdImageURL = menuAdNode.Attributes.GetNamedItem("img1").Value;
        }
        else
        {
            menuAdImageURL = menuAdNode.Attributes.GetNamedItem("img2").Value;
        }

        if (menuAdNode.Attributes.GetNamedItem("version") != null)
        {
            menuAdVersion = menuAdNode.Attributes.GetNamedItem("version").Value;
        }

        if(menuAdNode.Attributes.GetNamedItem("fullscreen") != null)
        {
            isFullScreenAd = bool.Parse(menuAdNode.Attributes.GetNamedItem("fullscreen").Value);
        }

        menuAdMarketUrl = menuAdNode.Attributes.GetNamedItem("linkto").Value;

        string menuAdPackageName = menuAdMarketUrl.Substring(menuAdMarketUrl.IndexOf('=')+1, menuAdMarketUrl.Substring(menuAdMarketUrl.IndexOf('=') + 1).IndexOf('&'));
        //if(AdsManager.Instance.isGameInstalled(menuAdPackageName))
        //{
        //    StartCoroutine(LoadNextLevel(nextSceneLoadingDelay));
        //    return;
        //}
        menuAdImageName = menuAdPackageName.Substring(menuAdPackageName.LastIndexOf('.') + 1)+".jpg";

        DirectoryInfo d = new DirectoryInfo(@GameConstants.PERSISTENT_PATH);
        FileInfo[] Files = d.GetFiles("*.jpg");
       
        foreach (FileInfo file in Files)
        {
           if(!menuAdImageName.Equals(file.Name))
            {
                File.Delete(GameConstants.PERSISTENT_PATH + "/" + file.Name);
            }
        }

        if (!string.IsNullOrEmpty(menuAdVersion) && menuAdVersion != PlayerPrefs.GetString(GameConstants.MENUAD_VERSION,string.Empty))
        {
            if (File.Exists(GameConstants.PERSISTENT_PATH + "/" + menuAdImageName))
            {
                File.Delete(GameConstants.PERSISTENT_PATH + "/" + menuAdImageName);
            }
            PlayerPrefs.SetString(GameConstants.MENUAD_VERSION, menuAdVersion);
            DownloadMenuAd();
        }
        else
        {
            if (File.Exists(GameConstants.PERSISTENT_PATH + "/" + menuAdImageName) && (DateTime.Today.Subtract(DateTime.Parse(PlayerPrefs.GetString(GameConstants.MENUAD_DOWNLOADED,DateTime.Today.ToString()))).TotalDays < 1)) 
            {
                setMenuAd();
            }
            else
            {
                DownloadMenuAd();
            }
        }
    }
    void DownloadMenuAd()
    {
        WebClient client = new WebClient();
        client.DownloadFileCompleted += new System.ComponentModel.AsyncCompletedEventHandler(DownloadFileCompleted);
        client.DownloadFileAsync(new Uri(menuAdImageURL.Replace("https","http")), GameConstants.PERSISTENT_PATH + "/" + menuAdImageName);
    }

    void DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
    {

        if (e.Error == null)
        {
            isDownloadedMenuAd = true;
        }else
        {
            shouldLoadNextScene = true;
        }
    }
    void setMenuAd()
    {
        if (IsInternetAvailable())
        {
            if (isPortraitGame)
            {
                menuAdTexture = new Texture2D(338, 600, TextureFormat.ARGB32, false);
            }
            else
            {
                menuAdTexture = new Texture2D(600, 372, TextureFormat.ARGB32, false);
            }
            menuAdTexture.LoadImage(File.ReadAllBytes(GameConstants.PERSISTENT_PATH + "/" + menuAdImageName));
            isMenuAdShown = true;
            MenuAdHandler.Instance.Enable(menuAdTexture,isFullScreenAd);
        }
        else
        {
            StartCoroutine(LoadNextLevel(AdsManager.Instance.nextSceneLoadingDelay));
        }

    }

    public IEnumerator LoadNextLevel(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        StartCoroutine(LoadXMLData());
    }
    public static bool IsInternetAvailable()
    {
        if (Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork ||
            Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork)
        {
            return true;
        }
        return false;
    }
}
