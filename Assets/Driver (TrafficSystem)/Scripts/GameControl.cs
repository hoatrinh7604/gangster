using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using GameAnalyticsSDK;
//using UnityEngine.Advertisements;
public enum ControlMode { simple = 1, touch = 2 }
public class GameControl : MonoBehaviour {

    public static GameControl manager;
	//public static GameControl instance;

	public bool pauseescape = false;
    public static float accelFwd,accelBack;
    public static float steerAmount;
	public Text Timertext;

    public static bool shift;
    public static bool brake;
    public static bool driving;
    public static bool jump;
	public static bool Punching;
	public GameObject Bigmap1;
	public GameObject minimap;
	public GameObject pausebtn;
	public GameObject Playercont;
	public GameObject tchmtr;
	public GameObject taskpp;
	public GameObject Exitpanel;




	//public GameObject Minimap;

	public GameObject veh;

	public GameObject CollecteboxPannels;
	public GameObject levelfailed;
	//public GameObject PUNCH;
	//public GameObject vehcont;





    public ControlMode controlMode = ControlMode.simple;

    public GameObject getInVehicle;


    private VehicleCamera vehicleCamera;
    private float drivingTimer=0.0f;
    public void VehicleAccelForward(float amount) 
	{ 
		accelFwd = amount;
		print (amount);
	}
    public void VehicleAccelBack(float amount) { accelBack = amount; }
    public void VehicleSteer(float amount) { steerAmount = amount; }
    public void VehicleHandBrake(bool HBrakeing) { brake = HBrakeing; }
    public void VehicleShift(bool Shifting) { shift = Shifting; }
    public void GetInVehicle() { if (drivingTimer == 0) { driving = true; drivingTimer = 3.0f; } }
    public void GetOutVehicle() { if (drivingTimer == 0) { driving = false; drivingTimer = 3.0f; } }
    public void Jumping() { jump = true; }
//	public void Punch() { Punching = true; }

    void Awake()
    {
		
        manager = this;
		driving = false;
    }
    void Start()
    {
        //		if (gameConfigs.mee) 
        //		{
        //
        //			gameConfigs.mee.runActions (gameConfigs.INGAME_page,1);
        //			PlusButtonsAPIExample.mee.HideButtons ();
        //
        //		}
        //if (GirlGameConfigs.mee) {
        //	GirlGameConfigs.mee.showRotationAds (1,AdsPageType.ingame);
        //}
        AdsManager.Instance.SetStatus(ReachedPage.InGame);
        vehicleCamera = AIContoller.manager.vehicleCamera;
		//MONSTER REM GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "Level" + PlayerPrefs.GetInt("Levelclick").ToString());
	}

	public void Yes()
	{
		Time.timeScale = 1;
		Application.LoadLevel ("LevelSelection");

	}


	public void No()
	{
		Exitpanel.SetActive (false);
		minimap.SetActive (true);
		pausebtn.SetActive (true);
		Playercont.SetActive (true);
		veh.SetActive (true);
		tchmtr.SetActive (true);
		Time.timeScale = 1;


	}

	public void Noads()
	{
		print ("Noads");
        //		GoogleIAB.purchaseProduct (InAppPurchaseManager.allSkus [0]);
        //GoogleIAB.purchaseProduct (GirlGameConfigs.mee.skus [0]);
        Purchaser.Instance.BuyProductID(0);
	}


	public void unlockeveryting()
	{
		print ("unlockeveryting");
        //		GoogleIAB.purchaseProduct (InAppPurchaseManager.allSkus [2]);
        //GoogleIAB.purchaseProduct (GirlGameConfigs.mee.skus [2]);
        Purchaser.Instance.BuyProductID(2);
    }





    void Update()
    {
//		if (Input.GetKeyDown (KeyCode.Escape)) {
//			Exitpanel.SetActive (true);
//			minimap.SetActive (false);
//			pausebtn.SetActive (false);
//			Playercont.SetActive (false);
//			veh.SetActive (false);
//			tchmtr.SetActive (false);
//			Time.timeScale = 0;
//		}
//			if (pauseescape)
//			{
//				pauseescape=false;
//				Bigmap1.SetActive (false);
//				pausebtn.SetActive (true);
//				veh.SetActive (true);
//				tchmtr.SetActive (true);
//				Time.timeScale = 1;
//			}
//			if (!pauseescape) {
//				Exitpanel.SetActive (true);
//				minimap.SetActive (false);
//				pausebtn.SetActive (false);
//				Playercont.SetActive (false);
//				veh.SetActive (false);
//				tchmtr.SetActive (false);
//				Time.timeScale = 0;
//			}
				
			

		 




        drivingTimer = Mathf.MoveTowards(drivingTimer,0.0f,Time.deltaTime);

		if (Timer.levelfailedz)
		{
			print (levelfailed);

			Invoke ("LevelFail",2.5f);


		}
//		if (vehcont) {
//			PUNCH.SetActive (false);     
//		} 

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

	}

	void LevelFail()
	{
		

		levelfailed.SetActive (true);
		minimap.transform.position = new Vector3 (1000,1000,1000);
		Invoke ("ShowAd",0.8f);
		Invoke ("timezzzz",1.5f);

	}


	public void Rate()
	{

		print ("Rate");
		Application.OpenURL ("https://play.google.com/store/apps/details?id=com.rcgames.sanandreascrimegang2017&hl=en");

	}

	public void LCShare()
	{
		print ("LcShare");

	}





	void timezzzz()
	{
		//Time.timeScale = 0;

	}

    public void CameraSwitch()
    {
        vehicleCamera.Switch++;
        if (vehicleCamera.Switch > vehicleCamera.cameraSwitchView.Count) { vehicleCamera.Switch = 0; }
    }



	public void Pause()
	{

		//Timer.instance.stop = true;
		pauseescape=true;
		Bigmap1.SetActive (true);
		minimap.SetActive (false);
		pausebtn.SetActive (false);
		Playercont.SetActive (false);
		veh.SetActive (false);
		tchmtr.SetActive (false);
		Time.timeScale = 0;
	}

	public void pauseclose()
	{
		pauseescape=false;
		Bigmap1.SetActive (false);
		pausebtn.SetActive (true);
		veh.SetActive (true);
		tchmtr.SetActive (true);
		Time.timeScale = 1;


	}

	public void BoxColletedOkay()
	{
		print ("BoxColletedOkay");
		CollecteboxPannels.SetActive (false);

	}


	public void BeerBoxNext()
	{
		Time.timeScale = 1;
		print ("1st Level NextLevel");
		if (PlayerPrefs.GetInt ("Levelclick") == PlayerPrefs.GetInt ("LevelUnlock")) {

			PlayerPrefs.SetInt ("LevelUnlock", PlayerPrefs.GetInt ("LevelUnlock") + 1);
			print ("done"+PlayerPrefs.GetInt ("LevelUnlock"));
		}

		Application.LoadLevel ("LevelSelection");


	}





	public void NextBt()
	{
		if (PlayerPrefs.GetInt ("Levelclick") == PlayerPrefs.GetInt ("LevelUnlock")) {
			
			PlayerPrefs.SetInt ("LevelUnlock", PlayerPrefs.GetInt ("LevelUnlock") + 1);
			print ("done"+PlayerPrefs.GetInt ("LevelUnlock"));
		}
			print ("NextLevel");
			Application.LoadLevel ("LevelSelection");
			Time.timeScale = 1;


	}
	 
	public void Mg()
	{
		Debug.Log ("Moregames");
		Application.OpenURL("market://search?q=pub:Redcorner Games");
	}


	public void Menu()
	{
		Time.timeScale = 1;
		Debug.Log ("Menu");
		Application.LoadLevel ("Menu");
	}
	public void TaskOk()
	{
		

//		Playercont.SetActive (true);
		minimap.SetActive (true);
		taskpp.SetActive (false);
	}


	public void retry()
	{
		Timer.levelfailedz = false;
		Time.timeScale = 1;
		Debug.Log ("retry");

		Application.LoadLevel (Application.loadedLevel);
	}



}
