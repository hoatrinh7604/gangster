using UnityEngine;
using System.Collections;

public class beachcartrig : MonoBehaviour {
	public GameObject LC;
//	public GameObject plc;
//	public GameObject vc;
//	public GameObject tcm;
	public GameObject map;
//	public GameObject[] vehc;
	int i;
	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	void OnTriggerEnter(Collider Other)
	{

		if (Other.gameObject.CompareTag("BeachPeople")) 
		{

			print ("BeachPeople");

		
			Invoke ("lcz",1.5f);

			
		}



	}


	void lcz()
	{
		LC.SetActive (true);
//		for (int i = 0; i < 8; i++) {
//			vehc [i].SetActive (false);
//		}

//		plc.SetActive (false);

		map.transform.position = new Vector3 (1000,1000,1000);
        //		vc.SetActive (false);
        //		vc.SetActive (false);
        //		pause.SetActive (false);
        AdsManager.Instance.SetStatusWithDelay(ReachedPage.LevelFail, AdsManager.Instance.levelFailAdDelay);





    }


}
