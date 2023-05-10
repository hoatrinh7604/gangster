using UnityEngine;
using System.Collections;

public class store : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{

//		if (Input.GetKeyDown(KeyCode.Escape))
//		{
//			Application.LoadLevel ("Menu");
//		}
	
	}
	public void Close()
	{
		print ("close");
		Application.LoadLevel ("Menu");
	}

	public void Buy1()
	{
		print ("buy1");
        //		GoogleIAB.purchaseProduct (InAppPurchaseManager.allSkus [0]);
        //GoogleIAB.purchaseProduct (GirlGameConfigs.mee.skus [0]);
        Purchaser.Instance.BuyProductID(0);
    }
	public void Buy2()
	{
		print ("buy2");
        //		GoogleIAB.purchaseProduct (InAppPurchaseManager.allSkus [2]);
        //GoogleIAB.purchaseProduct (GirlGameConfigs.mee.skus [2]);
        Purchaser.Instance.BuyProductID(2);
    }
	public void Buy3()
	{print ("buy3");
        //		GoogleIAB.purchaseProduct (InAppPurchaseManager.allSkus [1]);
        //GoogleIAB.purchaseProduct (GirlGameConfigs.mee.skus [1]);
        Purchaser.Instance.BuyProductID(1);
    }
}
