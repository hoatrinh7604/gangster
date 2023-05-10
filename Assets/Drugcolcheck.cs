using UnityEngine;
using System.Collections;

public class Drugcolcheck : MonoBehaviour {


	public GameObject LC;
	public GameObject Ragdollz;
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

		if(Other.gameObject.CompareTag ("Drughitcarcol"))
		{

			print("drughitcol");
			Ragdollz.SetActive (true);
			transform.position = new Vector3 (1000,1000,1000);
			Invoke ("LCMP",2.5f);
			
		}
	}


	void LCMP()
	{
		LC.SetActive (true);
      //  Invoke("MakeInGame", 2);
        AdsManager.Instance.SetStatusWithDelay(ReachedPage.LevelFail, AdsManager.Instance.levelFailAdDelay);


    }





}
