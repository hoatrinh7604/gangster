using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PunchEnemy : MonoBehaviour {

	public Image barz;

	public GameObject failed;
	public GameObject patiL;
//	public GameObject patiR;
//	public GameObject patiB;


	//	Animator anim;
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
		//	Invoke ("failedz",1.2f);

		}

	}

	void failedz()
	{

		failed.SetActive (true);

		//Time.timeScale = 0;
	}






	void OnTriggerEnter(Collider Other)
	{

		//print ("Enemy Head");
		if (Other.gameObject.CompareTag("Player"))
		{

			patiL.SetActive (true);
//			patiB.SetActive (true);
//			patiR.SetActive (true);
			print ("Player");
			barz.fillAmount -= 0.005f;

			Animator anim = Other.gameObject.GetComponentInParent<Animator>();
			anim.Play ("KB_Hit_m_MidFront_Med");




			//			Other.gameObject.GetComponent<Animator>().


		}

//		if (Other.gameObject.CompareTag("EnemyChest"))
//		{
//			print ("EnemyChest");
//			barz.fillAmount -= 0.04f;
//		}
//
//		if (Other.gameObject.CompareTag("EnemyLH"))
//		{
//			print ("EnemyLH");
//			barz.fillAmount -= 0.1f;
//		}
//
//
//		if (Other.gameObject.CompareTag("EnemyRH"))
//		{
//			print ("EnemyRH");
//			barz.fillAmount -= 0.04f;
//		}
//
//
//		if (Other.gameObject.CompareTag("EnemyBack"))
//		{
//			print ("EnemyBack");
//			Animator anim = Other.gameObject.GetComponentInParent<Animator>();
//			anim.Play ("KB_TurnR_180");
//			barz.fillAmount -= 0.04f;
//
//
//		}




	}
}
