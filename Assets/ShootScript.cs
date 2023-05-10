using UnityEngine;
using System.Collections;
//using UnityEngine.EventSystems;

public class ShootScript : MonoBehaviour  {
	public GameObject Bullet;
	public GameObject  cameraRef;
	public Animator anim;
	public Animator anim2;
	public Animator anim3;
	public Animator anim4;
	public Animator anim5;
//	public Animator anim6;
	public GameObject Shootsound;
	public GameObject Text;
	public bool Shooting=false;
	public static ShootScript instance;
	public GameObject comp;
	public GameObject mapmini;
	private bool Pressing2;
	private bool tempboo;
	//	public static ShootScript Inst;
	public int  i =0;
	public int j =2;
    public bool temp;

	void Start () 
	{
		instance = this;
		tempboo = true;
        temp = true;
	}


	//	public void OnPointerClick(PointerEventData eventData){
	//
	//		Pressing = true;
	//
	//	}
//	public void OnPointerDown(PointerEventData eventData){
//
//		Pressing = true;
//
//
//
//	}
//
//	public void OnPointerUp(PointerEventData eventData){
//
//		Pressing = false;
//
//	}

	public void PressedFun()
	{

		if (tempboo) {
			PlayerCamera.instance.initShootCam ();
			tempboo = false;
		}

		Shoot ();
		PlayerCamera.instance.camTask = CamTask.Shoot;
		Shooting = true;
		Camera.main.fieldOfView = 45;
		GameUI.instance.ShowCrosshair (true);
		Animator anim = gameObject.GetComponentInParent<Animator>();
		anim.Play ("mixamo.com");




	}


	public void PressedExit()
	{

		Shootsound.gameObject.GetComponent<AudioSource> ().enabled = false;

		tempboo = true;
		Shooting = false;
		PlayerCamera.instance.camTask = CamTask.Rotate;
		Camera.main.fieldOfView = 60;
		GameUI.instance.ShowCrosshair (false);
		Animator anim = gameObject.GetComponentInParent<Animator>();
		anim.Play ("Grounded");
//		Invoke("Foranimator",5f);

	}

	void ff()
	{
		comp.SetActive (true);
		mapmini.transform.position = new Vector3 (1000,1000,1000);
		PlayerPrefs.SetInt ("totalscore",PlayerPrefs.GetInt ("totalscore")+2000);
		//Time.timeScale = 0;

	}

	void Compcall()
	{
        if (temp)
        {
            temp = false;
            comp.SetActive(true);
            AdsManager.Instance.SetStatusWithDelay(ReachedPage.LevelFail, AdsManager.Instance.levelFailAdDelay);
        }
        //Time.timeScale = 0;
    }


	void Update () 
	{



		if (i==j) 
		{


			mapmini.transform.position = new Vector3 (1000,1000,1000);
			PlayerPrefs.SetInt ("totalscore",PlayerPrefs.GetInt ("totalscore")+2000);
			Invoke ("Compcall",3f);
			
		}

		if (Input.GetKeyDown (KeyCode.F))
		{
			PlayerCamera.instance.initShootCam ();
			PlayerCamera.instance.camTask = CamTask.Shoot;
			Camera.main.fieldOfView = 50;
			GameUI.instance.ShowCrosshair (true);
			Animator anim = gameObject.GetComponentInParent<Animator>();
			anim.Play ("mixamo.com");

		}
		if (Input.GetKey (KeyCode.F))
		{
			PlayerCamera.instance.camTask = CamTask.Shoot;

			Shoot ();
		}
		if (Input.GetKeyUp (KeyCode.F))
		{
//			Shooting = false;
			PlayerCamera.instance.camTask = CamTask.Rotate;
			Camera.main.fieldOfView = 60;
			GameUI.instance.ShowCrosshair (false);
			Animator anim = gameObject.GetComponentInParent<Animator>();
			anim.Play ("Grounded");
			//		Invoke("Foranimator",5f);

		}
	}

	public void Shoot()
	{
		if (RCCUIController.prezzing) {
			Shootsound.gameObject.GetComponent<AudioSource> ().enabled = true;
		} 

		//		GameObject bullet = GameObject.Instantiate (Bullet.gameObject, transform.position, transform.rotation) as GameObject;
		//		Ray ray =Camera.main.ScreenPointToRay(scr
		RaycastHit hit;
		if (Physics.Raycast (Camera.main.transform.position, Camera.main.transform.forward, out hit, 300)) 
		{
			print ("Shoot"+hit.transform.tag);

			Debug.Log ("Hitted"+hit.transform.tag);
			if (hit.transform.tag == "EnemyHead") 
			{
				Text.SetActive (true);
				i += 1;
				print ("done");
				PlayerCamera.instance.PlayerLookTarget (hit.point);
				anim.Play ("Dead_2");
				anim2.Play ("Dead_2");
				//				anim3.Play ("Dead_2");
				anim4.Play ("Dead_2");
//				anim6.Play ("Dead_2");
				anim5.Play ("Dead_2");
			}

			if (hit.transform.tag == "enemyhead2") 
			{
				Text.SetActive (true);
				i += 1;
				print ("done2");
				PlayerCamera.instance.PlayerLookTarget (hit.point);
				//anim.Play ("Dead_2");

				anim3.Play ("Dead_2");
				anim4.Play ("Dead_2");
//				anim6.Play ("Dead_2");
				anim5.Play ("Dead_2");
			}



		}

	}
}
