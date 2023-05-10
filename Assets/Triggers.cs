using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using GameAnalyticsSDK;
//using UnityEngine.Advertisements;
public class Triggers : MonoBehaviour {


	public GameObject CollectedBoxPannel;
	public GameObject DeleveredBoxPannel;
	public GameObject beercasedest;
	public GameObject gangsterdest;
	public GameObject PoliceDest;
	public GameObject Handletrig;
	public GameObject Levelcomp;
	public GameObject LevelFail;
	public GameObject fadecam;
	int k = 2000;
	public GameObject okbtngngstr;
	public GameObject gangsterobj;
	public GameObject minimap;
	public GameObject Playercontro;
	public GameObject beerbox;



	public string gangster;
	public Text gangstertext;
	public int TempCountForContinueBtn;
	void Awake()
	{
//		if (gameConfigs.mee) 
//		{
//
//			gameConfigs.mee.runActions (gameConfigs.INGAME_page,1);
////			PlusButtonsAPIExample.mee.ShoweButtons ();
//
//		}
	}


	// Use this for initialization
	void Start ()
	{
		if (gangstertext == null) return;
		gangster = gangstertext.text;
		gangstertext.text = "";

//		if (Advertisement.isInitialized == false) {
//			Advertisement.Initialize ("1454972");
//		}
	}
	
	// Update is called once per frame
	void beerboxcol ()
	{
        //		if (Advertisement.IsReady ()) {
        //
        //
        //			Advertisement.Show ();
        //
        //
        //		} else 
        //		{
        //			if (gameConfigs.mee) 
        //			{
        //				gameConfigs.mee.runActions (gameConfigs.LEVELFAIL_page, 2);
        //			}
        //			//gameConfigs.mee.jarToast ("Video is getting ready, please wait");
        //
        //		}
        //if (GirlGameConfigs.mee) {
        //	GirlGameConfigs.mee.showRotationAds (1,AdsPageType.prelf);
        //}
        AdsManager.Instance.SetStatusWithDelay(ReachedPage.LevelFail, AdsManager.Instance.levelFailAdDelay);
        Invoke("MakeInGame", 2);
        CollectedBoxPannel.SetActive (true);
		beercasedest.SetActive (true);
		Playercontro.SetActive (false);
		beerbox.SetActive (false);
//		minimap.SetActive (false);

	}

	public void OnTriggerEnter(Collider Other)
	{

		if (Other.gameObject.name == "sachin") 
		{
			print ("123456");
//			Enemyscript.instance.activateenemy = true;
			PlayerPrefs.SetInt("ActivateEnemy",1);
		}




		if (Other.CompareTag("BeerBoxTrigger"))
		{
			print ("Beer Box Collected");
			Invoke ("beerboxcol",1f);
		

		}





		if (Other.CompareTag("BeerBoxDestinationTrigger"))
		{
			print ("Beer Box Delevered");
			//Invoke ("ShowAd",0.35f);
			Invoke ("LevelComplete",1.2f);

		}


		if (Other.CompareTag("Police destination"))
		{
			print ("Police destination");

			Invoke ("PoliceReached",1.2f);

		}

		if (Other.CompareTag("Gangstertrig"))
		{
			print ("Gangstertrig");
			Invoke ("adz",1f);
			fadecam.SetActive (true);



			//CollectedBoxPannel.SetActive (true);
			gangsterdest.SetActive (true);

			Invoke ("fadein",0.75f);

		

			Invoke ("coro",1.5f);
			Invoke ("fadeout",5f);
			Invoke ("gngstroff",4f);

		}

		if (Other.CompareTag("GangstertDest") )
		{
			//print ("GangstertDest");
			if(Timer.levelfailedz)
			{
				
				print ("GangstertDest");

			}
		}
			
	}

	void gngstroff()
	{	
		gangsterobj.SetActive (false);

	}

	void adz()
	{
        //		if (Advertisement.IsReady ()) {
        //
        //
        //			Advertisement.Show ();
        //
        //
        //		} else 
        //		{
        //			if (gameConfigs.mee) 
        //			{
        //				gameConfigs.mee.runActions (gameConfigs.LEVELFAIL_page, 2);
        //			}
        //			///gameConfigs.mee.jarToast ("Video is getting ready, please wait");
        //
        //		}
        //if (GirlGameConfigs.mee) {
        //	GirlGameConfigs.mee.showRotationAds (1,AdsPageType.prelf);
        //}
        AdsManager.Instance.SetStatusWithDelay(ReachedPage.LevelFail, AdsManager.Instance.levelFailAdDelay);
        Invoke("MakeInGame", 2);
    }


	void coro()
	{	
		StartCoroutine (Boss2TypeText () );
		
	}



		
	void texT()
	{
		gangstertext.gameObject.SetActive (false);
	}


	IEnumerator Boss2TypeText () 
	{

		foreach (char letter in gangster.ToCharArray()) 
		{
			//						Debug.Log("Test");
			gangstertext.text += letter;
			TempCountForContinueBtn=TempCountForContinueBtn+1;
			//			if(TempCountForContinueBtn>=135)
			//BossContinueBtn2.gameObject.SetActive(true);
			yield return 0;
			yield return new WaitForSeconds (0.003f);
		}      
	}


	public void ShowAd()
	{
        //		if (Advertisement.IsReady ()) {
        //
        //
        //			Advertisement.Show ();
        //
        //
        //		} else 
        //		{
        //			if (gameConfigs.mee) 
        //			{
        //				gameConfigs.mee.runActions (gameConfigs.LEVELFAIL_page, 2);
        //			}
        //
        //		}
        //if (GirlGameConfigs.mee)
        //{
        //	GirlGameConfigs.mee.showRotationAds (1,AdsPageType.prelf);
        //}
        AdsManager.Instance.SetStatusWithDelay(ReachedPage.LevelFail, AdsManager.Instance.levelFailAdDelay);
        Invoke("MakeInGame", 2);
        print("ad");

//		if (Advertisement.IsReady ("rewardedVideo")) {
//			print("ad");
//
//			Advertisement.Show ("rewardedVideo", new ShowOptions {
//				resultCallback = result => {
//					//Failed, Skipped, Finished
//					Debug.Log ("UnityAdsss=" + result.ToString ());
//					if (result.ToString () == "Finished") {
//
//
//					}
//				}
//			});
//		} else 
//		{
//			gameConfigs.mee.jarToast ("Video is getting ready, please wait");
//
//		}

	}

	void LevelComplete()
	{
		

		DeleveredBoxPannel.SetActive (true);
		minimap.transform.position = new Vector3 (1000,1000,1000);
		PlayerPrefs.SetInt ("totalscore",PlayerPrefs.GetInt ("totalscore")+2000);
		Invoke ("ShowAd",0.85f);
		Invoke ("timeq",1.6f);
		//MONSTER REM GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "Level" + PlayerPrefs.GetInt("Levelclick").ToString());
	}


	void timeq()
	{
		//Time.timeScale = 0;

	}

	void PoliceReached()
	{

        //		if (Advertisement.IsReady ()) {
        //
        //
        //			Advertisement.Show ();
        //
        //
        //		} else 
        //		{
        //			if (gameConfigs.mee) 
        //			{
        //				gameConfigs.mee.runActions (gameConfigs.LEVELFAIL_page, 2);
        //			}
        //			//gameConfigs.mee.jarToast ("Video is getting ready, please wait");
        //
        //		}
        //if (GirlGameConfigs.mee) {
        //	GirlGameConfigs.mee.showRotationAds (1,AdsPageType.prelf);
        //}
       
        
        PoliceDest.SetActive (true);

		Handletrig.SetActive (true);

        Invoke("MakeInGame", 2);
        AdsManager.Instance.SetStatusWithDelay(ReachedPage.LevelFail, AdsManager.Instance.levelFailAdDelay);

    }


	void fadein()
	{
		fader.instance.FadeIn (false);
	}
	void fadeout()
	{

		okbtngngstr.SetActive (true);

	}

	public void OkBtn()
	{
		minimap.SetActive (true);
		fader.instance.FadeIn (true);

		okbtngngstr.SetActive (false);

		fadecam.SetActive (false);



	}

    void MakeInGame()
    {
        AdsManager.Instance.SetStatus(ReachedPage.InGame);
    }
}
