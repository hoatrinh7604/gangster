using UnityEngine;
using System.Collections;

public class carreached : MonoBehaviour {


	public GameObject DeleveredBoxPannel;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()

	{
	
	}


	public void OnTriggerEnter(Collider Other)
	{
		


		if (Other.CompareTag("BeerBoxDestinationTrigger"))
		{
			print ("Beer Box Delevered");

			Invoke ("LevelComplete",2f);

		}


}


	void LevelComplete()
	{

		DeleveredBoxPannel.SetActive (true);
        AdsManager.Instance.SetStatusWithDelay(ReachedPage.LevelFail, AdsManager.Instance.levelFailAdDelay);

    }

}
