using UnityEngine;
using System.Collections;
//using UnityEngine.Advertisements;

public class tutorial : MonoBehaviour {
	private int i=0;
	public GameObject[] Instr;
	public GameObject OkBtn;
	public GameObject Text;
	public GameObject Box;
	public bool temp =true;

	void Awake()
	{


				
	}


	// Use this for initialization
	void Start ()
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
        //AdsManager.Instance.SetStatus(ReachedPage.InGame);

        //		if (Advertisement.isInitialized == false)
        //		{
        //			Advertisement.Initialize ("1454972");
        //		}
        //		if (gameConfigs.mee) 
        //		{
        //			gameConfigs.mee.runActions (gameConfigs.LEVELFAIL_page, 2);
        //		}


    }
	public void OK()
	{
		i += 1;
	}
	// Update is called once per frame
	void Update () 
	{
		if (i==1) 
		{
			Instr [0].SetActive (false);
			Instr [1].SetActive (true);
		}
		if (i==2) 
		{
			Instr [1].SetActive (false);
			Instr [2].SetActive (true);
		}
		if (i==3) 
		{
			Instr [2].SetActive (false);
			Instr [3].SetActive (true);
		}

		if (i==4) 
		{
			Instr [3].SetActive (false);
			Instr [4].SetActive (true);
		}
		if (i==5) 
		{
			Instr [4].SetActive (false);
			Instr [5].SetActive (true);
		}
		if (i==6) 
		{
			Instr [5].SetActive (false);
			Instr [6].SetActive (true);
		}
		if (i==7) 
		{
			Instr [6].SetActive (false);
			Instr [7].SetActive (true);
		}
		if (i==8) 
		{
			Instr [7].SetActive (false);
			Instr [8].SetActive (true);
			if (temp) {
				Invoke ("Showad", 1f);
				temp = false;
			}
		}
	}

	 
	void Showad()
	{
        //if (GirlGameConfigs.mee) {
        //	GirlGameConfigs.mee.showRotationAds (1,AdsPageType.prelf);
        //}
        AdsManager.Instance.SetStatusWithDelay(ReachedPage.LevelFail, AdsManager.Instance.levelFailAdDelay);
        OkBtn.SetActive (false);
		Invoke ("SAchin",1.5f);
		Invoke ("adz",1f);
		Invoke ("textz",5f);
		Invoke ("levelload",7f);
	}

	public void adz()
	{
		
//		if (gameConfigs.mee) 
//		{
//			Debug.Log ("TaskOk");
//			gameConfigs.mee.runActions (gameConfigs.LEVELFAIL_page, 2);
//		}


//		if (Advertisement.IsReady ()) {
//
//
//			Advertisement.Show ();
//
//
//		}
//		else 
//		{
//					if (gameConfigs.mee) 
//					{
//						Debug.Log ("TaskOk");
//						gameConfigs.mee.runActions (gameConfigs.LEVELFAIL_page, 2);
//					}
//
//		}



	}
	public void SAchin()
	{
		print ("sachin");
		Box.SetActive (true);
		PlayerPrefs.SetInt ("totalscore",PlayerPrefs.GetInt ("totalscore")+500);
	}

	public void textz()
	{
		Text.SetActive (true);
	}
	public void levelload()
	{
		if (PlayerPrefs.GetInt ("Levelclick") == 1)
		{
			Application.LoadLevel ("Level1");
		}

	}



}
