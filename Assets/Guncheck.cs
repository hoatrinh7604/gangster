using UnityEngine;
using System.Collections;

public class Guncheck : MonoBehaviour {

	    public GameObject Gun1;
		public GameObject Gun2;
		public GameObject Gun3;

	void Start()
	{

				int gunind = PlayerPrefs.GetInt ("GunNumber",1);
				if (gunind == 1)
				{
					Gun1.SetActive (true);
					Gun2.SetActive (false);
					Gun3.SetActive (false);
				}
				if (gunind == 2)
				{
					Gun1.SetActive (false);
					Gun2.SetActive (true);
					Gun3.SetActive (false);
				}
				if (gunind == 3)
				{
					Gun1.SetActive (false);
					Gun2.SetActive (false);
					Gun3.SetActive (true);
		}
	}
	// Update is called once per frame
	void Update ()
	{
	
	}
}
