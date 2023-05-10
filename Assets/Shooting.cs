using UnityEngine;
using System.Collections;

public class Shooting : MonoBehaviour {


	public GameObject Guns;
	public GameObject hands;
	public GameObject revalv;
	public GameObject Crosshair;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	public	void disableGuns ()
	{

		//Animator anim = gameObject.GetComponentInParent<Animator>();
		//anim.Play ("gundiac");
		//Invoke ("gundiactive",0.75f);
	}

	void gundiactive()
	{
		//Guns.SetActive (false);
		//revalv.SetActive (true);
		//hands.SetActive (false);
		//Crosshair.SetActive (false);
	}



	public	void EnabeGuns ()
	{

		//Animator anim = gameObject.GetComponentInParent<Animator>();
		//anim.Play ("Take 001");

		//Invoke ("gunactive",0.95f);

	
	}
	void gunactive()
	{
		//Guns.SetActive (true);
		//hands.SetActive (true);
		//revalv.SetActive (false);
		//Crosshair.SetActive (true);

	}


	public  void gun()
	{

		print ("clicked");
		//Animator anim = gameObject.GetComponentInParent<Animator>();
		//anim.Play ("mixamo.com");
	}
}
