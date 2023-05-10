using UnityEngine;
using System.Collections;

public class GameConstants : MonoBehaviour {

    public const string GAME_OPENED_FIRST_TIME = "GameOpenedForFirstTime";
    public const string PLAYER_AUTHENTICATED = "PlayerAuthenticated";
    public const string HIGHSCORE_PGS = "PlayGameServicesHighScore";
    public const string COINS_PLAYERPREF = "CoinsHave";
    public const string INSTALLCOUNT_INCREASE = "CountIncreasedOnInstall";
    public const string NOADS = "NoAdsRequired";
    public const string MENUAD_VERSION = "MenuAdVersion";
    public const string MENUAD_DOWNLOADED = "MenuAdDownloadedDay";
    public const string RATED = "RatedAlready";
    public const string SHARED = "SharedAlready";
    public static string PERSISTENT_PATH = Application.persistentDataPath;
    public const string LANGUAGE_INDEX = "LanguageIndex";
    public const string NOTIFICATION_IDS = "NotificationIds";
    public const string PROMOS_USED = "PromoSuccessCodes";
    public const string FB_PACKAGENAME = "com.facebook.katana";
    public const string TWITTER_PACKAGENAME = "com.twitter.android";
    public const string WHATSAPP_PACKAGENAME = "com.whatsapp";

    public const string PLAYER_TAG = "Player";
    public const string PLAYER_WEAPON_LAYER = "PlayerWeapon";
    public const string CHARACTERLAYER = "Character";
    public bool IS_APPLY_MONSTER = true;
}
