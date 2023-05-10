using UnityEngine;
using System.Collections;

public class Loadingscene : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{

		Invoke ("levelstart",2f);
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}


	void levelstart()
	{
		if (PlayerPrefs.GetInt ("Levelclick")==1)
		{
			if (PlayerPrefs.GetInt("IsFirstRun", 0) == 0)
			{
				PlayerPrefs.SetInt("IsFirstRun", 1);
				Application.LoadLevel("Tutorial");
			}
			else
            {
				Application.LoadLevel("Level1");
			}
		}
		if (PlayerPrefs.GetInt ("Levelclick")==2)
		{
			Application.LoadLevel ("Level HoodMem2");
		}
		if (PlayerPrefs.GetInt ("Levelclick")==3)
		{
			Application.LoadLevel ("Level SchoolBus Driving3");
		}
		if (PlayerPrefs.GetInt ("Levelclick")==4)
		{
			Application.LoadLevel ("Level2drugdriver7");
		}if (PlayerPrefs.GetInt ("Levelclick")==5)
		{
			Application.LoadLevel ("Level3barehands8");
		}if (PlayerPrefs.GetInt ("Levelclick")==6)
		{
			Application.LoadLevel ("Level2Ambulancedriving5");
		}if (PlayerPrefs.GetInt ("Levelclick")==7)
		{
			Application.LoadLevel ("Level5Gangster9");
		}if (PlayerPrefs.GetInt ("Levelclick")==8)
		{
			Application.LoadLevel ("Level HoodMemnewtest");
		}if (PlayerPrefs.GetInt ("Levelclick")==9)
		{
			Application.LoadLevel ("Level Shooting new 6");
		}if (PlayerPrefs.GetInt ("Levelclick")==10)
		{
			Application.LoadLevel ("Level6killingatbeach10");
		}
	}




}
