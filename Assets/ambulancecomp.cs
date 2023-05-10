using UnityEngine;
using System.Collections;

public class ambulancecomp : MonoBehaviour {

	public GameObject LevelComp;
	public GameObject bustrigger;
	public GameObject mapz;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void comp ()
	{
		LevelComp.SetActive (true);
		mapz.transform.position= new Vector3(1000,1000,1000);
		PlayerPrefs.SetInt ("totalscore",PlayerPrefs.GetInt ("totalscore")+2000);
        AdsManager.Instance.SetStatusWithDelay(ReachedPage.LevelFail, AdsManager.Instance.levelFailAdDelay);
    }

	void OnTriggerEnter(Collider Other)

	{

		if (Other.CompareTag("Ambulanceparking")) 
		{
			print ("done");
			Invoke ("comp",2.8f);

		}



		if (Other.CompareTag("Bustrig")) 
		{
			print ("bustrig");
			bustrigger.SetActive (true);

			
		}



	}

}
