using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class bar : MonoBehaviour {

	public Image barz;

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{

		if (barz.fillAmount==0) 		
		{
			print ("working");
		}
	}


	public void  btn()
	{
		barz.fillAmount -= 0.1f;
	}

}
