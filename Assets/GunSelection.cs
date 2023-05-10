using UnityEngine;
using System.Collections;
public class GunSelection : MonoBehaviour {
	
	public GameObject Gun1;
	public GameObject Gun2;
	public GameObject Gun2select;
	public GameObject Gun2buy;
	public GameObject Gun3select;
	public GameObject Gun3buy;

	public GameObject Gun3;
	public GameObject Prev;
	public GameObject next;
	public static  int i = 0;
	public int k;
	void Awake()
	{
		

		}

	// Use this for initialization
	void Start ()
	{Timer.levelfailedz = false;

		 k = PlayerPrefs.GetInt ("Sachin");

		int GunNum = PlayerPrefs.GetInt ("GunNumber",1);
//		PlayerPrefs.SetInt("totalscore",20000);
	}	

	public void back()
	{
		print ("close");
		Application.LoadLevel ("Menu");
	}


	// Update is called once per frame
	void Update ()
	{
//		if (Input.GetKeyDown(KeyCode.Escape))
//		{
//			Application.LoadLevel ("Menu");
//		}
//		print (k);

		if (k==3) 
		{
			Gun2select.SetActive (true);
			Gun2buy.SetActive (false);
			Gun3select.SetActive (true);
			Gun3buy.SetActive (false);
		}


		if (i==0) 
		{
			Gun1.SetActive (true);
			Gun2.SetActive (false);
			Gun3.SetActive (false);
			Prev.SetActive (false);
			next.SetActive (true);
		}


		if (i==1) 
		{
			Gun1.SetActive (false);
			Gun2.SetActive (true);
			Gun3.SetActive (false);
			Prev.SetActive (true);
			next.SetActive (true);
		}

		 if (i==2) 
		{
			Gun1.SetActive (false);
			Gun2.SetActive (false);
			Gun3.SetActive (true);
			Prev.SetActive (true);
			next.SetActive (false);
		}

	}

	public void Next()
	{
		i += 1;

	}
	public void Prevs()
	{
		i -= 1;
		print (i);

	}

	public void buy2()
	{
		int i = PlayerPrefs.GetInt("totalscore");
		if (i>=6000)
		{
			PlayerPrefs.SetInt ("totalscore",PlayerPrefs.GetInt ("totalscore")-6000);
			Gun2select.SetActive (true);
			Gun2buy.SetActive (false);
		}



	}


	public void buy3()
	{
		int i = PlayerPrefs.GetInt("totalscore");
		if (i>=8000)
		{
			PlayerPrefs.SetInt ("totalscore",PlayerPrefs.GetInt ("totalscore")-8000);
			Gun3select.SetActive (true);
			Gun3buy.SetActive (false);
		}



	}








	public void Select()
	{
		if (i==0)
		{
			PlayerPrefs.SetInt ("GunNumber",1);
		}

		if (i==1)
		{
			PlayerPrefs.SetInt ("GunNumber",2);
		}
		if (i==2)
		{
			PlayerPrefs.SetInt ("GunNumber",3);
		}
		Application.LoadLevel ("LevelSelection");


	}

	public void Unlockallguns()
	{

		print ("Unlockallguns");
        //		GoogleIAB.purchaseProduct (InAppPurchaseManager.allSkus [2]);
        //GoogleIAB.purchaseProduct (GirlGameConfigs.mee.skus [2]);
        Purchaser.Instance.BuyProductID(2);
    }





}
