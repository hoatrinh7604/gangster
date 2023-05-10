using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class LevelSelection : MonoBehaviour {

	public Button[] LockedLevels;
	public Button[] Levels;
	public static int levelnum = 0;

	// Use this for initialization
	void Start ()
	{
//		PlayerPrefs.DeleteAll ();
//		PlayerPrefs.SetInt ("LevelUnlock",10);
		if (!PlayerPrefs.HasKey ("LevelUnlock"))
			PlayerPrefs.SetInt ("LevelUnlock", 1);

		Timer.levelfailedz = false;
//		levelnum = 4;
		int LevelUnlocked = PlayerPrefs.GetInt ("LevelUnlock");
		int Levelclick = PlayerPrefs.GetInt ("Levelclick");
		print (LevelUnlocked);
		for (int i = 0; i < LockedLevels.Length ; i++)
		{
			if ( i  < LevelUnlocked) 
			{
				LockedLevels [i].gameObject.SetActive (false);
			}
			
		}
		print ( PlayerPrefs.GetInt ("LevelUnlock"));
		print (PlayerPrefs.GetInt ("Levelclick"));
	}
	
	// Update is called once per frame
	void Update ()
	{
	
//		if (Input.GetKeyDown(KeyCode.Escape))
//		{
//			Application.LoadLevel ("GunSelection");
//		}

	}

	public void LsClose()
	{

		Application.LoadLevel ("GunSelection");
	}

	public void btn1()
	{
		Time.timeScale = 1;
		Application.LoadLevel ("Loading");
		PlayerPrefs.SetInt ("Levelclick",1);
		print ("level1");
	}
	public void btn2()
	{
		Time.timeScale = 1;
		PlayerPrefs.SetInt ("Levelclick",2);
		Application.LoadLevel ("Loading");

		print ("level2");
	}public void btn3()
	{Time.timeScale = 1;
		PlayerPrefs.SetInt ("Levelclick",3);
		Application.LoadLevel ("Loading");

		print ("level3");
	}public void btn4()
	{Time.timeScale = 1;
		PlayerPrefs.SetInt ("Levelclick",4);
		Application.LoadLevel ("Loading");

		print ("level4");
	}public void btn5()
	{Time.timeScale = 1;
		PlayerPrefs.SetInt ("Levelclick",5);
		Application.LoadLevel ("Loading");

		print ("level5");
	}public void btn6()
	{Time.timeScale = 1;
		PlayerPrefs.SetInt ("Levelclick",6);
		Application.LoadLevel ("Loading");

		print ("level6");
	}public void btn7()
	{Time.timeScale = 1;
		PlayerPrefs.SetInt ("Levelclick",7);
		Application.LoadLevel ("Loading");
	
		print ("level7");
	}
	public void btn8()
	{Time.timeScale = 1;
		PlayerPrefs.SetInt ("Levelclick",8);
		Application.LoadLevel ("Loading");

		print ("level8");
	}
	public void btn9()
	{Time.timeScale = 1;
		PlayerPrefs.SetInt ("Levelclick",9);
		Application.LoadLevel ("Loading");

		print ("level9");
	}
	public void btn10()
	{Time.timeScale = 1;
		PlayerPrefs.SetInt ("Levelclick",10);
		Application.LoadLevel ("Loading");

		print ("level10");
	}


	public void Unlockalllevels()
	{

		print ("Unlockalllevels");
        //		GoogleIAB.purchaseProduct (InAppPurchaseManager.allSkus [1]);
        //GoogleIAB.purchaseProduct (GirlGameConfigs.mee.skus [1]);
        Purchaser.Instance.BuyProductID(1);
    }


	public void Unlockalllevels_()
	{
		PlayerPrefs.SetInt("LevelUnlock", 20);
		Reload();
		print("Unlockalllevels");
	}

	public void Reload()
    {
		Timer.levelfailedz = false;
		//		levelnum = 4;
		int LevelUnlocked = PlayerPrefs.GetInt("LevelUnlock");
		int Levelclick = PlayerPrefs.GetInt("Levelclick");
		print(LevelUnlocked);
		for (int i = 0; i < LockedLevels.Length; i++)
		{
			if (i < LevelUnlocked)
			{
				LockedLevels[i].gameObject.SetActive(false);
			}

		}
	}

}
