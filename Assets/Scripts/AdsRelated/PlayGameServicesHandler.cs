using UnityEngine;
//MONSTER REM BEGIN
//using GooglePlayGames;
//using GooglePlayGames.BasicApi;
//using GooglePlayGames.BasicApi.SavedGame;
//MONSTER REM END
using System.Text;

public class PlayGameServicesHandler : MonoBehaviour {


    #region "Getter"
    private static PlayGameServicesHandler _instance;
    public static PlayGameServicesHandler Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<PlayGameServicesHandler>();
            }
            return _instance;
        }
    }
    #endregion

    #region "SavedGames"
    public const string CLOUD_DATA = "AdSetupCloudSavedData";

    bool isSaving;
    bool isCloudDataLoaded = false;
    #endregion

    private SignInEvent m_signInEvent = SignInEvent.None;
    private float highScore = 0;

    public delegate void PlayGamesSiginIn(bool isSuccess);
    public static event PlayGamesSiginIn PlayGamesSignInEvent;

    void Awake()
    {
        _instance = this;
        highScore = PlayerPrefs.GetFloat(GameConstants.HIGHSCORE_PGS);
    }

    // Use this for initialization
    void Start () {
        Init();
	}
	
	void Init()
    {

        //setting default value, if the game is played for the first time
        if (!PlayerPrefs.HasKey(CLOUD_DATA))
        {
            PlayerProgress.Init();
            SaveLocal();
        }
        //tells us if it's the first time that this game has been launched after install - 0 = no, 1 = yes 
        if (!PlayerPrefs.HasKey("IsFirstTime"))
            PlayerPrefs.SetInt("IsFirstTime", 1);

        LoadLocal(); //we want to load local data first because loading from cloud can take quite a while, if user progresses while using local data, it will all
                     //sync in our comparating loop in StringToGameData(string, string)

        //MONSTER REM BEGIN
        // PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
        //// enables saving game progress.
        //.Build();

        // PlayGamesPlatform.InitializeInstance(config);
        // //recommended for debugging:

        //PlayGamesPlatform.DebugLogEnabled = true;
        //// Activate the Google Play Games platform
        //PlayGamesPlatform.Activate();
        //MONSTER REM END

        //((PlayGamesPlatform)Social.Active).SetDefaultLeaderboardForUI("CgkI-d2n1LEEEAIQCw");

        if (PlayerPrefs.HasKey(GameConstants.PLAYER_AUTHENTICATED))
        {
            AuthenticateOrLogin();
        }
    }

    public void AuthenticateOrLogin(SignInEvent successEvent = SignInEvent.None)
    {
        m_signInEvent = successEvent;
        Social.localUser.Authenticate(authenticationResponse);
    }

    public void Logout()
    {
        //MONSTER REM PlayGamesPlatform.Instance.SignOut();

        if (PlayGamesSignInEvent != null)
        {
            PlayGamesSignInEvent(false);
        }
    }

    public bool IsSignedIn
    {
        get
        {
            return Social.localUser.authenticated;
        }
    }

    public void PlayGamesButtonClick()
    {
        if(!IsSignedIn)
        {
            AuthenticateOrLogin();
        }else
        {
            Logout();
        }
    }

    public void ShowLeaderBoard(string leaderBoardId = null)
    {
        if (!Social.localUser.authenticated)
        {
            Debug.Log("Authentication not done");
            AuthenticateOrLogin(SignInEvent.LeaderBoard);
            return;
        }

        if (string.IsNullOrEmpty(leaderBoardId))
        {
            Social.ShowLeaderboardUI();
        }else
        {
            //MONSTER REM PlayGamesPlatform.Instance.ShowLeaderboardUI(leaderBoardId);
        }
    }


    public void ShowAcheivements()
    {
        if (!Social.localUser.authenticated)
        {
            Debug.Log("Authentication not done");
            AuthenticateOrLogin(SignInEvent.Achievement);
            return;
        }

        Social.ShowAchievementsUI();
    }

    void authenticationResponse(bool isSuccess)
    {
        Debug.LogError("Authentication response :: " + isSuccess);
        LoadData();
        if (isSuccess)
        {
            if(PlayGamesSignInEvent != null)
            {
                PlayGamesSignInEvent(true);
            }

            PlayerPrefs.SetString(GameConstants.PLAYER_AUTHENTICATED, "Success");
        }

        switch(m_signInEvent)
        {
            case SignInEvent.LeaderBoard:
                ShowLeaderBoard();
                break;
            case SignInEvent.Achievement:
                ShowAcheivements();
                break;
        }

        m_signInEvent = SignInEvent.None;
    }

    /// <summary>
    /// To push score
    /// </summary>
    /// <param name="score"></param>
    /// <param name="leaderBoardId"></param>
    public void PushScoreToLeaderBoard(long score, string leaderBoardId = null)
    {
        if(((float)score) < highScore)
        {
            return;
        }
        highScore = (float)score;

        if(string.IsNullOrEmpty(leaderBoardId))
        {
            Social.ReportScore(score, AdsManager.Instance.leaderBoardID, scoreUpdatedResponse);
        }
        else
        {
            Social.ReportScore(score, leaderBoardId, scoreUpdatedResponse);
        }
    }

    void scoreUpdatedResponse(bool isSuccess)
    {
        Debug.Log("Score updated successfully");
        if(isSuccess)
        {
            PlayerPrefs.SetFloat(GameConstants.HIGHSCORE_PGS, highScore);
        }
    }

    /// <summary>
    /// To Unlock achievement - Index as parameter
    /// </summary>
    /// <param name="index"></param>
    public void UnlockAchievement(int index)
    {
        Social.ReportProgress(AdsManager.Instance.achievementIDs[index], 100.0f, achievementUnlockedSuccessfully);
    }

    /// <summary>
    /// To unlock achievement - achievement id as parameter
    /// </summary>
    /// <param name="id"></param>
    public void UnlockAchievement(string id)
    {
        Social.ReportProgress(id, 100.0f, achievementUnlockedSuccessfully);
    }

    void achievementUnlockedSuccessfully(bool isSuccess)
    {
        Debug.Log("Achievement unlocked successfully");
        if(isSuccess)
        {

        }
    }

    #region "SavedGames"
    string GameDataToString()
    {
        return JsonUtility.ToJson(PlayerProgress.gameData);
    }

    //this overload is used when user is connected to the internet
    //parsing string to game data (stored in CloudVariables), also deciding if we should use local or cloud save
    void StringToGameData(string cloudData, string localData)
    {
        //AdsManager.Instance.ShowToast("Data restored from cloud :: " + cloudData);
        Debug.LogError("Cloud data :: " + cloudData);
        if (cloudData == string.Empty)
        {
            StringToGameData(localData);
            isCloudDataLoaded = true;
            return;
        }

        GameData cloudVariables = JsonUtility.FromJson<GameData>(cloudData);
        if (localData == string.Empty)
        {
            PlayerProgress.gameData = cloudVariables;
            PlayerPrefs.SetString(CLOUD_DATA, cloudData);
            isCloudDataLoaded = true;
            return;
        }

        // Get the local data
        GameData localVariables = JsonUtility.FromJson<GameData>(localData);

        //if it's the first time that game has been launched after installing it and successfuly logging into Google Play Games
        if (PlayerPrefs.GetInt("IsFirstTime") == 1)
        {
            //set playerpref to be 0 (false)
            PlayerPrefs.SetInt("IsFirstTime", 0);
            for (int i = 0; i < cloudVariables.ImportantValues.Length; i++)
                if (cloudVariables.ImportantValues[i] > localVariables.ImportantValues[i]) //cloud save is more up to date
                {
                    //set local save to be equal to the cloud save
                    PlayerPrefs.SetString(CLOUD_DATA, cloudData);
                }

        }
        //if it's not the first time, start comparing
        else
        {
            for (int i = 0; i < cloudVariables.ImportantValues.Length; i++)
                //comparing integers, if one int has higher score in it than the other, we update it
                if (localVariables.ImportantValues[i] > cloudVariables.ImportantValues[i])
                {
                    //update the cloud save, first set CloudVariables to be equal to localSave
                   PlayerProgress.gameData = localVariables;
                    isCloudDataLoaded = true;
                    //saving the updated CloudVariables to the cloud
                    SaveData();
                    return;
                }

        }
        //if the code above doesn't trigger return and the code below executes,
        //cloud save and local save are identical, so we can load either one
        PlayerProgress.gameData = cloudVariables;

        isCloudDataLoaded = true;
    }

    //this overload is used when there's no internet connection - loading only local data
    void StringToGameData(string localData)
    {
        PlayerProgress.gameData = JsonUtility.FromJson<GameData>(localData);

        Debug.LogError( "Local Player pref "+PlayerProgress.gameData.ImportantValues.Length + " Local Data :: "+localData);
    }

    //used for loading data from the cloud or locally
    public void LoadData()
    {
        //Debug.LogError("Loading data :: Playgameservices :: " + Social.localUser.authenticated);

        //basically if we're connected to the internet, do everything on the cloud
        if (Social.localUser.authenticated)
        {
            isSaving = false;
            //MONSTER REM ((PlayGamesPlatform)Social.Active).SavedGame.OpenWithManualConflictResolution(CLOUD_DATA,
            //MONSTER REM DataSource.ReadCacheOrNetwork, true, ResolveConflict, OnSavedGameOpened);
        }
        //this will basically only run in Unity Editor, as on device,
        //localUser will be authenticated even if he's not connected to the internet (if the player is using GPG)
        else
        {
            LoadLocal();
        }
    }

    private void LoadLocal()
    {
        StringToGameData(PlayerPrefs.GetString(CLOUD_DATA, string.Empty));
    }

    //used for saving data to the cloud or locally
    public void SaveData()
    {

        //if we're still running on local data (cloud data has not been loaded yet), we also want to save only locally
        if (!isCloudDataLoaded)
        {
            SaveLocal();
            return;
        }
        //same as in LoadData
        if (Social.localUser.authenticated)
        {
            isSaving = true;
            //MONSTER REM ((PlayGamesPlatform)Social.Active).SavedGame.OpenWithManualConflictResolution(CLOUD_DATA,
            //MONSTER REM DataSource.ReadCacheOrNetwork, true, ResolveConflict, OnSavedGameOpened);
        }
        else
        {
            SaveLocal();
        }

        PlayFabHandler.Instance.SaveData();
    }

    private void SaveLocal()
    {
        PlayerPrefs.SetString(CLOUD_DATA, GameDataToString());
    }

    //MONSTER REM BEGIN
    //private void ResolveConflict(IConflictResolver resolver, ISavedGameMetadata original, byte[] originalData,
    //    ISavedGameMetadata unmerged, byte[] unmergedData)
    //{
    //    if (originalData == null)
    //        resolver.ChooseMetadata(unmerged);
    //    else if (unmergedData == null)
    //        resolver.ChooseMetadata(original);
    //    else
    //    {
    //        //decoding byte data into string
    //        string originalStr = Encoding.ASCII.GetString(originalData);
    //        string unmergedStr = Encoding.ASCII.GetString(unmergedData);

    //        //parsing
    //        GameData originalArray = JsonUtility.FromJson<GameData>(originalStr);
    //        GameData unmergedArray = JsonUtility.FromJson<GameData>(unmergedStr);

    //        for (int i = 0; i < originalArray.ImportantValues.Length; i++)
    //        {
    //            //if original score is greater than unmerged
    //            if (originalArray.ImportantValues[i] > unmergedArray.ImportantValues[i])
    //            {
    //                resolver.ChooseMetadata(original);
    //                return;
    //            }
    //            //else (unmerged score is greater than original)
    //            else if (unmergedArray.ImportantValues[i] > originalArray.ImportantValues[i])
    //            {
    //                resolver.ChooseMetadata(unmerged);
    //                return;
    //            }
    //        }

    //        //if return doesn't get called, original and unmerged are identical
    //        //we can keep either one
    //        resolver.ChooseMetadata(original);
    //    }
    //}

    //private void OnSavedGameOpened(SavedGameRequestStatus status, ISavedGameMetadata game)
    //{

    //    //Debug.LogError("On Saved game opened :: " + status + " :: "+isSaving);
    //    //if we are connected to the internet
    //    if (status == SavedGameRequestStatus.Success)
    //    {
    //        //if we're LOADING game data
    //        if (!isSaving)
    //            LoadGame(game);
    //        //if we're SAVING game data
    //        else
    //            SaveGame(game);
    //    }
    //    //if we couldn't successfully connect to the cloud, runs while on device,
    //    //the same code that is in else statements in LoadData() and SaveData()
    //    else
    //    {
    //        if (!isSaving)
    //            LoadLocal();
    //        else
    //            SaveLocal();
    //    }
    //}

    //private void LoadGame(ISavedGameMetadata game)
    //{
    //    //Debug.LogError("On load game ::" + game.Description);
    //    ((PlayGamesPlatform)Social.Active).SavedGame.ReadBinaryData(game, OnSavedGameDataRead);
    //}

    //private void SaveGame(ISavedGameMetadata game)
    //{
    //    string stringToSave = GameDataToString();
    //    //saving also locally (can also call SaveLocal() instead)
    //    PlayerPrefs.SetString(CLOUD_DATA, stringToSave);

    //    //encoding to byte array
    //    byte[] dataToSave = Encoding.ASCII.GetBytes(stringToSave);
    //    //updating metadata with new description
    //    SavedGameMetadataUpdate update = new SavedGameMetadataUpdate.Builder().Build();
    //    //uploading data to the cloud
    //    ((PlayGamesPlatform)Social.Active).SavedGame.CommitUpdate(game, update, dataToSave,
    //        OnSavedGameDataWritten);
    //}

    ////callback for ReadBinaryData
    //private void OnSavedGameDataRead(SavedGameRequestStatus status, byte[] savedData)
    //{
    //    //if reading of the data was successful
    //    if (status == SavedGameRequestStatus.Success)
    //    {
    //        string cloudDataString;
    //        //if we've never played the game before, savedData will have length of 0
    //        if (savedData.Length == 0)
    //            //in such case, we want to assign "0" to our string
    //            cloudDataString = string.Empty;
    //        //otherwise take the byte[] of data and encode it to string
    //        else
    //            cloudDataString = Encoding.ASCII.GetString(savedData);

    //        //getting local data (if we've never played before on this device, localData is already
    //        //"0", so there's no need for checking as with cloudDataString)
    //        string localDataString = PlayerPrefs.GetString(CLOUD_DATA);

    //        //this method will compare cloud and local data
    //        StringToGameData(cloudDataString, localDataString);
    //    }
    //}

    ////callback for CommitUpdate
    //private void OnSavedGameDataWritten(SavedGameRequestStatus status, ISavedGameMetadata game)
    //{
    //    //if (status == SavedGameRequestStatus.Success)
    //    //    AdsManager.Instance.ShowToast("Data saved on cloud successfully " + PlayerPrefs.GetString(CLOUD_DATA));
    //}
    //MONSTER REM END

    /// <summary>
    /// 0 index for levels unlock, 1 index for coins, 2 index for dialy challenge levels unlock
    /// </summary>
    /// <param name="index"></param>
    /// <param name="val"></param>
    public void Increment(int index, int val = 1)
    {
        PlayerProgress.gameData.ImportantValues[index] += val;
        SaveData();
    }

    /// <summary>
    /// 0 index for levels unlock, 1 index for coins, 2 index for dialy challenge levels unlock
    /// </summary>
    /// <param name="index"></param>
    /// <param name="val"></param>
    public void Decrement(int index, int val)
    {
        PlayerProgress.gameData.ImportantValues[index] -= val;
        SaveData();
    }

    /// <summary>
    /// 0 index for levels unlock, 1 index for coins, 2 index for dialy challenge levels unlock
    /// </summary>
    /// <param name="index"></param>
    /// <param name="finalVal"></param>
    public void SetValue(int index, int finalVal)
    {
       PlayerProgress.gameData.ImportantValues[index] = finalVal;
        SaveData();
    }
    #endregion /Saved Games
}

public enum SignInEvent
{
    None,
    LeaderBoard,
    Achievement
}
  

   
