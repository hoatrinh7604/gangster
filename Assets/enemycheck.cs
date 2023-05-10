using UnityEngine;
using System.Collections;
//using UnityEngine.Advertisements;

public class enemycheck : MonoBehaviour {
	public GameObject LF;
	public GameObject Lc;
	public GameObject Map;
	public GameObject Ragdols;
//	public GameObject Ragdol2;
//	public GameObject Ragdol3;
	int i = 0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	public void OnTriggerEnter(Collider Other)
	{
			if (Other.gameObject.CompareTag("Hoodmemtrig")) 
		{
			print ("failed");

			Invoke ("LFD",1.5f);
		}

		if (Other.gameObject.CompareTag("HoodCar")) 
		{
			i++;
			print ("comp");
			if (i==1) {
				print ("comp");
				gameObject.transform.position = new Vector3 (1000,0,0);
				Ragdols.SetActive (true);
				Invoke ("Lcd",1.5f);
			}

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
        //		if (Advertisement.IsReady ()) {
        //
        //
        //			Advertisement.Show ();
        //
        //
        //		} else 
        //		{
        //			gameConfigs.mee.jarToast ("Video is getting ready, please wait");
        //
        //		}
        //if (GirlGameConfigs.mee) {
        //	GirlGameConfigs.mee.showRotationAds (1,AdsPageType.prelf);
        //}
        AdsManager.Instance.SetStatusWithDelay(ReachedPage.LevelFail, AdsManager.Instance.levelFailAdDelay);
      //  Invoke("MakeInGame", 2);
    }


	public void LFD()
	{
		Map.transform.position = new Vector3 (1000,1000,1000);
		LF.SetActive (true);
		Invoke ("ShowAd",0.5f);
		Invoke ("tim",1.5f);

	}

	void tim()
	{
		//Time.timeScale = 0;

	}

	public void Lcd()
	{
		Map.transform.position = new Vector3 (1000,1000,1000);
		Lc.SetActive (true);
		Invoke ("ShowAd",0.5f);
		Invoke ("tim",1.5f);

	}
    void MakeInGame()
    {
        AdsManager.Instance.SetStatus(ReachedPage.InGame);
    }











}
