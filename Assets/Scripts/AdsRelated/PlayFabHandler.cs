using UnityEngine;
//MONSTER REM BEGIN
//using PlayFab;
//using PlayFab.ClientModels;
//using Facebook.Unity;
//using LoginResult = PlayFab.ClientModels.LoginResult;
//MONSTER REM END
using System.Collections.Generic;
using System;

public class PlayFabHandler : MonoBehaviour {

    public static PlayFabHandler Instance { get; private set; }

    protected internal string playFabUserId;

    private bool isFacebookDataLoaded;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
       
    }

    public void LinkWithFacebook()
    {
        //MONSTER REM PlayFabClientAPI.LinkFacebookAccount(new LinkFacebookAccountRequest { AccessToken = AccessToken.CurrentAccessToken.ToString() }, OnPlayfabFacebookLinkComplete, OnPlayfabFacebookLinkFailed);
    }

    //MONSTER REM BEGIN
    //private void OnPlayfabFacebookLinkComplete(LinkFacebookAccountResult obj)
    //{
    //    SetGameData();
    //}

    //private void OnPlayfabFacebookLinkFailed(PlayFabError obj)
    //{

    //}

    //public void LoginWithFacebook()
    //{
    //    //PlayFabClientAPI.LoginWithFacebook(new LoginWithFacebookRequest { TitleId = AdsManager.Instance.playFabTitleId, CreateAccount = true, AccessToken = AccessToken.CurrentAccessToken.TokenString },
    //    //    OnPlayfabFacebookAuthComplete, OnPlayfabFacebookAuthFailed);
    //}

    //// When processing both results, we just set the message, explaining what's going on.
    //private void OnPlayfabFacebookAuthComplete(LoginResult result)
    //{
    //   Debug.LogError("PlayFab Facebook Auth Complete. Session ticket: " + result.SessionTicket);
    //    playFabUserId = result.PlayFabId;
    //    GetGameData();
    //}

    //private void OnPlayfabFacebookAuthFailed(PlayFabError error)
    //{
    //    Debug.LogError("PlayFab Facebook Auth Failed: " +AccessToken.CurrentAccessToken.UserId + " :: " +AccessToken.CurrentAccessToken.TokenString +" :: "+ error.GenerateErrorReport());
    //}
    //MONSTER REM END

    #region "Get and Set Player Data"
    public void GetGameData()
    {
        if (string.IsNullOrEmpty(playFabUserId))
        {
            Debug.LogError("Not logged in or authenticated yet");
            return;
        }
        
        List<string> keysToGet = new List<string>();
       
        keysToGet.Add(PlayGameServicesHandler.CLOUD_DATA);

        //MONSTER REM BEGIN
        //GetUserDataRequest userDataRequest = new GetUserDataRequest()
        //{
        //    Keys = keysToGet,
        //    PlayFabId = playFabUserId
        //};

        //PlayFabClientAPI.GetUserData(userDataRequest, getResultCallBack, getErrorCallBack);
        //MONSTER REM END
    }

    //MONSTER REM BEGIN
    //void getResultCallBack(GetUserDataResult result)
    //{
    //    Debug.LogError("Got player data " + result.Data.Keys.Count);
    //    bool isDataExisted = false;
    //    foreach (string key in result.Data.Keys)
    //    {
    //        if(key == PlayGameServicesHandler.CLOUD_DATA)
    //        {
    //            isDataExisted = true;
    //            StringToGameData(result.Data[key].Value, PlayerPrefs.GetString(PlayGameServicesHandler.CLOUD_DATA));
    //        }
    //    }

    //    if (!isDataExisted)
    //    {
    //        StringToGameData(string.Empty, PlayerPrefs.GetString(PlayGameServicesHandler.CLOUD_DATA));
    //    }

    //}

    //void getErrorCallBack(PlayFabError err)
    //{
    //    Debug.LogError("Got player data error " + err.ToString());
    //}
    //MONSTER REM END

    public void SetGameData()
    {
        if (string.IsNullOrEmpty(playFabUserId))
        {
            Debug.LogError("Not logged in or authenticated yet");
            return;
        }

        Dictionary<string, string> keyValuePair = new Dictionary<string, string>();
        keyValuePair.Add(PlayGameServicesHandler.CLOUD_DATA, GameDataToString());

        //MONSTER REM BEGIN
        //UpdateUserDataRequest userDataRequest = new UpdateUserDataRequest()
        //{
        //    Data = keyValuePair,
        //    KeysToRemove = null,
        //    Permission = UserDataPermission.Public
        //};

        //PlayFabClientAPI.UpdateUserData(userDataRequest, updateResultCallBack, updateErrorCallBack);
        //MONSTER REM END
    }

    //this overload is used when user is connected to the internet
    //parsing string to game data (stored in CloudVariables), also deciding if we should use local or cloud save
    void StringToGameData(string fbStoredData, string localData)
    {
        Debug.LogError("Facebook data :: " + fbStoredData);
        if (fbStoredData == string.Empty)
        {
            StringToGameData(localData);
            isFacebookDataLoaded = true;
            SaveData();
            return;
        }

        GameData fbVariables = JsonUtility.FromJson<GameData>(fbStoredData);
        if (localData == string.Empty)
        {
            PlayerPrefs.SetString(PlayGameServicesHandler.CLOUD_DATA, fbStoredData);
            isFacebookDataLoaded = true;
            return;
        }

        // Get the local data
        GameData localVariables = JsonUtility.FromJson<GameData>(localData);

        //if it's the first time that game has been launched after installing it and successfuly logging into Google Play Games
        if (PlayerPrefs.GetInt("IsFirstTime") == 1)
        {
            //set playerpref to be 0 (false)
            PlayerPrefs.SetInt("IsFirstTime", 0);
            for (int i = 0; i < fbVariables.ImportantValues.Length; i++)
                if (fbVariables.ImportantValues[i] > localVariables.ImportantValues[i]) //cloud save is more up to date
                {
                    //set local save to be equal to the cloud save
                    PlayerPrefs.SetString(PlayGameServicesHandler.CLOUD_DATA, fbStoredData);
                }

        }
        //if it's not the first time, start comparing
        else
        {

            for (int i = 0; i < fbVariables.ImportantValues.Length; i++)
                //comparing integers, if one int has higher score in it than the other, we update it
                if (localVariables.ImportantValues[i] > fbVariables.ImportantValues[i])
                {
                    //update the cloud save, first set CloudVariables to be equal to localSave
                    PlayerProgress.gameData = localVariables;
                    isFacebookDataLoaded = true;
                    //saving the updated CloudVariables to the cloud
                    SaveData();
                    return;
                }

        }
        //if the code above doesn't trigger return and the code below executes,
        //cloud save and local save are identical, so we can load either one
        PlayerProgress.gameData = fbVariables;

        isFacebookDataLoaded = true;
    }

    //used for saving data to the cloud or locally
    public void SaveData()
    {
        //if we're still running on local data (cloud data has not been loaded yet), we also want to save only locally
        if (!isFacebookDataLoaded)
            return;
        SetGameData();
    }

    //this overload is used when there's no internet connection - loading only local data
    void StringToGameData(string localData)
    {
        PlayerProgress.gameData = JsonUtility.FromJson<GameData>(localData);
    }

    string GameDataToString()
    {
        return JsonUtility.ToJson(PlayerProgress.gameData);
    }

    //MONSTER REM BEGIN
    //void updateResultCallBack(UpdateUserDataResult result)
    //{
    //    Debug.LogError("Update data :: " + result.DataVersion);
    //}

    //void updateErrorCallBack(PlayFabError err)
    //{
    //    Debug.LogError("Set player data error " + err.ToString());
    //}
    //MONSTER REM END
    #endregion


}
