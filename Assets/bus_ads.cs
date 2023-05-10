using UnityEngine;
using System.Collections;
//using UnityEngine.Advertisements;

public class bus_ads : MonoBehaviour {
    public bool temp;
    // Use this for initialization
    void Start () {
        //		if (Advertisement.isInitialized == false) {
        //			Advertisement.Initialize ("1454972");
        //		}
        temp = true;
    }
	public void OnTriggerEnter(Collider Other)
	{

		if (Other.gameObject.CompareTag("Adz")) 
		{
			print ("Adz");
            //			if (Advertisement.IsReady ()) {
            //
            //				print ("Adz");
            //				Advertisement.Show ();
            //
            //
            //			} else 
            //			{
            //				 
            //				if (gameConfigs.mee) 
            //				{
            //					gameConfigs.mee.runActions (gameConfigs.LEVELFAIL_page, 2);
            //				}
            //			}
            //if (GirlGameConfigs.mee) {
            //	GirlGameConfigs.mee.showRotationAds (1,AdsPageType.prelf);
            //}
            if (temp)
            {
                temp = false;
                Invoke("MakeInGame", 2);
                AdsManager.Instance.SetStatusWithDelay(ReachedPage.LevelFail, AdsManager.Instance.levelFailAdDelay);
                
            }
        }

	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void MakeInGame()
    {
        AdsManager.Instance.SetStatus(ReachedPage.InGame);
    }

}
