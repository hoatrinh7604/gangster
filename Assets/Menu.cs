using UnityEngine;
using System.Collections;

//using UnityEngine.Advertisements;
public class Menu : MonoBehaviour {
	public GameObject SettingsPanel;
	public GameObject MenuPanel;
	public GameObject QuitPanel;
	public GameObject Musicon;
	public GameObject Musicoff;

	static float j;


	void Awake()
	{
//		if (Advertisement.isInitialized == false) {
//			Advertisement.Initialize ("1454972");
//		}

	}
	// Use this for initialization
	void Start () {
		
		Timer.levelfailedz = false;
		//	if (Advertisement.IsReady ()) {
		//
		//
		//		Advertisement.Show ();
		//
		//
		//	} else 
		//	{
		//		gameConfigs.mee.jarToast ("Video is getting ready, please wait");
		//
		//	}

		//		PlayerPrefs.DeleteAll ();
		//		j = PlayerPrefs.GetInt ("Sachin",3);
		//		PlayerPrefs.SetInt ("unlockallguns",3);
		//		if (gameConfigs.mee) 
		//		{
		//			
		//			gameConfigs.mee.runActions (gameConfigs.MENU_page,1);
		////			PlusButtonsAPIExample.mee.ShoweButtons ();
		//
		//		}
		//		if (gameConfigs.mee) 
		//		{
		//
		//			gameConfigs.mee.runActions (gameConfigs.INGAME_page,2);
		//			PlusButtonsAPIExample.mee.ShoweButtons ();
		//
		//		}

		//if (GirlGameConfigs.mee) {
		//	GirlGameConfigs.mee.showRotationAds (1,AdsPageType.menu);
		//}

		//AdsManager.Instance.SetStatus(ReachedPage.Menu); //MONSTER ADS REM
	}

	public void Yes()
	{
		Application.Quit ();
	}

	public void no()
	{
		QuitPanel.SetActive (false);
		MenuPanel.SetActive (true);
	}


	public void GPlusSignIn()
	{
		//		if (!PlayGameServices.isSignedIn ()) {
//		PlayGameServices.authenticate ();

		//		} else
		//		{
		//			PlayGameServices.signOut ();
		//
		//		}
		////		Invoke ("Checkgplusimage", 1);
	}
	void HandleauthenticationSucceededEvent (string obj)
	{
//		CheckGoogleSignin ();
		//		iTween.ScaleFrom(GplusParent,iTween.Hash("x",GplusParent.transform.localScale.x * 0.65f,"y",GplusParent.transform.localScale.y *1.35f,"time",1f,"easetype",iTween.EaseType.easeOutElastic));
//		if (GameConfigs2015.ShowAchievementsOnSignIn) {
//			GSConfig.ShowAchievements ();
//		}
//		if (GameConfigs2015.ShowLeaderBoardsOnSignIn) {
//			GSConfig.ShowLeaderBoard ();
//		}
//
//		if (GameConfigs2015.ShowSavedGamessOnSignIn) {
//			GSConfig.ShowSavedGames ();
//		}


	}
	// Update is called once per frame
	void Update () 
	{
//		if (Input.GetKeyDown(KeyCode.Escape))
//		{
//			QuitPanel.SetActive (true);
//			MenuPanel.SetActive (false);
//			SettingsPanel.SetActive (false);
//		}
//		print (PlayerPrefs.GetInt ("Sachin"));
	}
	public void Ldr()
	{
		print ("ldr");
//		GSConfig.ShowLeaderBoard ();
	}
	public void ach()
	{
		print ("ach");
//		GSConfig.ShowAchievements ();
	}
		

	public void shr()
	{
		print ("shr");
//		FBMainMenu.Instance.ShareLevelCompleteNormal ();

	}
	public void Noads()
	{
		print ("Noads");
        //		GoogleIAB.purchaseProduct (InAppPurchaseManager.allSkus [0]);
        //GoogleIAB.purchaseProduct (GirlGameConfigs.mee.skus [0]);
        Purchaser.Instance.BuyProductID(0);
    }

	public void store()
	{
		print ("store");
		Application.LoadLevel ("store");

	}

	public	void Play()
	{

		print ("Play");
		Application.LoadLevel ("GunSelection");
	}


	public	void Settings()
	{
		print ("Settings");
		MenuPanel.SetActive (false);
		SettingsPanel.SetActive (true);			
	}


	public	void SettingsClose()
	{
		print ("SettingsClose");
		MenuPanel.SetActive (true);
		SettingsPanel.SetActive (false);			
	}




	public	void MoreGames()
	{

		print ("MoreGames");

		Application.OpenURL("market://search?q=pub:Redcorner Games");

	}
	public	void MusicOff()
	{
		
		print ("off");
		Musicon.SetActive (true);
		Musicoff.SetActive (false);
		AudioListener.volume = 0;
	

	}

	public	void MusicOn()
	{

		print ("on");
		Musicon.SetActive (false);
		Musicoff.SetActive (true);
		AudioListener.volume = 1;

	}





}
